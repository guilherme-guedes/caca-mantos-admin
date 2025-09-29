FROM --platform=linux/amd64  mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet publish "CacaMantos.Admin.API/CacaMantos.Admin.API.csproj" -c Release --use-current-runtime --self-contained true  -o build

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:9.0-alpine
# Install Culture prerequisities
RUN apk add --no-cache \
        icu-data-full \
        icu-libs
# enable all cultures - it is set to true in base image
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=build /src/build .

RUN apk add -U tzdata
ENV TZ=America/Sao_Paulo
ENV ASPNETCORE_URLS=http://+:7005
EXPOSE 7005
ENTRYPOINT ["./CacaMantos.Admin.API"]
