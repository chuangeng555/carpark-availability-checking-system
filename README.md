# Carpark Avaialbility Checking System API ## Setup```bashOn PowerShell cd carpark-availability-checking-systemcd CarparkAvailabilityCheckingSystem#To run the application dotnet run ```# Swagger UI [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)# API Links### Registration `POST` /api​/users​/registration### Login `POST` /api​/users​/login### Get Member Details - Protected`GET` /api​/users​/getMembersDetail### Get Carpark Availability - Protected`GET` /api/carpark/getCarparkAvailability