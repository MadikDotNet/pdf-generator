FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY building-blocks/PdfGenerator.FileStorage/PdfGenerator.FileStorage.csproj building-blocks/PdfGenerator.FileStorage/
COPY building-blocks/PdfGenerator.Observability/PdfGenerator.Observability.csproj building-blocks/PdfGenerator.Observability/
COPY building-blocks/PdfGenerator.Shared/PdfGenerator.Shared.csproj building-blocks/PdfGenerator.Shared/

COPY web-server/PdfGenerator.Domain/PdfGenerator.Domain.csproj web-server/PdfGenerator.Domain/
COPY web-server/PdfGenerator.Application/PdfGenerator.Application.csproj web-server/PdfGenerator.Application/
COPY web-server/PdfGenerator.Infrastructure/PdfGenerator.Infrastructure.csproj web-server/PdfGenerator.Infrastructure/
COPY web-server/PdfGenerator.Infrastructure.DataAccess/PdfGenerator.Infrastructure.DataAccess.csproj web-server/PdfGenerator.Infrastructure.DataAccess/

COPY background-worker/PdfGenerator.Worker/PdfGenerator.Worker.csproj background-worker/PdfGenerator.Worker/

RUN dotnet restore "background-worker/PdfGenerator.Worker/PdfGenerator.Worker.csproj"

COPY building-blocks building-blocks
COPY web-server/PdfGenerator.Domain web-server/PdfGenerator.Domain
COPY web-server/PdfGenerator.Application web-server/PdfGenerator.Application
COPY web-server/PdfGenerator.Infrastructure web-server/PdfGenerator.Infrastructure
COPY web-server/PdfGenerator.Infrastructure.DataAccess web-server/PdfGenerator.Infrastructure.DataAccess
COPY background-worker background-worker

WORKDIR "background-worker/PdfGenerator.Worker"
RUN dotnet build "PdfGenerator.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PdfGenerator.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update && apt-get -f install && apt-get -y install wget gnupg2 apt-utils

RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | gpg --dearmor -o /usr/share/keyrings/google-archive-keyring.gpg \
    && echo "deb [arch=amd64 signed-by=/usr/share/keyrings/google-archive-keyring.gpg] http://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google.list \
    && apt-get update \
    && apt-get install -y google-chrome-unstable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst fonts-freefont-ttf \
      --no-install-recommends \
    && rm -rf /var/lib/apt/lists/*

ENV PUPPETEER_EXECUTABLE_PATH "/usr/bin/google-chrome-unstable"

ENTRYPOINT ["dotnet", "PdfGenerator.Worker.dll"]
