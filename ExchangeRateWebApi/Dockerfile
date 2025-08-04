FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /exchangewebapi

EXPOSE 80
EXPOSE 5039

COPY ./*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /exchangewebapi

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /exchangewebapi
COPY --from=build /exchangewebapi/out ./
ENTRYPOINT ["dotnet", "ExchangeRateWebApi.dll"]