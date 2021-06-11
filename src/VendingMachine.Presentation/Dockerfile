#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/VendingMachine.Presentation/Presentation.csproj", "src/VendingMachine.Presentation/"]
COPY ["src/VendingMachine.Infrastructure/Infrastructure.csproj", "src/VendingMachine.Infrastructure/"]
COPY ["src/VendingMachine.Application/Application.csproj", "src/VendingMachine.Application/"]
COPY ["src/VendingMachine.Domain/Domain.csproj", "src/VendingMachine.Domain/"]
RUN dotnet restore "src/VendingMachine.Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/src/VendingMachine.Presentation"
RUN dotnet build "Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
