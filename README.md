# Blazor in Action

In this repo, I follow the book [Blazor in Action](https://www.manning.com/books/blazor-in-action) by Chris Santy. I really enjoy using my Manning subscription to get my feet wet with stuff I have not the time to use on a regular basis on my job.

## Differences with .NET 10

There are likely to be some minor (and hopefully few major) differences to the book version when using .NET 10 to implement the examples. I'd like to have them documented here.


### Appendix: Adding an ASP.NET Core Web API

When we create the webapi project, we need to tell that we want to use controllers, 
as the default has changed to start with a minimal api: `dotnet new webapi --use-controllers ...`

Configure the Project to support a Blazor WebAssembly client: https://www.nuget.org/packages/Microsoft.AspNetCore.Components.WebAssembly.Server


## EditContext

We have access to the EditContext and can:
- perform actions manually like triggering validation
- hook into events like `OnFieldChanged`
- plug in a custom CSS class provider