FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "Agenda.csproj"

RUN dotnet build "Agenda.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Agenda.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Agenda.dll"]