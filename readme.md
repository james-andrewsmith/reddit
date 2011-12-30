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
    <add key="moderated-subreddit" value="subreddit-you-moderate" />    
  </appSettings>
</configuration>

```

To ensure all unit tests work correctly, ensure the username/password provided is a modorator on the 'moderated-subreddit',
and this subreddit has at least 1 post, and has at least 1 flair user configured.

To understand the API more have a look at https://github.com/reddit/reddit/wiki/API.