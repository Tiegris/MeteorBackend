FROM nginx:1.19.0-alpine AS base
WORKDIR /app
EXPOSE 80
COPY ["Web/nginx.conf", "/etc/nginx/nginx.conf"]

FROM base AS final
WORKDIR /app
COPY ["Web/Client", "/app/wwwroot"]
