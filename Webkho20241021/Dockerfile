# ===============================
# BUILD STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file csproj và restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ source code
COPY . .

# Build và publish project ra thư mục /app/publish
RUN dotnet publish -c Release -o /app/publish

# ===============================
# RUNTIME STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Webkho20241021.dll"]
