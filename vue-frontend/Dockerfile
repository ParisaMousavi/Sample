# Build Vue Project
FROM node:12.18.1-alpine AS Build
WORKDIR /app
COPY package.json ./
RUN npm install
COPY . .
RUN npm run build

# Create NGINX server
FROM nginx:1.19.1-alpine AS prod-stage
COPY --from=Build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD [ "nginx","-g","daemon off;" ]