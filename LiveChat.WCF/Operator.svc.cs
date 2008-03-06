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

        public List<OperatorEntity> Fetch()
        {
            return LiveChat.BusinessLogic.Operator.Fetch();
        }

        public bool Save(OperatorEntity updatedEntity)
        {
            return LiveChat.BusinessLogic.Operator.Save(updatedEntity);
        }

        public List<DepartmentEntity> FetchDepartment()
        {
            return LiveChat.BusinessLogic.Operator.FetchDepartment();
        }

        public int CreateDepartment(string departmentName)
        {
            return LiveChat.BusinessLogic.Operator.CreateDepartment(departmentName);
        }

        public bool RemoveDepartment(int departmentId)
        {
            return LiveChat.BusinessLogic.Operator.RemoveDepartment(departmentId);
        }

        public bool AddOperatorToDepartment(int departmentId, int operatorId)
        {
            return LiveChat.BusinessLogic.Operator.AddOperatorToDepartment(departmentId, operatorId);
        }

        public bool RemoveOperatorFromDepartment(int departmentId, int operatorId)
        {
            return LiveChat.BusinessLogic.Operator.RemoveOperatorFromDepartment(departmentId, operatorId);
        }

        public List<OperatorEntity> GetOperator(int departmentId)
        {
            return LiveChat.BusinessLogic.Operator.GetOperator(departmentId);
        }
    }
}
