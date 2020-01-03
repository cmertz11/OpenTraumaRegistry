# Open Trauma Registry
  Trauma Center verification by the American College of Surgeons (ACS) is considered the gold standard. One ACS requirement is a trauma registry. This registry houses all data regarding the care and outcomes of injured patients. This data is used to aid in the evaluation of care, development of performance improvement and to guide injury prevention activities. Trauma data from ACS verified centers is used collectively to document the number and type of injuries occurring annually for the National Highway Safety Council and the Centers for Disease Control. 
  
## Goals
The goal of this project initally started as an avenue to learn the following technologies. 
- Git and GitHub / https://github.com/
- .NET Core / https://dotnet.microsoft.com/
- Blazor / https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor
- NSwagStudio / https://github.com/RicoSuter/NSwag/wiki/NSwagStudio
- Docker / https://www.docker.com/
- EntityFrameworkCore / https://github.com/aspnet/EntityFrameworkCore
## Notable links
- http://www.traumavendoralliance.org/

## Demo 
- Coming Soon! https://opentraumaregistry.com/

## Screen Shots
![image](https://user-images.githubusercontent.com/5183421/71624887-d7a04780-2bb2-11ea-9716-6f1661ccd7b1.png)
![image](https://user-images.githubusercontent.com/5183421/71624963-39f94800-2bb3-11ea-8114-49411fd54159.png)

## Projects used and or referenced
- [Blazor](https://blazor.net)
- [Blazorboilerplate](https://github.com/enkodellc/blazorboilerplate)
- [MatBlazor](https://github.com/SamProf/MatBlazor)
- [Blazored/LocalStorage](https://github.com/Blazored/LocalStorage)
- NSwagStudio (https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)

## Prerequisites
- [.Net Core SDK 3.1.0-preview4.19579.2](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- Install the Latest Visual Studio 2019 Preview with the ASP.NET and web development workload selected.
- Entity Framework Core on the command-line tools: **dotnet tool install --global dotnet-ef --version 3.1.0-preview4.19579.2**
- NSwagStudio (https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)

### How to run
 I'm currently working on a step by step set of instructions to get Open Trauma Registry (OTR) up and running.
 - Clone or Download
 - Ensure the Solution properties are set to Multiple startup project with OpentTraumaRegistry.Api starting first then OpenTruamaRegisty.UI.MD
 ![image](https://user-images.githubusercontent.com/5183421/71628673-0aecd180-2bc7-11ea-90ef-fdaec734ed12.png)

- Create the database and prepopulate with test data 
  The appsettings.json is defaulting to use sqlserver. OTR supports sqlserver, Postgresql, and MySql. Testing is incomplete this point and some funtionality may not be availabe in all but sqlserver.  
 - If you have a PostgreSql, or MySql instance you would rather use please ensure you update the connectionstring password.
  
Right click OpenTraumaRegistry.Data --> debug  --> start new instance this will create the database:
![image](https://user-images.githubusercontent.com/5183421/71628852-3a500e00-2bc8-11ea-9a94-89d3d4671fcf.png)

 
