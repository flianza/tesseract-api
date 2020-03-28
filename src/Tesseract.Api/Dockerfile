ARG REPO=mcr.microsoft.com/dotnet/core/runtime
FROM $REPO:3.1-alpine3.10

ENV ASPNETCORE_VERSION 3.1.1

RUN wget -O aspnetcore.tar.gz https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/$ASPNETCORE_VERSION/aspnetcore-runtime-$ASPNETCORE_VERSION-linux-musl-x64.tar.gz \
    && aspnetcore_sha512='865e08a6ca9ad83ad8475ecfe2383b746f5a228b9daaf88e4b5883afa1e50d3209568d57695b02f059e2038d70388d16fd754770fbd5868e9d8a20f88f83604b' \
    && echo "$aspnetcore_sha512 aspnetcore.tar.gz" | sha512sum -c - \
    && tar -zxf aspnetcore.tar.gz -C /usr/share/dotnet ./shared/Microsoft.AspNetCore.App \
    && rm aspnetcore.tar.gz

RUN echo "http://dl-cdn.alpinelinux.org/alpine/edge/testing" >> /etc/apk/repositories && apk update && apk add --no-cache libgdiplus

RUN apk update && apk add --no-cache wget && apk add --no-cache --update tesseract-ocr

WORKDIR /app
ARG source
ENV ASPNETCORE_URLS http://*:7020
ENV ASPNETCORE_ENVIRONMENT=Docker
EXPOSE 7020
COPY ${source:-/build} .
ENTRYPOINT ["dotnet", "Tesseract.Api.dll"]
