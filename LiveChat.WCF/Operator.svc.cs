using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveChat.Entities;

namespace LiveChat.WCF
{
    // NOTE: If you change the class name "Operator" here, you must also update the reference to "Operator" in Web.config.
    public class Operator : IOperator
    {
        public List<OperatorEntity> GetOnlineOperator()
        {
            // Load the provider
            //LoadProvider();

            return LiveChat.BusinessLogic.Operator.GetOnlineOperator();
        }

        public bool IsOperatorOnline()
        {
            // Load the provider
            //LoadProvider();

            return LiveChat.BusinessLogic.Operator.IsOperatorOnline();
        }

        public int Create(string name, string password, string email, bool isAdmin)
        {
            // Load the provider
            //LoadProvider();

            return LiveChat.BusinessLogic.Operator.Create(name, password, email, isAdmin);
        }

        public void UpdateStatus(int operatorId, bool isOnline)
        {
            // Load the provider
            //LoadProvider();

            LiveChat.BusinessLogic.Operator.UpdateStatus(operatorId, isOnline);
        }


        public OperatorEntity LogIn(string name, string password)
        {
            // Load the provider
            //LoadProvider();

            return LiveChat.BusinessLogic.Operator.LogIn(name, password);
        }

        public bool Remove(int operatorId)
        {
            // Load the provider
            //LoadProvider();

            return LiveChat.BusinessLogic.Operator.Remove(operatorId);
        }

        public List<ChannelEntity> GetChatChannel(int operatorId)
        {
            // Load the provider
            //LoadProvider();

            return LiveChat.BusinessLogic.Operator.GetChatChannel(operatorId);
        }

        public List<DepartmentEntity> GetOnlineDepartment()
        {
            return LiveChat.BusinessLogic.Operator.GetOnlineDepartment();
        }
    }
}
