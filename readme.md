REDDIT C# API
=================

This is a library to interact with www.reddit.com (or any website which uses reddit - as it's open source), 
from any Mono (coming soon) or  C# / .NET application.  

Current Requirements:  
- .NET 4.0 / VS2010  
- NSOUP, as we parse HTML in some places  
- Newtonsoft JSON.NET  

To get started:  
- Look at the wiki for this repository  
- If you prefer to dive into code, checkout the com.reddit.api.tests project  
  
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



Copyright & License
---------------------

Copyright 2011 PressF12 Pty Ltd

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this work except in compliance with the License.
You may obtain a copy of the License in the LICENSE file, or at:

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.