using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Collections.Concurrent;
using System.Diagnostics;


namespace Demo.LCSK
{
    public class ChatHub : Hub
    {
        private const string ConfigFile = "lcsk.dat";
        /*
        List<Agent> Agents
        {
            get
            {
                if (HttpContext.Current.Cache["lcsk_agents"] != null)
                    return (List<Agent>)HttpContext.Current.Cache["lcsk_agents"];
                else
                {
                    List<Agent> agents = new List<Agent>();
                    HttpContext.Current.Cache.Add(
                        "lcsk_agents",
                        agents,
                        null,
                        DateTime.Now.AddHours(1),
                        TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
                    return agents;
                }
            }
        }
        */
        private static ConcurrentDictionary<string, Agent> _agents;
        private static ConcurrentDictionary<string, string> _chatSessions;

        public void AgentConnect(string name, string pass)
        {
            if (_agents == null)
                _agents = new ConcurrentDictionary<string, Agent>();

            if (_chatSessions == null)
                _chatSessions = new ConcurrentDictionary<string, string>();

            string hashPass = ToHash(pass);

            var config = GetConfig();
            if (config == null || config.Length < 2)
            {
                Clients.Caller.loginResult(false, "config", "");
            }
            else if ((config[0] == hashPass) || (config[1] == hashPass))
            {
                var agent = new Agent
                {
                    Id = Context.ConnectionId,
                    Name = name,
                    IsOnline = true
                };

                // if the agent is already signed-in
                if(_agents.Any(x => x.Key == name))
                {
                    agent = _agents[name];

                    Clients.Caller.loginResult(true, agent.Id, agent.Name);

                    Clients.All.onlineStatus(_agents.Count(x => x.Value.IsOnline) > 0);
                }
                else if (_agents.TryAdd(name, agent))
                {

                    Clients.Caller.loginResult(true, agent.Id, agent.Name);

                    Clients.All.onlineStatus(_agents.Count(x => x.Value.IsOnline) > 0);
                }
                else
                {
                    Clients.Caller.loginResult(false, "error", "");
                }
            }
            else
                Clients.Caller.loginResult(false, "pass", "");
        }

        public void ChangeStatus(bool online)
        {
            var agent = _agents.SingleOrDefault(x => x.Value.Id == Context.ConnectionId).Value;
            if (agent != null)
            {
                agent.IsOnline = online;
                // TODO: Check if the agent was in chat sessions.
                Clients.All.onlineStatus(_agents.Count(x => x.Value.IsOnline) > 0);
            }
        }

        public void EngageVisitor(string connectionId)
        {
            var agent = _agents.SingleOrDefault(x => x.Value.Id == Context.ConnectionId).Value;
            if(agent != null && _chatSessions.Count(x => x.Key == connectionId && x.Value == agent.Id) == 0)
            {
                _chatSessions.TryAdd(connectionId, agent.Id);
                Clients.Caller.newChat(connectionId);
                Clients.Client(connectionId).setChat(connectionId, agent.Name, false);
                Clients.Client(connectionId).openChatWindow();
                Clients.Caller.addMessage(connectionId, "system", "You invited this visitor to chat...");
                Clients.Client(connectionId).addMessage(agent.Name, "Hey there. I'm " + agent.Name + " let me know if you have any questions.");
            }
        }

        public void LogVisit(string page, string referrer, string city, string region, string country, string existingChatId)
        {
            if (_agents == null)
                _agents = new ConcurrentDictionary<string, Agent>();

            Clients.Caller.onlineStatus(_agents.Count(x => x.Value.IsOnline) > 0);
            var cityDisplayName = GetCityDisplayName(city, region);
            var countryDisplayName = country ?? string.Empty;

            if (!string.IsNullOrEmpty(existingChatId) &&
                _chatSessions.ContainsKey(existingChatId))
            {
                var agentId = _chatSessions[existingChatId];
                Clients.Client(agentId).visitorSwitchPage(existingChatId, Context.ConnectionId, page);
                var agent = _agents.SingleOrDefault(x => x.Value.Id == agentId).Value;

                if (agent != null)
                    Clients.Caller.setChat(Context.ConnectionId, agent.Name, true);

                string buffer;
                _chatSessions.TryRemove(existingChatId, out buffer);
                _chatSessions.TryAdd(Context.ConnectionId, agentId);
            }

            foreach (var agent in _agents)
            {
                var chatWith = (from c in _chatSessions
                               join a in _agents on c.Value equals a.Value.Id
                               where c.Key == Context.ConnectionId
                               select a.Value.Name).SingleOrDefault();

                Clients.Client(agent.Value.Id).newVisit(page, referrer, cityDisplayName, countryDisplayName, chatWith, Context.ConnectionId);
            }
        }

