#### 2016/11/16 - Please note that LCSK is not maintained anymore

I've started this .NET project in 2007, but since 2014 I'm not running on Windows nor doing .NET development anymore.

I'm currently considering a brand new open source project, similar to what [Intercom](https://intercom.io) are doing with all the marketing automation and good stuff. I'm considering building this on livecoding.tv. This is still only a thought.

But if there's interest, I would be more energized :). In any case, [read this for more detail](https://github.com/dstpierre/lcsk/issues/53)

Thanks to everyone that tried LCSK.

------------------------------------------------------------
original README
------------------------------------------------------------

LCSK (LiveChat Starter Kit)
=============================

LCSK is a simple, lightweight live chat / live support ASP.NET C# app. It uses SignalR for the communication channel 
between the website visitors and the agent(s). No database are required.

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

Startup.cs issues
-------------------------------------------

If you already have a Startup.cs file, please copy the app.MapSignalR() to your existing Startup.cs file 
and delete the one supplied in LCSK/Startup.cs.

Adding the visitor chat box to your pages
-------------------------------------------

Please note that I've included the SignalR and jQuery javascript files into the LCSK/js for quick 
references. Feel free to remove those .js files and use the one that came with the SignalR package instead or 
use whatever file structure you like.

	*** If you do not already have jQuery referenced
	<script src="/lcsk/assets/js/jquery-2.1.1.min.js" type="text/javascript"></script>
	***
	<script src="/lcsk/assets/js/jquery.signalR-2.1.1.min.js" type="text/javascript"></script>
	<script src="/signalr/hubs" type="text/javascript"></script>
	<script src="/lcsk/chat.js" type="text/javascript"></script>

Login in to the agent panel
----------------------------

To login you'll first have to set an admin and agent password. Simply go to /lcsk/install.html to set 
both passwords.

Return to /lcsk/agent.html and login with the following credentials:

* username:* anything (i.e: Dominic)
* password:* either the admin or agent password.

You can have an unlimited number of agent, they'll all have the same password and uses different names.

View a demo
----------------

There is a Demo project in the src folder. It's an ASP.NET MVC 4 web app. You can run LCSK in a web 
forms application as well.

Videos
------------------

You can watch a video on how to add LCSK to an existing project.

[How to add LCSK to your project](http://youtu.be/DjaO4R1knJE)


Project Status
-----------------------

I'm continuously improving this project. Contribution is very welcome. I've started the project in 
2007.

