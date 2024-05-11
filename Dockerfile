FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5090



FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MvcDocuments.Web/MvcDocuments.Web.csproj", "MvcDocuments.Web/"]
COPY ["MvcDocuments.Application/MvcDocuments.Application.csproj", "MvcDocuments.Application/"]
COPY ["MvcDocuments.Domain/MvcDocuments.Domain.csproj", "MvcDocuments.Domain/"]
COPY ["MvcDocuments.Infrastructure/MvcDocuments.Infrastructure.csproj", "MvcDocuments.Infrastructure/"]
RUN dotnet restore "MvcDocuments.Web/MvcDocuments.Web.csproj"
COPY . .
WORKDIR "/src/MvcDocuments.Web"
RUN dotnet build "MvcDocuments.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MvcDocuments.Web.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MvcDocuments.Web.dll"]
