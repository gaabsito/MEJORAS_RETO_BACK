# Utilizar la imagen base oficial de .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar el archivo de proyecto y restaurar dependencias
COPY BACK-END.csproj .
RUN dotnet restore

# Copiar el resto de los archivos y construir la aplicación
COPY . .
RUN dotnet publish -c Release -o out

# Imagen base para la ejecución (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar los archivos de publicación desde la etapa de construcción
COPY --from=build /app/out .

# Exponer el puerto en el que la aplicación escuchará
EXPOSE 5000

# Establecer el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "BACK-END.dll"]