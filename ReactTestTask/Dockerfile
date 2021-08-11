FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
EXPOSE 80
EXPOSE 443
RUN mkdir /app
WORKDIR /app

COPY ReactTestTask.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app
COPY --from=build-env /app/out .

CMD ["dotnet", "ReactTestTask.dll"]