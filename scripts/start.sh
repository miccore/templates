pm2 delete gateway.webapi;dotnet build ../Microservice.WebApi.sln;pm2 start start-gateway.sh --name gateway.webapi;
