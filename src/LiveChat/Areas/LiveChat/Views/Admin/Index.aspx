<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/LiveChat/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LiveChat Starter Kit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
    	$(document).ready(function () {
    		$("#tabs").tabs();
    	});
	</script>

	<div id="login">
		<div id="tabs">
			<ul>
				<li><a href="#tabs-1">Sign In</a></li>
			</ul>
			<div id="tabs-1">
				<h3>Sign In to start an operator session</h3>

				<% using (Html.BeginForm())
	   { %>
				
				<% } %>
			</div>
		</div>
	</div>

</asp:Content>
