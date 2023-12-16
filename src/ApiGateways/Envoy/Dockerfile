FROM envoyproxy/envoy:v1.28.0
WORKDIR /

COPY src/ApiGateways/Envoy/envoy.yaml /tmpl/envoy.yaml.tmpl
COPY src/ApiGateways/Envoy/docker-entrypoint.ps1 / 
COPY build/scripts/InstallPowerShell.sh /

RUN chmod 500 /docker-entrypoint.ps1 && \
    chmod 500 /InstallPowerShell.sh

RUN apt-get update && \
    apt-get install gettext -y && \
    /InstallPowerShell.sh

ENTRYPOINT ["pwsh", "-f", "/docker-entrypoint.ps1"]
