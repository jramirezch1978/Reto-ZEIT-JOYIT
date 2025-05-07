# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto y restaurar dependencias
COPY ["CleanArchitecture.API/CleanArchitecture.API.csproj", "CleanArchitecture.API/"]
COPY ["CleanArchitecture.Application/CleanArchitecture.Application.csproj", "CleanArchitecture.Application/"]
COPY ["CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "CleanArchitecture.Domain/"]
COPY ["CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "CleanArchitecture.Infrastructure/"]
RUN dotnet restore "CleanArchitecture.API/CleanArchitecture.API.csproj"

# Copiar el resto del código fuente
COPY . .

# Compilar la aplicación
RUN dotnet build "CleanArchitecture.API/CleanArchitecture.API.csproj" -c Release -o /app/build

# Publicar la aplicación
RUN dotnet publish "CleanArchitecture.API/CleanArchitecture.API.csproj" -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Exponer los puertos que usa la aplicación
EXPOSE 7000

# Establecer la variable de entorno para los puertos
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:7000

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "CleanArchitecture.API.dll"] 