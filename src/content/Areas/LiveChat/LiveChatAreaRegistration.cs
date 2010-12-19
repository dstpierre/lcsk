using System.Web.Mvc;

namespace LCSK.Web.Areas.LiveChat
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
				"LiveChat/{action}/{id}",
				new { controller = "Chat", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
