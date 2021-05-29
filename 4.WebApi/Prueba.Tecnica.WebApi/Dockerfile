FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["4.WebApi/Prueba.Tecnica.WebApi/Prueba.Tecnica.WebApi.csproj", "4.WebApi/Prueba.Tecnica.WebApi/"]
COPY ["1.Domain/Prueba.Tecnica.Domain.Entities/Prueba.Tecnica.Domain.Entities.csproj", "1.Domain/Prueba.Tecnica.Domain.Entities/"]
COPY ["2.Infraestructure/Prueba.Tecnica.Infra.IoC/Prueba.Tecnica.Infra.IoC.csproj", "2.Infraestructure/Prueba.Tecnica.Infra.IoC/"]
COPY ["3.Application/Prueba.Tecnica.Application.Services/Prueba.Tecnica.Application.Services.csproj", "3.Application/Prueba.Tecnica.Application.Services/"]
COPY ["3.Application/Prueba.Tecnica.Application.Contracts/Prueba.Tecnica.Application.Contracts.csproj", "3.Application/Prueba.Tecnica.Application.Contracts/"]
COPY ["3.Application/Prueba.Tecnica.Application.Interfaces/Prueba.Tecnica.Application.Interfaces.csproj", "3.Application/Prueba.Tecnica.Application.Interfaces/"]
COPY ["1.Domain/Prueba.Tecnica.Domain.Services/Prueba.Tecnica.Domain.Services.csproj", "1.Domain/Prueba.Tecnica.Domain.Services/"]
COPY ["1.Domain/Prueba.Tecnica.Domain.Interfaces/Prueba.Tecnica.Domain.Interfaces.csproj", "1.Domain/Prueba.Tecnica.Domain.Interfaces/"]
COPY ["2.Infraestructure/Prueba.Tecnica.Infra.Data/Prueba.Tecnica.Infra.Data.csproj", "2.Infraestructure/Prueba.Tecnica.Infra.Data/"]
RUN dotnet restore "4.WebApi/Prueba.Tecnica.WebApi/Prueba.Tecnica.WebApi.csproj"
COPY . .
WORKDIR "/src/4.WebApi/Prueba.Tecnica.WebApi"
RUN dotnet build "Prueba.Tecnica.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Prueba.Tecnica.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Prueba.Tecnica.WebApi.dll"]