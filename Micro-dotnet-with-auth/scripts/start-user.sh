dotnet restore ../User.Microservice/User.Microservice.csproj;dotnet publish ../User.Microservice/User.Microservice.csproj -c Release;dotnet ../User.Microservice/bin/Release/netcoreapp3.1/User.Microservice.dll --urls "http://localhost:44373";