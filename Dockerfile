FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app
EXPOSE 80

WORKDIR /src
COPY Carsales.sln ./
COPY Application/*.csproj ./Application/
COPY Carsales/*.csproj ./Carsales/
COPY Domain/*.csproj ./Domain/
COPY Repository/*.csproj ./Repository/
COPY CarsalesTest/*.csproj ./CarsalesTest/

RUN dotnet restore

COPY . .
WORKDIR /src/Application
RUN dotnet build -c Release -o /app

WORKDIR /src/Carsales
RUN dotnet build -c Release -o /app

WORKDIR /src/Domain
RUN dotnet build -c Release -o /app

WORKDIR /src/Repository
RUN dotnet build -c Release -o /app

FROM build AS testrunner
WORKDIR /src/CarsalesTest
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM build AS test
WORKDIR /src/CarsalesTest
RUN dotnet test

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Carsales.dll"]