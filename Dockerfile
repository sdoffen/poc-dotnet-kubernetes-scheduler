FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
ARG ENVIRONMENT=Development
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src

# Copy the contents of the ./src directory
COPY ./src ./

# Restore packages
RUN dotnet restore

FROM restore AS publish
RUN dotnet publish ./Scheduler.Api -c Release -o /app/publish --no-restore

# Build runtime image
FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT [ "dotnet", "Scheduler.Api.dll" ]