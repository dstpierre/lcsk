using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Entities;
using LiveChat.Providers;

namespace LiveChat.BusinessLogic
{
    public class Operator
    {
        private static OperatorProvider _provider = LiveChat.Providers.Manager.Operator.Provider;
        //private static object _lock = new object();

        //public OperatorProvider Provider
        //{
        //    get { return _provider; }
        //}

        public static List<OperatorEntity> GetOnlineOperator()
        {
            // Load the provider
            //LoadProvider();

            return _provider.GetOnlineOperator();
        }

        public static bool IsOperatorOnline()
        {
            // Load the provider
            //LoadProvider();

            return _provider.IsOperatorOnline();
        }

        public static int Create(string name, string password, string email, bool isAdmin)
        {
            // Load the provider
            //LoadProvider();

            return _provider.Create(name, password, email, isAdmin);
        }

        public static void UpdateStatus(int operatorId, bool isOnline)
        {
            // Load the provider
            //LoadProvider();

            _provider.UpdateStatus(operatorId, isOnline);
        }


        public static OperatorEntity LogIn(string name, string password)
        {
            // Load the provider
            //LoadProvider();

            return _provider.LogIn(name, password);
        }

        public static bool Remove(int operatorId)
        {
            // Load the provider
            //LoadProvider();

            return _provider.Remove(operatorId);
        }

        public static List<ChannelEntity> GetChatChannel(int operatorId)
        {
            // Load the provider
            //LoadProvider();

            return _provider.GetChatChannel(operatorId);
        }

        public static List<DepartmentEntity> GetOnlineDepartment()
        {
            return _provider.GetOnlineDepartment();
        }
    }
}
