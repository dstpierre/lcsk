<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Demo Page</title>
    <script language="Javascript">
    function openChat()
    {
        var win = window.open('Default.aspx?OrderID=1234&CustomParam=Some other param&ClientID=2020', 'chat', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,width=450,height=510');
        win.focus();
        win.opener = window;
        return false;
    }
    </script>
</head>
<body>
    <form id="mainForm" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
		<script language="javascript">
			document.write('<a href="javascript://" onclick="openChat();"><img id="_imgLC" src="ProcessImage.ashx?referrer=' + document.referrer + '" border="0" alt="Click here for live chat" /></a>');
		</script>         
        </div>
    </form>
</body>
</html>
