using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using System.Threading.Tasks;


namespace Demo.LCSK
{
    public class ChatHub : Hub, IDisconnect
    {
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
            if (name == "test" && pass == "debug")
            {
                var agent = new Agent()
                {
                    Id = Context.ConnectionId,
                    Name = name,
                    IsOnline = true
                };

                Agents.Add(agent);

                Caller.loginResult(true, agent.Id, agent.Name);
            }
            else
                Caller.loginResult(false, "", "");
        }

        public void ChangeStatus(bool online)
        {
            var agent = Agents.SingleOrDefault(x => x.Id == Context.ConnectionId);
            if (agent != null)
            {
                agent.IsOnline = online;
            }
        }

        public void RequestChat()
        {
            // We assign the chat to the less buzy agent
            var workload = from a in Agents
                           where a.IsOnline
                           select new
                           {
                               Id = a.Id,
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

            Caller.addMessage("", "Please wait for an agent...");
        }

        public void Send(string data)
        {
            Caller.addMessage("me", data);

            if (ChatSessions.ContainsKey(Context.ConnectionId))
            {
                var opId = ChatSessions[Context.ConnectionId];
                Clients[opId].addMessage(Context.ConnectionId, "visitor", data);
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

        public Task Disconnect()
        {
            //TODO: Check how to handle disconnection properly
            // was it an agent
            /*var agent = Agents.SingleOrDefault(x => x.Id == Context.ConnectionId);
            if (agent != null)
            {
                Agents.Remove(agent);

                var sessions = ChatSessions.Where(x => x.Value == agent.Id);
                if(sessions != null)
                {
                    foreach(var session in sessions)
                        Clients[session.Key].addMessage("The agent was disconnected from chat.");
                }

                Clients.updateStatus(Agents.Count(x => x.IsOnline) > 0);
            }

            if (ChatSessions.ContainsKey(Context.ConnectionId))
            {
                var agentId = ChatSessions[Context.ConnectionId];
                Clients[agentId].addMessage("The visitor close the connection.");
            }
            */
            return null;
        }
    }
}