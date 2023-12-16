ARG NET_IMAGE=8.0

FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE} AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src
ENV DOTNET_ROLL_FORWARD=LatestMajor
COPY src/ /src/src
WORKDIR /src/src/ApiGateways/Aggregators/ImageResizerAggregator/Imager.ImageResizerAggregator.Server
RUN dotnet restore

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imager.ImageResizerAggregator.Server.dll"]