        public void RequestChat(string message)
        {
            // We assign the chat to the less buzy agent
            var workload = from a in _agents
                           where a.Value.IsOnline
                           select new
                           {
                               a.Value.Id,
                               a.Value.Name,
                               Count = _chatSessions.Count(x => x.Value == a.Value.Id)
                           };

            var lessBuzy = workload.OrderBy(x => x.Count).FirstOrDefault();
            if (lessBuzy == null)
            {
                Clients.Caller.addMessage("", "No agent are currently available.");
                return;
            }
            
            _chatSessions.TryAdd(Context.ConnectionId, lessBuzy.Id);
            Clients.Client(lessBuzy.Id).newChat(Context.ConnectionId);
            Clients.Caller.setChat(Context.ConnectionId, lessBuzy.Name, false);
            message = Regex.Replace(message, @"(\b(?:(?:(?:https?|ftp|file)://|www\.|ftp\.)[-A-Z0-9+&@#/%?=~_|$!:,.;]*[-A-Z0-9+&@#/%=~_|$]|((?:mailto:)?[A-Z0-9._%+-]+@[A-Z0-9._%-]+\.[A-Z]{2,6})\b)|""(?:(?:https?|ftp|file)://|www\.|ftp\.)[^""\r\n]+""|'(?:(?:https?|ftp|file)://|www\.|ftp\.)[^'\r\n]+')", "<a target='_blank' href='$1'>$1</a>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Clients.Client(lessBuzy.Id).addMessage(Context.ConnectionId, "visitor", message);
            Clients.Caller.addMessage("me", message);
        }

        public void Transfer(string connectionId, string agentName, string messages)
        {
            if(!_agents.ContainsKey(agentName))
            {
                Clients.Caller.addMessage(Context.ConnectionId, "system", "This agent does not exists: " + agentName);
                return;
            }

            var agent = _agents[agentName];
            if(!agent.IsOnline)
            {
                Clients.Caller.addMessage(Context.ConnectionId, "system", agentName + " is not online at the moment.");
                return;
            }

            if(!_chatSessions.ContainsKey(connectionId))
            {
                Clients.Caller.addMessage(Context.ConnectionId, "system", "This chat session does not exists anymore.");
                return;
            }

            string currentAgentId;
            if (_chatSessions.TryRemove(connectionId, out currentAgentId) &&
                _chatSessions.TryAdd(connectionId, agent.Id))
            {
                Clients.Client(agent.Id).newChat(connectionId);
                Clients.Client(agent.Id).addMessage(connectionId, "system", "New chat transfered to you.");
                Clients.Client(agent.Id).addMessage(connectionId, ">>", "Starting previous conversation");
                Clients.Client(agent.Id).addMessage("", messages);
                Clients.Client(agent.Id).addMessage(connectionId, "<<", "End of previous conversation");

                Clients.Client(connectionId).addMessage("", "You have been transfered to " + agent.Name);
                Clients.Client(connectionId).setChat(connectionId, agent.Name, true);

                Clients.Caller.addMessage(connectionId, "system", "Chat transfered to " + agentName);
            }
        }

        public void Send(string data)
        {
            //snatch any url using regex pattern
            data = Regex.Replace(data, @"(\b(?:(?:(?:https?|ftp|file)://|www\.|ftp\.)[-A-Z0-9+&@#/%?=~_|$!:,.;]*[-A-Z0-9+&@#/%=~_|$]|((?:mailto:)?[A-Z0-9._%+-]+@[A-Z0-9._%-]+\.[A-Z]{2,6})\b)|""(?:(?:https?|ftp|file)://|www\.|ftp\.)[^""\r\n]+""|'(?:(?:https?|ftp|file)://|www\.|ftp\.)[^'\r\n]+')", "<a target='_blank' href='$1'>$1</a>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Clients.Caller.addMessage("me", data);

            if (_chatSessions.ContainsKey(Context.ConnectionId))
            {
                var opId = _chatSessions[Context.ConnectionId];
                Clients.Client(opId).addMessage(Context.ConnectionId, "visitor", data);
            }
            else
            {
                Debug.WriteLine("Chat Session not found.");

                // refactor this
                var workload = from a in _agents
                               where a.Value.IsOnline
                               select new
                               {
                                   a.Value.Id,
                                   a.Value.Name,
                                   Count = _chatSessions.Count(x => x.Value == a.Value.Id)
                               };

                var lessBuzy = workload.OrderBy(x => x.Count).FirstOrDefault();
                if (lessBuzy == null)
                {
                    Clients.Caller.addMessage("", "No agent are currently available.");
                    return;
                }

                _chatSessions.TryAdd(Context.ConnectionId, lessBuzy.Id);
                Clients.Client(lessBuzy.Id).newChat(Context.ConnectionId);
                Clients.Caller.setChat(Context.ConnectionId, lessBuzy.Name, false);
                Clients.Client(lessBuzy.Id).addMessage(Context.ConnectionId, "system", "This visitor appear to have lost their chat session.");
                Clients.Client(lessBuzy.Id).addMessage(Context.ConnectionId, "visitor", data);
            }
        }

        public void OpSend(string id, string data)
        {
            var agent = _agents.SingleOrDefault(x => x.Value.Id == Context.ConnectionId).Value;
            if (agent == null)
            {
                Clients.Caller.addMessage(id, "system", "We were unable to send your message, please reload the page.");
                return;
            }

            if (id == "internal")
            {
                foreach (var a in _agents.Where(x => x.Value.IsOnline))
                    Clients.Client(a.Value.Id).addMessage(id, agent.Name, data);
                        
            }
            else if (_chatSessions.ContainsKey(id))
            {
                data = Regex.Replace(data, @"(\b(?:(?:(?:https?|ftp|file)://|www\.|ftp\.)[-A-Z0-9+&@#/%?=~_|$!:,.;]*[-A-Z0-9+&@#/%=~_|$]|((?:mailto:)?[A-Z0-9._%+-]+@[A-Z0-9._%-]+\.[A-Z]{2,6})\b)|""(?:(?:https?|ftp|file)://|www\.|ftp\.)[^""\r\n]+""|'(?:(?:https?|ftp|file)://|www\.|ftp\.)[^'\r\n]+')", "<a target='_blank' href='$1'>$1</a>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Clients.Caller.addMessage(id, "you", data);
                Clients.Client(id).addMessage(agent.Name, data);
            }
        }

        public void CloseChat(string id)
        {
            if (_chatSessions.ContainsKey(id))
            {
                Clients.Client(id).addMessage("", "The agent close the chat session.");

                string buffer;
                _chatSessions.TryRemove(id, out buffer);
            }
        }

        public void LeaveChat(string id)
        {
            // was it an agent
            var agent = _agents.SingleOrDefault(x => x.Value.Id == id).Value;
            if (agent != null)
            {
                Agent tmp;
                if (_agents.TryRemove(agent.Name, out tmp))
                {
                    var sessions = _chatSessions.Where(x => x.Value == agent.Id);
                    foreach (var session in sessions)
                        Clients.Client(session.Key).addMessage("", "The agent was disconnected from chat.");
                    Clients.All.updateStatus(_agents.Count(x => x.Value.IsOnline) > 0);
                }
            }

            // was it a visitor
            if (_chatSessions.ContainsKey(id))
            {
                var agentId = _chatSessions[id];
                Clients.Client(agentId).addMessage(id, "system", "The visitor close the connection.");
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return Clients.All.leave(Context.ConnectionId);
        }

        public void SendEmail(string from, string message)
        {
            var config = GetConfig();
            if (config != null && config.Length >= 3)
            {
                message = "From: " + from + "\n\n" + message;

                var msg = new MailMessage();
                msg.From = new MailAddress(config[2]);
                msg.To.Add(new MailAddress(config[2]));
                msg.Subject = "LCSK - Offline Contact";
                msg.Body = "You received an offline contact from your LCSK chat widget.\r\n\r\n" + message;

                using (var client = new SmtpClient())
                {
                    client.Send(msg);
                }
            }
        }

        #region Install and config methods
        public void getInstallState()
        {
            var config = GetConfig();

            if (config != null && config.Length >= 2)
                Clients.Caller.installState(true, config[0]);
            else
                Clients.Caller.installState(false, "lcskv2hctemptoken");
        }

        public void AdminRequest(string pass)
        {
            var config = GetConfig();

            if (config != null && config.Length >= 2)
            {
                if (config[0] == ToHash(pass))
                    Clients.Caller.adminResult(true, config[0]);
                else
                    Clients.Caller.adminResult(false, "");
            }
            else
                Clients.Caller.adminResult(false, "");
        }

        public void SetConfig(string token, string adminPass, string agentPass, string email)
        {
            bool shouldSave = false;
            var config = GetConfig();

            if (config != null && config.Length >= 2)
            {
                if (config[0] == token)
                    shouldSave = true;
            }
            if (token == "lcskv2hctemptoken")
                shouldSave = true;

            if (shouldSave)
            {
                string configPath = HttpContext.Current.Server.MapPath("~/App_Data/" + ConfigFile);

                File.WriteAllText(
                    configPath,
                    ToHash(adminPass) + "\n" + ToHash(agentPass) + "\n" + email);

                Clients.Caller.setConfigResult(true, "Config file updated.");
            }
            else
                Clients.Caller.setConfigResult(false, "Unable to save the config file.");
        }

        private string GetCityDisplayName(string city, string region)
        {
            var displayCity = string.Empty;
            if (!string.IsNullOrEmpty(city))
            {
                displayCity = city;
                if (!string.IsNullOrEmpty(region))
                {
                    displayCity += ", " + region;
                }
            }
            return displayCity;
        }

        private string[] GetConfig()
        {
            string configPath = HttpContext.Current.Server.MapPath("~/App_Data/" + ConfigFile);
            if (File.Exists(configPath))
            {
                return File.ReadAllLines(configPath);
            }
            return null;
        }

        public string ToHash(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "";

            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return Convert.ToBase64String(provider.ComputeHash(encoding.GetBytes(password)));
        }
        #endregion
    }
}