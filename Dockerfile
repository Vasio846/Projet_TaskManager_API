# image contenant le SDK .NET 10.0
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# répertoire de travail 
WORKDIR /src

# copier le .csproj vers le répertoire courant (ici /src)
COPY ["TaskManager_API.csproj", "./"]

# restore, télécharge ou résout les dépendances déclarées par le projet 
RUN dotnet restore "TaskManager_API.csproj"

# copie le reste vers /src
COPY . .

# compile et publie l'application 
# -c : utilise la configuration de compilation Release
# -o : place les fichiers dans ce répertoire
# --no-restore : évite de restaurer une seconde fois les dépendances
RUN dotnet publish "TaskManager_API.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# image destinée à l'exécution ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

# copie les ficchiers publiés dans la première étape (build) vers le répertoire courant 
COPY --from=build /app/publish .

# application prévue pour écouter sur le port indiqué
EXPOSE 8080

# définit une variable d'environnement dans le container pour configurer le port HTTP d'écoute
ENV ASPNETCORE_HTTP_PORTS=8080

# commande exécutée lorsque le container démarre 
ENTRYPOINT ["dotnet", "TaskManager_API.dll"]
