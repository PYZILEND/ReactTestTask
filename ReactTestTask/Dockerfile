FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
RUN mkdir /app
WORKDIR /app

COPY ReactTestTask.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "ReactTestTask.dll"]