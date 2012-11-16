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

        public void AgentConnect(string name)
        {
            var agent = new Agent()
            {
                Id = Context.ConnectionId,
                Name = name,
                IsOnline = true
            };

            Agents.Add(agent);
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
            ChatSessions.Add(Context.ConnectionId, "");

            Caller.addMessage("Please wait for an agent...");

            foreach (var agent in Agents)
                Clients[agent.Id].newChat(Context.ConnectionId);
        }

        public void AcceptChat(string id)
        {
            if (ChatSessions.ContainsKey(id))
            {
                ChatSessions[id] = Context.ConnectionId;

                var agent = Agents.Single(x => x.Id == Context.ConnectionId);

                Clients[id].addMessage("You are now connected with " + agent.Name);

                Caller.addMessage("Chat is established.");
            }
        }

        public void Send(string data)
        {
            Caller.addMessage(data);

            if (ChatSessions.ContainsKey(Context.ConnectionId))
            {
                var opId = ChatSessions[Context.ConnectionId];
                Clients[opId].addMessage(Context.ConnectionId, data);
            }
        }

        public void OpSend(string id, string data)
        {
            if (ChatSessions.ContainsKey(id))
            {
                Caller.addMessage(id, data);
                Clients[id].addMessage(data);
            }
        }

        public Task Disconnect()
        {
            // was it an agent
            var agent = Agents.SingleOrDefault(x => x.Id == Context.ConnectionId);
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

            return null;
        }
    }
}