# Sử dụng hình ảnh .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# Sao chép các file csproj để restore các gói NuGet
COPY *.csproj .
RUN dotnet restore

# Sao chép toàn bộ mã nguồn và build ứng dụng
COPY . .
RUN dotnet publish -c Release -o out

# Sử dụng hình ảnh .NET Runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Thiết lập biến môi trường
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Chạy ứng dụng khi container được khởi chạy
ENTRYPOINT ["dotnet", "JobDemoPrintHelloWhenAtTime.dll"]
