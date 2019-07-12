# Asp.Net MVC #
- It's a web framework
- It's a subset of the asp.net core. 
## This is a trivial exmaple of a basic ASP.NET MVC Project ##
- Connect the front end view to the backend database
- mysql databse. 



- Classes that models the data
- Validation Logic and stuff like that. 

```
[Display(Name - "Employee ID")]
[Range(100000, 99999, ErrorMessage="You need to enter a valid EmployeedId")]

[Required(ErrorMessage= "")]

[Compare("EmailAddress", ErrorMessage="The Email and COnfirm Eail must match.")]

[Required(ErrorMessage = "PW is required. ")]
[DataType(DataType.Password)]
[StringLength(100, MinimumLength = 10)]
```
This solution is natively well supported by the framework. 
## Controller ##

#### Routing ####
- Configgured in Startup.cs file. 
- This is the rounting logic: 
    - /[Contoller]/[ActionName]/[Parameters] <=> /[Name of the controller class]
/[Name of the method inside the controller]/[parameters]
    - 
- Default route: 
    - Home controller under index method. 
    - Index method will be called when action name is not specified.
#### Parameters ####
```
    // ./HellowWorld/Welcome?name=rick&numtimes=4
    public string Welcome(string name, int numtimes =1)
    {
        return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        // Encode prevent from code injections
    }
```
* The GET query string is never part of the {id?} defined in the startup.cs *

```
// Controller that forward a url with id. 
public string Welcome(string name, int ID = 1)
{
    return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
}
```
- url: https://localhost:xxx/HelloWorld/Welcome/3?name=Rick

## Views ##
- Right click on Views folder to add new modules for views. 
    - MVC razor view 
- All the template of views added are inside the @RenderBody() in _Layout.cshtml

### Transferring data using ViewData Dictionary ###
```
// On the controller: 
ViewData["Message"] = "Hello " + name;
ViewData["NumTimes"] = numTimes;

// on the cs html template:
 @for (int i = 0; i < (int)ViewData["NumTimes"]; i++)
{
    <li>@ViewData["Message"]</li>
}
```


## Model ##
- Right click on Modles folder to add models
- Scaffold the model class
    - Controller, add New Scaffolded Item
    - Add your class as new data context type. 
    - An entity frameworks will be created. 
### Entity Framework ###
- The things that sticks the model class you wrote with the databse. 
- you need to do a "Initial Migration" to start the process, described here: [link]
(https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-2.2&tabs=visual\
-studio#pmc)
- Working with mysql and entity framework: 
     - [link](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html)

## Reference:  ##
- https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/?view=aspnetcore-2.2
    - This is not asp.net core, it's different, I know, but the MVC and razor 
    should be presented under both framework. 