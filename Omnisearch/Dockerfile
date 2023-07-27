FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Omnisearch/Omnisearch.csproj", "Omnisearch/"]
RUN dotnet restore "Omnisearch/Omnisearch.csproj"
COPY . .
WORKDIR "/src/Omnisearch"
RUN dotnet build "Omnisearch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Omnisearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Omnisearch.dll"]
