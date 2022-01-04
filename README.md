# Carpark Avaialbility Checking System API 

## Information 

```bash
Proxy server to obtain carpark availability data in Singapore, using C#, ASP.NET Core
, Entity Framework and MySQL

Objective: 
- Familiarize with Repository-Service Pattern
- Backend MVC 
- Role of Proxy server 
- Microsoft stack
```
## Setup


```bash
On PowerShell 

cd carpark-availability-checking-system

cd CarparkAvailabilityCheckingSystem

#To run the application 
dotnet run 

```
# Swagger UI 

[http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

# API Links

### Registration 
`POST` /api​/users​/registration

### Login 
`POST` /api​/users​/login

### Get Member Details - Protected
`GET` /api​/users​/getMembersDetail

### Get Carpark Availability - Protected
`GET` /api/carpark/getCarparkAvailability

