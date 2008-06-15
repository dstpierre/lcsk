<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="LiveChat.WebSite._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Live Chat</title>
</head>
<body>
    <form id="mainForm" runat="server">
    <div style="width: 500px;">
		<div style="float: left;"><img src="Images/livechat_logo.jpg" /></div>
		<div style="float: right;"><h2>LiveChat Starter Kit</h2>
			<blockquote><h4>An open-source Live Help / Live Support application</h4></blockquote>
		</div>
		<asp:panel id="pnlOperator" visible="false" runat="server">
		<h5>Please fill the form below to request a chat with an operator</h5>
		<p>
			<strong>Your name</strong><br />
			<asp:textbox id="txtName" width="250px" runat="server"></asp:textbox>
		</p>
		
		<p>
			<strong>Your email (optional)</strong><br />
			<asp:textbox id="txtEmail" width="250px" runat="server"></asp:textbox>
		</p>
		
		<p>
			<strong>Department</strong><br />
			<asp:dropdownlist id="ddlDepartments" runat="server"></asp:dropdownlist>
		</p>
		
		<p>
			<asp:linkbutton id="lnkRequestChat" text="Request Chat Now" onclick="RequestChat" runat="server"></asp:linkbutton>
		</p>
		</asp:panel>
		
		<asp:panel id="pnlNoOperator" visible="false" runat="server">
		<h5>There is currently no operator online. Please leave a message.</h5>
		
		<p>
			<strong>Your name</strong><br />
			<asp:textbox id="txtYourName" width="250px" runat="server"></asp:textbox>
		</p>
		
		<p>
			<strong>Your email</strong><br />
			<asp:textbox id="txtYourEmail" width="250px" runat="server"></asp:textbox>
		</p>
		
		<p>
			<strong>Message</strong><br />
			<asp:textbox id="txtComment" textmode="MultiLine" width="250px" height="150px" runat="server"></asp:textbox>
		</p>
		
		<p>
			<asp:linkbutton id="lnkSendComment" text="Send the message" runat="server"></asp:linkbutton>
		</p>
		</asp:panel>
    </div>
    </form>
</body>
</html>
