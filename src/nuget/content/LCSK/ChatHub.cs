using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Demo.LCSK
{
    public class ChatHub : Hub, IDisconnect
    {
        private const string CONFIG_FILE = "lcsk.dat";

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

        Dictionary<string, string> ChatSessions
        {
            get
            {
                if (HttpContext.Current.Cache["lcsk_sessions"] != null)
                    return (Dictionary<string, string>)HttpContext.Current.Cache["lcsk_sessions"];
                else
                {
                    Dictionary<string, string> sessions = new Dictionary<string, string>();
                    HttpContext.Current.Cache.Add(
                        "lcsk_sessions",
                        sessions,
                        null,
                        DateTime.Now.AddHours(1),
                        TimeSpan.Zero,
                        System.Web.Caching.CacheItemPriority.Default, null);
                    return sessions;
                }
            }
        }

        public void AgentConnect(string name, string pass)
        {
            string hashPass = ToHash(pass);

            var config = GetConfig();
            if (config == null || config.Length < 2)
            {
                Caller.loginResult(false, "config", "");
            }
            else if ((config[0] == hashPass) || (config[1] == hashPass))
            {
                var agent = new Agent()
                {
                    Id = Context.ConnectionId,
                    Name = name,
                    IsOnline = true
                };

                Agents.Add(agent);

                Caller.loginResult(true, agent.Id, agent.Name);

                Clients.onlineStatus(Agents.Count(x => x.IsOnline) > 0);
            }
            else
                Caller.loginResult(false, "pass", "");
        }

        public void ChangeStatus(bool online)
        {
            var agent = Agents.SingleOrDefault(x => x.Id == Context.ConnectionId);
            if (agent != null)
            {
                agent.IsOnline = online;

                // TODO: Check if the agent was in chat sessions.

                Clients.onlineStatus(Agents.Count(x => x.IsOnline) > 0);
            }
        }

        public void LogVisit(string page, string referrer, string existingChatId)
        {
            Caller.onlineStatus(Agents.Count(x => x.IsOnline) > 0);

            if (!string.IsNullOrEmpty(existingChatId) &&
                ChatSessions.ContainsKey(existingChatId))
            {
                var agentId = ChatSessions[existingChatId];
                Clients[agentId].visitorSwitchPage(existingChatId, Context.ConnectionId, page);

                var agent = Agents.SingleOrDefault(x => x.Id == agentId);

                if (agent != null)
                    Caller.setChat(Context.ConnectionId, agent.Name, true);

                ChatSessions.Remove(existingChatId);

                ChatSessions.Add(Context.ConnectionId, agentId);
            }

            foreach (var agent in Agents)
            {
                var chatWith = (from c in ChatSessions
                               join a in Agents on c.Value equals a.Id
                               where c.Key == Context.ConnectionId
                               select a.Name).SingleOrDefault();

                Clients[agent.Id].newVisit(page, referrer, chatWith);
            }
        }

        public void RequestChat(string message)
        {
            // We assign the chat to the less buzy agent
            var workload = from a in Agents
                           where a.IsOnline
                           select new
                           {
                               Id = a.Id,
                               Name = a.Name,
                               Count = ChatSessions.Count(x => x.Value == a.Id)
                           };

            if (workload == null)
            {
                Caller.addMessage("", "No agent are currently available.");
                return;
            }

            var lessBuzy = workload.OrderBy(x => x.Count).FirstOrDefault();

            if (lessBuzy == null)
            {
                Caller.addMessage("", "No agent are currently available.");
                return;
            }
            
            ChatSessions.Add(Context.ConnectionId, lessBuzy.Id);

            Clients[lessBuzy.Id].newChat(Context.ConnectionId);

            Caller.setChat(Context.ConnectionId, lessBuzy.Name, false);

            Clients[lessBuzy.Id].addMessage(Context.ConnectionId, "visitor", message);
            Caller.addMessage("me", message);
        }

        public void Send(string data)
        {
            Caller.addMessage("me", data);

            if (ChatSessions.ContainsKey(Context.ConnectionId))
            {
                var opId = ChatSessions[Context.ConnectionId];
                Clients[opId].addMessage(Context.ConnectionId, "visitor", data);
            }
            else
            {
                // refactor this
                var workload = from a in Agents
                               where a.IsOnline
                               select new
                               {
                                   Id = a.Id,
                                   Name = a.Name,
                                   Count = ChatSessions.Count(x => x.Value == a.Id)
                               };

                if (workload == null)
                {
                    Caller.addMessage("", "No agent are currently available.");
                    return;
                }

                var lessBuzy = workload.OrderBy(x => x.Count).FirstOrDefault();

                if (lessBuzy == null)
                {
                    Caller.addMessage("", "No agent are currently available.");
                    return;
                }

                ChatSessions.Add(Context.ConnectionId, lessBuzy.Id);

                Clients[lessBuzy.Id].newChat(Context.ConnectionId);

                Caller.setChat(Context.ConnectionId, lessBuzy.Name, false);

                Clients[lessBuzy.Id].addMessage(Context.ConnectionId, "system", "This visitor appear to have lost their chat session.");
                Clients[lessBuzy.Id].addMessage(Context.ConnectionId, "visitor", data);
            }
        }

        public void OpSend(string id, string data)
        {
            if (ChatSessions.ContainsKey(id))
            {
                var agent = Agents.SingleOrDefault(x => x.Id == Context.ConnectionId);

                if (agent == null)
                {
                    Caller.addMessage(id, "system", "We were unable to send your message, please reload the page.");
                    return;
                }

                Caller.addMessage(id, "you", data);
                Clients[id].addMessage(agent.Name, data);
            }
        }

        public void CloseChat(string id)
        {
            if (ChatSessions.ContainsKey(id))
            {
                Clients[id].addMessage("", "The agent close the chat session.");

                ChatSessions.Remove(id);
            }
        }

        public void LeaveChat(string id)
        {
            // was it an agent
            var agent = Agents.SingleOrDefault(x => x.Id == id);
            if (agent != null)
            {
                Agents.Remove(agent);

                var sessions = ChatSessions.Where(x => x.Value == agent.Id);
                if(sessions != null)
                {
                    foreach(var session in sessions)
                        Clients[session.Key].addMessage("", "The agent was disconnected from chat.");
                }

                Clients.updateStatus(Agents.Count(x => x.IsOnline) > 0);
            }

            // was it a visitor
            if (ChatSessions.ContainsKey(id))
            {
                var agentId = ChatSessions[id];
                Clients[agentId].addMessage(id, "system", "The visitor close the connection.");
            }
        }

        public Task Disconnect()
        {
            return Clients.leave(Context.ConnectionId);
        }

        #region Install and config methods
        public void getInstallState()
        {
            var config = GetConfig();

            if (config != null && config.Length >= 2)
                Caller.installState(true, config[0]);
            else
                Caller.installState(false, "lcskv2hctemptoken");
        }

        public void AdminRequest(string pass)
        {
            var config = GetConfig();

            if (config != null && config.Length >= 2)
            {
                if (config[0] == ToHash(pass))
                    Caller.adminResult(true, config[0]);
                else
                    Caller.adminResult(false, "");
            }
            else
                Caller.adminResult(false, "");
        }

        public void SetConfig(string token, string adminPass, string agentPass)
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
                string configPath = HttpContext.Current.Server.MapPath("~/App_Data/" + CONFIG_FILE);

                File.WriteAllText(
                    configPath,
                    ToHash(adminPass) + "\n" + ToHash(agentPass));

                Caller.setConfigResult(true, "Config file updated.");
            }
            else
                Caller.setConfigResult(false, "Unable to save the config file.");
        }

        private string[] GetConfig()
        {
            string configPath = HttpContext.Current.Server.MapPath("~/App_Data/" + CONFIG_FILE);
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