FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /exchangeconsoleapp

COPY ./*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /exchangeconsoleapp

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /exchangeconsoleapp
COPY --from=build /exchangeconsoleapp/out ./
ENTRYPOINT ["dotnet", "ExchangeRateComparator.dll"]