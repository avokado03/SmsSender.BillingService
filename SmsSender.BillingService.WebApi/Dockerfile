#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SmsSender.BillingService.WebApi/SmsSender.BillingService.WebApi.csproj", "SmsSender.BillingService.WebApi/"]
COPY ["SmsSender.BillingService.CQRS/SmsSender.BillingService.CQRS.csproj", "SmsSender.BillingService.CQRS/"]
COPY ["SmsSender.BillingService.Domain/SmsSender.BillingService.Data.csproj", "SmsSender.BillingService.Domain/"]
RUN dotnet restore "SmsSender.BillingService.WebApi/SmsSender.BillingService.WebApi.csproj"
COPY . .
WORKDIR "/src/SmsSender.BillingService.WebApi"
RUN dotnet build "SmsSender.BillingService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmsSender.BillingService.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmsSender.BillingService.WebApi.dll"]