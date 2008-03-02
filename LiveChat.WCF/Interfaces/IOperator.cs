using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveChat.Entities;

namespace LiveChat.WCF
{
    // NOTE: If you change the interface name "IOperator" here, you must also update the reference to "IOperator" in Web.config.
    [ServiceContract]
    public interface IOperator
    {
        [OperationContract]
        List<OperatorEntity> GetOnlineOperator();
        [OperationContract]
        bool IsOperatorOnline();
        [OperationContract]
        int Create(string name, string password, string email, bool isAdmin);
        [OperationContract]
        void UpdateStatus(int operatorId, bool isOnline);
        [OperationContract]
        OperatorEntity LogIn(string name, string password);
        [OperationContract]
        bool Remove(int operatorId);
        [OperationContract]
        List<ChannelEntity> GetChatChannel(int operatorId);
        [OperationContract]
        List<DepartmentEntity> GetOnlineDepartment();
    }
}
