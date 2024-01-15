FROM node:lts-alpine
# ENV NODE_ENV=production
WORKDIR /src/Jira.Webapp
# COPY ["./package.json", "./package-lock.json*", "npm-shrinkwrap.json*", "./"]
COPY package.json .
# COPY *package.json /usr/src/app/package.json
# COPY *package-lock.json /usr/src/app/package-lock.json
# RUN npm install --production --silent && mv node_modules ../
RUN npm install --silent && mv node_modules ../
COPY . .
EXPOSE 3000
# RUN chown -R node /usr/src/app
# USER node
CMD ["npm", "start"]



