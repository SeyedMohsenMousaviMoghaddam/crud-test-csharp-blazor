#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mc2.CrudTest/Mc2.CrudTest.Presentation/Client/Mc2.CrudTest.Presentation.Client.csproj", "Mc2.CrudTest/Mc2.CrudTest.Presentation.Client/"]
COPY ["Mc2.CrudTest/Mc2.CrudTest.Presentation/Shared/Mc2.CrudTest.Presentation.Shared.csproj", "Mc2.CrudTest/Mc2.CrudTest.Presentation.Shared/"]
COPY ["Mc2.CrudTest/Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj", "Mc2.CrudTest/Mc2.CrudTest.Presentation.Server/"]
COPY ["nuget.config","nuget.config"]
COPY Mc2.CrudTest.sln .
RUN dotnet restore "Mc2.CrudTest/Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj"
COPY . .
WORKDIR "/src/Mc2.CrudTest"
RUN dotnet build "Mc2.CrudTest.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Presentation.Server.dll"]