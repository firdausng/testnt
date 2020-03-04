#reference https://espressocoder.com/2020/02/05/adding-docker-to-the-asp-net-core-angular-template/

FROM node:12.16.0 AS client

EXPOSE 4200 49153
USER node
RUN mkdir /home/node/.npm-global
ENV PATH=/home/node/.npm-global/bin:$PATH
ENV NPM_CONFIG_PREFIX=/home/node/.npm-global

RUN npm install -g @angular/cli

WORKDIR /app
CMD ["ng", "serve", "--port", "4200", "--host", "0.0.0.0", "--disable-host-check", "--poll", "2000"]

#COPY ["src/Testnt.Main.Api.Rest/ClientApp/package.json", "."]
#RUN npm install
#COPY ["src/Testnt.Main.Api.Rest/ClientApp", "."]
#RUN npm run build
#COPY ["src/Testnt.Main.Api.Rest/ClientApp/dist/ClientApp/", "dist/"]

