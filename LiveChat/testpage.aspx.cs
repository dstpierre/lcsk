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

namespace LiveChat.WebSite
{
    public partial class testpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(LiveChat.Providers.Manager.Operator.Provider.Name);
            //LiveChat.Entities.MessageEntity msg = new LiveChat.Entities.MessageEntity();
            //msg.Message = "x";
            //ChatService.WriteMessage(msg);
        }
    }
}
