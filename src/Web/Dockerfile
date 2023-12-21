ARG NET_IMAGE=8.0

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src
ENV DOTNET_ROLL_FORWARD=LatestMajor
COPY src/ /src/src
WORKDIR /src/src/Web/WebBlazor
ENTRYPOINT [ "dotnet", "run" ]
