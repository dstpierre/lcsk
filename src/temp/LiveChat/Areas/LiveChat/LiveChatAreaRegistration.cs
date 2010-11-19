using System.Web.Mvc;

namespace LCSK
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
				new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
