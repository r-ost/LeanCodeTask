FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["LeanCodeTask.WebApi/LeanCodeTask.WebApi.csproj", "LeanCodeTask.WebApi/"]
RUN dotnet restore "LeanCodeTask.WebApi/LeanCodeTask.WebApi.csproj"

COPY . .
WORKDIR "/LeanCodeTask.WebApi"
RUN dotnet build "LeanCodeTask.WebApi.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "LeanCodeTask.WebApi.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeanCodeTask.WebApi.dll"]