using System.Web.Mvc;

namespace LiveChat.Areas.LiveChat
{
	public class LiveChatAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "LiveChat";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"LiveChat_default",
				"LiveChat/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
