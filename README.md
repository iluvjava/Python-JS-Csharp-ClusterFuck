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
    Branch: 
        - Use Restsharp and see if problem 1 persisted, Content-type and Content Disposition were shown in the python's
        request. 
    Problem: 
        2. A mobile page is returned when using the rest client, very weird problem. Yeah, I visited a mobile page 
        on my desktop. 
            - Setting the header's user agent solved the problem. 

```