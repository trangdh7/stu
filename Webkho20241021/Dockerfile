# ===============================
# BUILD STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file .csproj từ thư mục Webkho20241021
COPY Webkho20241021/*.csproj ./Webkho20241021/
RUN dotnet restore Webkho20241021/Webkho20241021.csproj

# Copy toàn bộ source code
COPY Webkho20241021/. ./Webkho20241021/

# Publish ứng dụng
RUN dotnet publish Webkho20241021/Webkho20241021.csproj -c Release -o /app/publish

# ===============================
# RUNTIME STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Webkho20241021.dll"]
