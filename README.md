# FunWith-API-HTTP-HTML-and-C-
Just for fun. It's either this or math analysis textbook.
# What is this: 
Python. C# and Node js. 

# Developer Log: 17/06/2019:
```
    Question: 
        Does C# httpclient save cookies automatically from the response header of the request? 
            - Yeah, the cookies is saved under the HttpClientHandler that used to initiate the HttpClient. It auto sets
            cookies if the given response has "set cookies" properties in the header. 
        What are the list of Request Properties httpclinet supports? 
            - By default, there is no properties in the HttpClientHandler.
    Problem: 
        1. Content-Type and Content Disposition headers were missing in the response headers from DeviantArt domain, 
            - No solution, cause unknown. 
    Progress: 
        Code Refractoring. 
    Branch out: 
        - Use Restsharp and see if problem 1 persisted, Content-type and Content Disposition were shown in the python's
        request. 
    Problem: 
        2. A mobile page is returned when using the rest client, very weird problem. Yeah, I visited a mobile page 
        on my desktop. 
            - Setting the header's user agent solved the problem. 

```

# 18/06/2019: 
```
    Progress: 
        Sync GET and POST request method in Namespace RestSharpAttempt are completed and seems to work. 
```

# 20/06/2019:
```
    Problem: 
        The CsQuery doesn't have a way to get the innertext for a DOM node, lacking of similar functionalities is a problem. 
            - Solution: AngulSharp
```

# 21/062019: 
```
    Problem:
        Sometimes the client is associated with the correct User-Agent header, sometimes it's not, 
        this is a very mysterious bug. 
        - Fix Attempt: Set the user agent properties in the Instance of RequestClient 
        object instead of adding then to the headers of the Irequest. (failed)
        - Fix Attempt: Set the accept property in header to be: "*/*" (Success 75% sure)
        - Fix Attempt: Set the headers using delagate, set the header completely the 
        same from the code provided by postman. (95% Worked)
            - Problem: Sometimes, once in a blue moon, I got to the client html. 
    Question: 
        - Why does keeping the same header key "Postman-token" seems to work? 
            - Don't know, is this magic? 
```
# 22/062019: 
```
    Action: 
        Abandon Deviantart scraping project. 
    Action: 
        Testing API class and make requests to derpibooru
    Tools: 
        Json.Net framework. 
```

# 25/06/2019: 
```
    Action: 
        - Add different way of encoding the GET request parameters for the MyLittleResclient 
        class. (Done but not tested yet)
    Action: 
        - Add support for GET with query string too. (Done but not tested yet.)
    Action: 
        - Chose a bunch of source codes that is actually working and make them 
        into a library. 
```

# 30/06/2019: 
```
    Progress: 
        Added gitignore to so that I can work on different places and different computers. 
    Progress: 
        Copied and paste XML serialization (MyLittleXML) from my previous project. 
    Progress: 
        Get request no support different modes for encoding the paramters. 
    Learned: 
        metadata is in the html in the syntax of "{{variablename}}", 
        control block and structure are in the syntax of {% ???? %}. 
        This is the Jinga that python flask is using. 
    Oh my god: 
        - Python is creepy in many ways, and quirky too. I have run into 
        problem just because I run the srcipt in a different directory than before. 

        - jinja is also running script that has it own syntax and grammar 
        outside of the python script, which is quirky. I don't like that one 
        bit, but it's probably for the best because python slow. 

        - Furthermore, url_for('static', filename='{{varname}}') is not good 
        because the {{varname}} is interpreted literally. 

        - Low level stuff is handled by python, there is completely no need 
        to worry about uri encoding, which is... pretty cool! 
```

# 02/07/2019: 
## What I learned: 
- Making a connection to a MYSQL database, execute the command, and then read out the stuff. 
- Major components:
    - MySql.Data.MysqlClient from nuget. 
    - server url; servername, database name, urserid, password; (I wonder why port is not here...)
    - MySqlConnectionStringBuilder
    - MySqlConnection
    - MysqlCommand
    - MySqlDataReader
- Procedures For doing this shit: 
    - Make a connection string
    - Prepare a connection
    - Prepare a sql command 
        - Give the connection to the sqlcommand
        - Add parameters for your commands
    - Execute the command object and it will return a MySqlDataReader
    - The data reader is an iterable, it's a list of map. 
- How to make the process painless? 
    - Store the configs as xml first. 
    - Put all the query together, minimize the number of times connection is used. 
    - Use OOP programming paradigm and Functional paradigms. 
    - Run tests. 
    - Package everything and make it into a library. 


#09/07/2019: 
## What I leared: 
- Post reponse with Json type. 
    ```
    js = json.dumps(parameters) # construct the json response.
    resp = Response(js, status= 200, mimetype="application/json")
    return resp  # the client want the returned object. 

    request.args # parameters for the get requests. 
    url_for("function_routehandle", key=vals) 
        - when call under the context of executing scirpt: 
            - if parameters is part of the url, it will be ther
            - if not,
                - the paramters will be in the key and val of the request to the rounted endpoint.
    ```
# 31/07/2019: 
- Parser, Twiexact, OptBalance validation. 


# Major Referece Materials
- The Python Flask: 
    - https://github.com/CoreyMSchafer/code_snippets/tree/master/Python/Flask_Blog
- The SQL Connection: 
    - https://github.com/microsoft/sql-server-samples/tree/master/samples/tutorials
- C# Windows MYSQL Command Object: 
    - https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html
- C# ASP.NET MVC (Slow)
    - https://www.youtube.com/watch?v=phyV-OQNeRM
    - https://www.youtube.com/watch?v=bIiEv__QNxw