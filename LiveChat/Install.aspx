<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="LiveChat.WebSite.Install" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
		<table border="0" cellpadding="4" cellspacing="5">
		<tr>
			<td><img src="Images/livechat_logo.jpg" alt="logo" /></td>
			<td><h1>Installing LiveChat Starter Kit 1.0</h1></td>
		</tr>
		</table>
		<h3>Please perform the following manual tasks</h3>
		
		<ol>
			<li>Edit the web.config ConnectionStrings section (LiveChat connectionString)</li>
			<li>Optionally create a database in SQL Server and run the CreateDB.sql script or create the database from this page.</li>
		</ol>
		
		<h3>Database creation</h3>
		
		<p>Once the web.config is saved, you may create the database by clicking on the following button.</p>
		
		<p style="color: Red;"><asp:Literal id="litCreated" runat="server"></asp:Literal></p>	
		
		<p><asp:Button id="btnCreate" text="Create the database" onclick="CreateDB" runat="server" /></p>
		
		
    </form>
</body>
</html>
