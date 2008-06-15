using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveChat.Entities;

namespace LiveChat.WCF
{
    // NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in Web.config.
    [ServiceContract]
    public interface IChatService
    {

        [OperationContract]
        string RequestChat(ChatRequestEntity request);
        [OperationContract]
        void WriteMessage(MessageEntity msg);
        [OperationContract]
        List<MessageEntity> GetMessages(string channelId, long lastId);
        [OperationContract]
        void RemoveChatRequest(int requestId);
        [OperationContract]
        bool HasNewMessage(string channelId, long lastId);

    }

}
