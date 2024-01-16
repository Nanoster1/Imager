ARG NET_IMAGE=8.0

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} as build
WORKDIR /src
ENV DOTNET_ROLL_FORWARD=LatestMajor
COPY src/ /src/src
WORKDIR /src/src/Web/Imager.Web.Client
RUN dotnet publish -c Release -o /app/publish

FROM nginx:1.25.3-alpine
WORKDIR /
COPY src/Web/nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html