# Su dung Node.js de build React app
FROM node:20-alpine AS build

WORKDIR /app

# Copy package.json va package-lock.json, sau do cai dat dependencies
COPY minibox-presentation/package.json \
	 minibox-presentation/package-lock.json \
	 minibox-presentation/tsconfig*.json \
	 minibox-presentation/vite.config.ts \
	 minibox-presentation/index.html \
	 ./

COPY minibox-presentation/src ./src

RUN ls -l /app/src

RUN npm install

# Copy toan bo source code vao container
COPY . .

# Build ung dung React
RUN npm run build

# Dung Nginx de chay ung dung
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html

# Mo cong 80
EXPOSE 80

# Chay Nginx
CMD ["nginx", "-g", "daemon off;"]
