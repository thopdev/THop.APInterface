# Warning
## this package is still alpha. 
Your free to use this but keep in mind its in alpha. If have you any problems or feature requests make sure to create a issue.

# Thop APInterface
Use your API though a interface

- [GitHub](https://github.com/thopdev/THop.APInterface)
- [Nuget](https://www.nuget.org/packages/THop.APInterface/)


![.NET Core](https://github.com/thopdev/THop.APInterface/workflows/.NET%20Core/badge.svg?branch=master)

## Usage

### Create a interface for your controller (Shared) implemented by the controller (API)
```csharp

    public interface IExampleEndpoint
    {
        [HttpGet]
        Task<object> Get();

        [HttpGet("{id}")]
        Task<object> Get(string id);

        [HttpGet]
        Task<object> Get([FromQuery] int number);

        [HttpPost]
        Task<object> Post([FromBody]object obj);

        [HttpDelete]
        Task Delete();
    }
```
### Add the interface though Dependency injection
```csharp
services.AddAPInterface(typeof(IExampleEndpoint));
```

## Known bugs
Their are currently no known bugs. If you encounter any bugs please submit a issue though the following link.

[Submit bug report](https://github.com/thopdev/THop.APInterface/issues/new?assignees=&labels=&template=bug_report.md&title=)

## Planned features
Currenlty their are no new planned features. If you have any ideas for new features please create a issue. 

[Request new feature](https://github.com/thopdev/THop.APInterface/issues/new?assignees=&labels=&template=feature_request.md&title=)

## Contributers
- [THop](https://github.com/thopdev)
