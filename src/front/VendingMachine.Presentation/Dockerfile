#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/front/VendingMachine.Presentation/Presentation.csproj", "src/front/VendingMachine.Presentation/"]
COPY ["src/back/VendingMachine.Infrastructure/Infrastructure.csproj", "src/back/VendingMachine.Infrastructure/"]
COPY ["src/back/VendingMachine.Application/Application.csproj", "src/back/VendingMachine.Application/"]
COPY ["src/back/VendingMachine.Domain/Domain.csproj", "src/back/VendingMachine.Domain/"]
RUN dotnet restore "src/front/VendingMachine.Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/src/front/VendingMachine.Presentation"
RUN dotnet build "Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
