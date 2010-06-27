<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/LiveChat/Views/Shared/Admin.Master"
	Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Installing LiveChat Starter Kit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#tabs").tabs();
		});
	</script>
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1">Installation</a></li>
		</ul>
		<div id="tabs-1">
			<h3>Installing LiveChat Starter Kit by Focus Centric inc.</h3>

			<p>Supply your SQL Server information so we can create tables for LCSK.</p>

			<% using (Html.BeginForm())
	  { %>
			
			<fieldset>
				<legend>Database setup</legend>

				<p>
					<%= Html.Label("SQL Server Name") %>
					<%= Html.TextBox("server") %><%= Html.ValidationMessage("server") %>
				</p>

				<p>
					<%= Html.Label("Database name") %>
					<%= Html.TextBox("dbname") %><%= Html.ValidationMessage("dbname") %>
				</p>

				<p>If you are not using Integrated Security, enter a username & password</p>

				<p>
					<%= Html.Label("Username (optional)") %>
					<%= Html.TextBox("username") %>
				</p>

				<p>
					<%= Html.Label("Password (optional)") %>
					<%= Html.Password("password") %>
				</p>

				<p>
					<input type="submit" value="Create Database" />
				</p>
			</fieldset>


	<% } %>

		</div>
	</div>
</asp:Content>
