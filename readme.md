REDDIT C# API
=================

This API is a client for http://www.reddit.com

To get started -- have a look at the unit tests in com.reddit.tests, make sure you create an app.config 
file with the following keys setup.

``` xml

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="username" value="your-username" />
    <add key="password" value="your-password" />
    <add key="second-username" value="your-second-username" />
    <add key="second-password" value="your-second-password" />
    <add key="moderated-subreddit" value="subreddit-you-moderate" />    
  </appSettings>
</configuration>

```

To ensure all unit tests work correctly, ensure there are mutliple username/password 
combinations provided, the first should be a modorator on the 'moderated-subreddit',
and this subreddit has at least 1 post, and has at least 1 flair user configured.

The second user is for testing banning / messaging functionality. I would suggest that 
both users be user exclusively for testing unless you're very familar with what these 
tests do.

To understand the API more have a look at https://github.com/reddit/reddit/wiki/API.