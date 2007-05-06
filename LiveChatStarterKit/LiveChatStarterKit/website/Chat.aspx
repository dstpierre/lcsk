<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Chat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Chat Session</title>
    <style type="text/css">
    body
    {
		font-family: Arial;
		color: black;
		font-size: 10pt;
    }
    #Welcome
    {
		font-weight: bold;
		color: blue;
		font-size: 12pt;
		width: 500px;
    }
    .formField
    {
		font-weight: bold;
    }
	#chat
	{
		height: 300px;
		width: 590px;
		overflow: auto;
		padding: 8px;
	}
	.chatName { color: blue; }
    </style>
    <script language="javascript">
		function scrollDiv()
		{
			var d;
			if ((d = document.getElementById('chat')) && ('undefined' != typeof d.scrollTop))
			{
				//d.scrollTop = 0;
				d.scrollTop = 5000;
			}
			
			window.setTimeout("scrollDiv()", 950);
		}
		
		window.onload = scrollDiv;
    </script>
</head>
<body topmargin="0" leftmargin="0">
    <form id="formMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <img src="Images/logo_base.jpg" alt="Place your logo here" /><br /><br />
    <asp:panel id="pnlNoOperator" visible="false" runat="server">
		<div style="padding-left: 5px;">
			<asp:label id="lblConfirmation" visible="false" runat="server"></asp:label>
			
			<span id="Welcome">There are no operator available for the moment.<br />Please leave us a message.</span>
			<br /><br />
			<span class="formField">Your Email</span><br />
			<asp:textbox id="txtSendBy" runat="server"></asp:textbox><br /><br />
			<span class="formField">Your questions / comments</span><br />
			<asp:textbox id="txtComment" textmode="MultiLine" width="350" height="75" runat="server"></asp:textbox><br /><br />
			<asp:linkbutton id="lnkSendEmail" text="Send Email" runat="server" onclick="lnkSendEmail_Click"></asp:linkbutton>
			</div>
    </asp:panel>
    <asp:panel id="pnlRequest" visible="true" runat="server">
    <div style="padding-left: 5px;">
    <span id="Welcome">Please fill the form below to request a chat session.</span><br />
    <span class="formField">Your Name</span><br />
    <asp:textbox id="txtName" runat="server"></asp:textbox><br /><br />
    <span class="formField">Your Email</span><br />
    <asp:textbox id="txtEmail" runat="server"></asp:textbox><br /><br />
    <asp:linkbutton id="lnkStartChat" text="Request Chat" runat="server" onclick="lnkStartChat_Click"></asp:linkbutton>
    </div>
    </asp:panel>
    <asp:panel id="pnlChat" visible="false" runat="server">
		<asp:updatepanel id="upChat" runat="server">
		<triggers>
			<asp:asyncpostbacktrigger controlid="timerRefresh" />
		</triggers>
		<contenttemplate>
			<div id="chat">
				<asp:literal id="litChat" runat="server"></asp:literal>
			</div>
		</contenttemplate>
		</asp:updatepanel>
    <br />
    &nbsp;&nbsp;
    <asp:textbox id="txtMsg" width="500" runat="server"></asp:textbox>
    <asp:button id="btnSendMsg" text="Send" runat="server" onclick="btnSendMsg_Click" /><br /><br />
		<asp:timer id="timerRefresh" interval="2123" runat="server" ontick="timerRefresh_Tick">
		</asp:timer>
    </asp:panel>
    </form>
</body>
</html>
