LiveChat Starter Kit version 2.1.0

http://livechatstarterkit.codeplex.com

By Dominic St-Pierre (Focus Centric inc.)

-----

How to add the visitor chat box to your pages:

LCSK depend on SignalR 2.0.1. The SignalR javascript file are included in the LCSK/js folder as well as the jQuery 1.7.1 which is a dependency of SignalR.

Feel free to move those javascripts file to fit your project's file structure. Also note that those files would be added to your Scripts folder 
since they are dependencies of LCSK. Feel free to use either one and remove the other.

1) On each pages where you want to add the visitor chat box you will have to add the following javascript tags:

  *** If you do not already have jQuery referenced
    <script src="/lcsk/assets/js/jquery-1.7.1.min.js" type="text/javascript"></script>
  ***
    <script src="/lcsk/assets/js/jquery.signalR-2.0.1.min.js" type="text/javascript"></script>
    <script src="/signalr/hubs" type="text/javascript"></script>
    <script src="/LCSK/chat.js" type="text/javascript"></script>

-----

How to log in

- Navigate to the /lcsk/install.html page and assign the admin and agent passwords.

- You can now log in on /lcsk/agent.html using any name and either the admin or agent password.

-----

Troubleshooting

- Make sure you have a reference to jQuery *before the SignalR and LCSK's script tags.

- Make sure the jquery-signalr-[version].min.js matches with 2.0.1.

- Make sure you have write access to the App_Data folder, since the config file "lcsk.dat" will be saved there.