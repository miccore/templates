pm2 delete User.microservice;pm2 delete gateway.webapi;dotnet build ../Microservice.WebApi.sln;pm2 start start-user.sh --name User.microservice;pm2 start start-gateway.sh --name gateway.webapi;