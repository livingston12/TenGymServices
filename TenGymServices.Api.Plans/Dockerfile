FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 6002

ENV ASPNETCORE_URLS=http://+:6002

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["TenGymServices.Api.Plans/TenGymServices.Api.Plans.csproj", "TenGymServices.Api.Plans/"]
RUN dotnet restore "TenGymServices.Api.Plans/TenGymServices.Api.Plans.csproj"
COPY . .
WORKDIR "/src/TenGymServices.Api.Plans"
RUN dotnet build "TenGymServices.Api.Plans.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TenGymServices.Api.Plans.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TenGymServices.Api.Plans.dll"]
