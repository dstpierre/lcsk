<%@ Page language="C#" autoeventwireup="true" codebehind="Chat.aspx.cs" inherits="LiveChat.WebSite.Chat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
			color: gray;
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
		.chatName
		{
			color: gray;
		}
	</style>

	<script language="javascript" type="text/javascript">
		var lastCheck = new Date();
		 
		function scrollDiv()
		{
			var d;
			if ((d = document.getElementById('chat')) && ('undefined' != typeof d.scrollTop))
			{
				//d.scrollTop = 0;
				d.scrollTop = 5000;
			}
			
			window.setTimeout("scrollDiv()", 950);
			
			var now = new Date();
			var elapse = now.getSeconds() - lastCheck.getSeconds();
			var sameMinute = now.getMinutes - lastCheck.getMinutes();
			if( sameMinute != 0 || elapse >= 2 )
			{
				// Check for typing notification
				PageMethods.CheckTypingNotification(getCookie('chatId'), OnCheckTypingNotificationComplete);
				
				lastCheck = new Date();
			}
		}
		
		function OnCheckTypingNotificationComplete(result, methodName)
		{
			if( result != '' )
			{
				var v = document.getElementById('typingNotification');
				if( v != 'undefined' ) 
					v.innerText = result; 
			}
		}
		
		window.onload = scrollDiv;
	</script>

</head>
<body topmargin="0" leftmargin="0">
	<form id="formMain" runat="server">
	<asp:ScriptManager id="ScriptManager1" runat="server" enablepagemethods="True">
		<Scripts>
			<asp:ScriptReference path="SendMsg.js" />
		</Scripts>
	</asp:ScriptManager>
	<img src="Images/logo_base.JPG" alt="Itookia logo" /><br />
	<br />
	<asp:Panel id="pnlNoOperator" visible="false" runat="server">
		<div style="padding-left: 5px;">
			<asp:Label id="lblConfirmation" visible="false" runat="server"></asp:Label>
			<span id="Welcome">There are no operator available for the moment.<br />
				Please leave us a message.</span>
			<br />
			<br />
			<span class="formField">Your Email</span><br />
			<asp:TextBox id="txtSendBy" runat="server"></asp:TextBox><br />
			<br />
			<span class="formField">Your questions / comments</span><br />
			<asp:TextBox id="txtComment" textmode="MultiLine" width="350" height="75" runat="server"></asp:TextBox><br />
			<br />
			<asp:LinkButton id="lnkSendEmail" text="Send Email" runat="server" onclick="lnkSendEmail_Click"></asp:LinkButton>
		</div>
	</asp:Panel>
	<asp:Panel id="pnlRequest" visible="true" runat="server">
		<div style="padding-left: 5px;">
			<span id="Welcome">Please fill the form below to request a chat session.</span><br />
			<span class="formField">Your Name</span><br />
			<asp:TextBox id="txtName" runat="server"></asp:TextBox><br />
			<br />
			<span class="formField">Your Email</span><br />
			<asp:TextBox id="txtEmail" runat="server"></asp:TextBox><br />
			<br />
			<asp:LinkButton id="lnkStartChat" text="Request Chat" runat="server" onclick="lnkStartChat_Click"></asp:LinkButton>
		</div>
	</asp:Panel>
	<asp:Panel id="pnlChat" visible="false" runat="server">
		<div id="chat">
			<asp:UpdatePanel id="upChat" runat="server" updatemode="Conditional" childrenastriggers="False">
				<Triggers>
					<asp:AsyncPostBackTrigger controlid="timerRefresh" eventname="Tick" />
				</Triggers>
				<ContentTemplate>
					<asp:Literal id="litChat" runat="server"></asp:Literal>
					<asp:Timer id="timerRefresh" interval="2223" runat="server" ontick="timerRefresh_Tick">
					</asp:Timer>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
		<br />
		<br />
		<input type="text" id="txtMsg" runat="server" style="width: 500px;" onkeypress="checkEnter(event)" />
		<input type="button" id="btnSend" value="Send" onclick="CallSendMsg()" /><br />
		<span id="typingNotification">Status: </span>
	</asp:Panel>
	</form>
</body>
</html>
