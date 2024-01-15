#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jira.Query/JiraSample.Query.Api/JiraSample.Query.Api.csproj", "src/Jira.Query/JiraSample.Query.Api/"]
COPY ["src/Jira.Query/JiraSample.Query.Application/JiraSample.Query.Application.csproj", "src/Jira.Query/JiraSample.Query.Application/"]
COPY ["src/Jira.Common/JiraSample.Common/JiraSample.Common.csproj", "src/Jira.Common/JiraSample.Common/"]
COPY ["src/Jira.Query/JiraSample.Query.Domain/JiraSample.Query.Domain.csproj", "src/Jira.Query/JiraSample.Query.Domain/"]
COPY ["src/Jira.Query/JiraSample.Query.Infrastructure/JiraSample.Query.Infrastructure.csproj", "src/Jira.Query/JiraSample.Query.Infrastructure/"]
RUN dotnet restore "./src/Jira.Query/JiraSample.Query.Api/./JiraSample.Query.Api.csproj"
COPY . .
WORKDIR "/src/src/Jira.Query/JiraSample.Query.Api"
RUN dotnet build "./JiraSample.Query.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JiraSample.Query.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JiraSample.Query.Api.dll"]