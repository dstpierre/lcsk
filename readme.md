LCSK (LiveChat Starter Kit)
=============================

LCSK is a simple, lightweight live chat / live support ASP.NET C# app. It uses SignalR for the communication channel 
between the website visitors and the agent(s).

I just moved the project over to GitHub, it was previoulsy hosted on [CodePlex](http://livechatstarterkit.codeplex.com).

What can it be used for?
--------------------------

If you have a website and want to offer live support to the visitors, you can do this easily with LCSK.

Get it on NuGet
-------------------

	Install-Package LCSK

Manual installation
---------------------------

You will need an ASP.NET 4 web application to start. Since LCSK depend on 
[SignalR](http://signalr.net), you'll need to add this from Nuget.

	Install-Package Microsoft.AspNet.SignalR

You can copy the LCSK folder into your project. You might want to change the namespacing of the 
.cs file, when using NuGet this is done automatically.

Adding the visitor chat box to your pages
-------------------------------------------

Please not that I've included the SignalR and jQuery javascript files into the LCSK/js for quick 
references. Feel free to remove those .js files and use the one that came with the SignalR package instead or 
use whatever file structure you like.

	*** If you do not already have jQuery referenced
	<script src="/lcsk/assets/js/jquery-1.7.1.min.js" type="text/javascript"></script>
	***
	<script src="/lcsk/assets/js/jquery.signalR-2.0.1.min.js" type="text/javascript"></script>
	<script src="/signalr/hubs" type="text/javascript"></script>
	<script src="/LCSK/chat.js" type="text/javascript"></script>

Login in to the agent panel
----------------------------

To login you'll first have to set an admin and agent password. Simply to to /lcsk/install.html to set 
both passwords.

Return to /lcsk/agent.html and login with the following credentials:

* username:* anything (i.e: Dominic)
* password:* either the admin or agent password.

You can have an unlimited number of agent, they'll all have the same password and uses different name.

View a demo
----------------

There is a Demo project in the src folder. It's an ASP.NET MVC 4 web app. You can run LCSK in a web 
forms application as well.

Videos
------------------

I've started a set of video of me rewriting the app using SingalR.

1. [Pluging SignalR](http://youtu.be/Mmq9yLXVqcU) - 2012/11/16
2. [Agent Panel](http://twitter.com/dominicstpierre) - 2012/11/19
3. [Visitor experience](http://youtu.be/vwvV-XrXO7s) - 2012/11/21

Project Status
-----------------------

I'm continuously improving this project. Contribution is very welcome. I've started the project in 
2007.

