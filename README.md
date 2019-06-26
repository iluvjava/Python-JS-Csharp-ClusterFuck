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