<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Demo chat button</h2>
    
	<a href="#" onclick="window.open('/LiveChat/Visitor', 'lcsk', 'scrollbars,resizable,width=700,height=530');">
	<img src="/LiveChat/Visitor/ChatImage" alt="LiveChat Starter Kit" />
	</a>
</asp:Content>
