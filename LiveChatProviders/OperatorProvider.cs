using System;
using System.Configuration.Provider;
using System.Collections.Generic;
using LiveChat.Entities;

namespace LiveChat.Providers
{
    public abstract class OperatorProvider : ProviderBase
    {
        public abstract List<OperatorEntity> GetOnlineOperator();
        public abstract bool IsOperatorOnline();
        
        public abstract int Create(string name, string password, string email, bool isAdmin);
        public abstract void UpdateStatus(int operatorId, bool isOnline);

        public abstract OperatorEntity LogIn(string name, string password);

        public abstract bool Remove(int operatorId);
        
        public abstract List<ChannelEntity> GetChatChannel(int operatorId);
        
    }
}