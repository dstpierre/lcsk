using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LiveChat.Entities;
using System.Collections.Generic;

namespace LiveChat.WebSite
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ChatServiceReference.ChatServiceClient ChatService = new LiveChat.WebSite.ChatServiceReference.ChatServiceClient();
            OperatorServiceReference.OperatorClient OperatorService = new LiveChat.WebSite.OperatorServiceReference.OperatorClient();
            

            if (!IsPostBack)
            {
                // Get the online department
                List<DepartmentEntity> onlineDepartments = new List<DepartmentEntity>(OperatorService.GetOnlineDepartment());
                if (onlineDepartments != null && onlineDepartments.Count() > 0)
                {
                    pnlOperator.Visible = true;

                    ddlDepartments.DataSource = onlineDepartments;
                    ddlDepartments.DataTextField = "DepartmentName";
                    ddlDepartments.DataValueField = "EntityId";
                    ddlDepartments.DataBind();
                }
                else
                    pnlNoOperator.Visible = true;
                
            }
        }

        protected void RequestChat(object sender, EventArgs e)
        {
            ChatServiceReference.ChatServiceClient ChatService = new LiveChat.WebSite.ChatServiceReference.ChatServiceClient();

            ChatRequestEntity chat = new ChatRequestEntity();
            chat.DepartmentId = int.Parse(ddlDepartments.SelectedValue);
            chat.RequestedDate = DateTime.Now;
            chat.SendCopyOfChat = false; // not implemented
            chat.VisitorEmail = txtEmail.Text;
            chat.VisitorIp = Request.UserHostAddress;
            chat.VisitorName = txtName.Text;

            // We get the channel id
            //string channelId = ChatService.RequestChat(chat);
            string channelId = ChatService.RequestChat(chat);
            // TODO: Redirect to the chat session page with the channel id
        }
    }
}
