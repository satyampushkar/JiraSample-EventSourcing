#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jira.Command/JiraSample.Command.Api/JiraSample.Command.Api.csproj", "src/Jira.Command/JiraSample.Command.Api/"]
COPY ["src/Jira.Command/JiraSample.Command.Application/JiraSample.Command.Application.csproj", "src/Jira.Command/JiraSample.Command.Application/"]
COPY ["src/Jira.Command/JiraSample.Command.Domain/JiraSample.Command.Domain.csproj", "src/Jira.Command/JiraSample.Command.Domain/"]
COPY ["src/Jira.Common/JiraSample.Common/JiraSample.Common.csproj", "src/Jira.Common/JiraSample.Common/"]
COPY ["src/Jira.Command/JiraSample.Command.Infrastructure/JiraSample.Command.Infrastructure.csproj", "src/Jira.Command/JiraSample.Command.Infrastructure/"]
RUN dotnet restore "./src/Jira.Command/JiraSample.Command.Api/./JiraSample.Command.Api.csproj"
COPY . .
WORKDIR "/src/src/Jira.Command/JiraSample.Command.Api"
RUN dotnet build "./JiraSample.Command.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JiraSample.Command.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JiraSample.Command.Api.dll"]