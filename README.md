<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/marlonajgayle/Net6WebApiTemplate/develop/src/Content/.template.config/icon.png" />
...
# .NET 6 GoatEdu

This is a solution dotnet goatedu for developing an enterprise-level Web API with.NET 6 ASP.NET Core, following Clean Architecture principles and API best practices.
The .NET 6 Web Api consist of scafolding for API versioning, Repository Pattern, email, Unit Of work, logging, IP rate limiting, JWT, Open API, validation, postgresql and more ...

## Table of Contents
* [Prerequisites](#Prerequisites)
* [Instructions](#Instructions)
* [Contributions](#Contributions)
* [Credits](#Credits)


## Prerequisites
You will need the following tools:
* [Visual Studio Code or Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (version 17.0.0 Preview 7.0 or later)
* [.NET Core SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)

## Instructions
1. Install the latest [.NET Core 6 SDK](https://dotnet.microsoft.com/download). 
2. Run `git clone https://github.com/huscongao1692003/SWD_GoatEdu_Backend.git` to install the project
3. Then navigate to the location you would like to create to open


### Docker Setup
ASP.NET Core Web API uses HTTPS and relies on certificates for trust, identity and encryption. 
To run Net5WebTemplate application Docker over HTTPS during development do the following:
1. Generate certificate using 'dotnet dev-certs' (for localhost use Only!).

Note: Update the docker-compose file with dev-cert password used.

On Windows using Linux Containers
```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx  -p your_password
dotnet dev-certs https --trust
````
When using PowerShell, replace %USERPROFILE% with $env:USERPROFILE.

On macOS or Linux
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p { password here }
dotnet dev-certs https --trust
```
2. Build and run Docker containers run Docker compose located in the solution directory
```
docker-compose -f 'docker-compose.yml' up --build
```

### Database Setup
To setup the SQL Server database following the instrcutions below:
Update db: 

Add migration: dotnet ef migrations add AddUserWalletOneToOne -p GoatEdu.Infrastructure -s GoatEdu.API

Update db : dotnet ef database update -p GoatEdu.Infrastructure -s GoatEdu.API


dotnet ef dbcontext scaffold "Host=****;Port=5432;Username=root;Password=Admin123456789@;Database=goateduprimary;" Npgsql.EntityFrameworkCore.PostgreSQL -o ../GoatEdu.Core/Models --context-dir ../GoatEdu.Infrastructure/Data --context GoatEduContext --data-annotations

## Contributions
- [ThanhNguyen](https://github.com/huscongao1692003) - Implemented API endpoints, Unit and Integration tests.
- [Khang](https://github.com/b3os) - .

## Credits
This solution's structure was heavily infuenced by [Thanh Nguyen's](https://github.com/jasontaylordev) Clean Architecture model.
Icon made by [Flat Icons](https://www.flaticon.com/authors/flat-icons) from [www.flaticon.com](https://www.flaticon.com/)


## Versions
The [main](https://github.com/marlonajgayle/Net6WebApiTemplate/main) branch is running .NET 6.0

## License
This project is licensed under the MIT License - see the [LICENSE.md]() [main]() branch is running .NET 6.0
file for details.

## Documented
How to use stripe payment in fe:
First you need to send request to stripe to get tokenId with published key with card <img width="448" alt="image" src="https://github.com/huscongao1692003/SWD_GoatEdu_Backend/assets/72685335/119e00bf-c0cf-4584-8ca7-faa772be10d0">  <img width="419" alt="image" src="https://github.com/huscongao1692003/SWD_GoatEdu_Backend/assets/72685335/54df3ed1-50fd-4418-98f1-143c593939e4"> 
<img width="642" alt="image" src="https://github.com/huscongao1692003/SWD_GoatEdu_Backend/assets/72685335/71170c04-e45c-4e5d-a765-2e7a7a82b825">
this is example in js file. then use that tokenId , send the request to backend /api/stripe/customer with fomat 
{
  "stripeToken": "tok_1EHT7e2eZvKYlo2CljWykUEn",
  "email": "john.doe@example.com",
  "name": "John Doe"
}
then the response will have customerId.
<img width="350" alt="image" src="https://github.com/huscongao1692003/SWD_GoatEdu_Backend/assets/72685335/fa94d858-4152-4847-a821-5deb30a5d9f4">
save that for later request, for the charge payment.
If user pay some things call the api/stripe/charge and add that customerId to it, by fomat <img width="216" alt="image" src="https://github.com/huscongao1692003/SWD_GoatEdu_Backend/assets/72685335/049d8ed3-a6b7-4577-9e38-7afc6dad2ac2">
the backend will automatic save your transtraction.




