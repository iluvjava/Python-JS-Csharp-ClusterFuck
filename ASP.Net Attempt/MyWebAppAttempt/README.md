# Asp.Net MVC #
- It's a web framework
- It's a subset of the asp.net core. 
## This is a trivial exmaple of a basic ASP.NET MVC Project ##
- Connect the front end view to the backend database
- mysql databse. 


## Model ##
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

### Controller ###

## Reference:  ##
- https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/?view=aspnetcore-2.2
    - This is not asp.net core, it's different, I know, but the MVC and razor 
    should be presented under both framework. 