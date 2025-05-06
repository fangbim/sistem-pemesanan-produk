Langkah-langkah build dan run docker

1. Build Docker Image
`docker build -t sistem-pemesanan-produk-api .`

2. Run Docker Container
`docker run -d -p 8000:8080 --name pemesanan-api sistem-pemesanan-produk-api`

3. Lihat daftar container yang jalan
`docker ps`

4. Stop Docker Container
`docker stop pemesanan-api`

5. Lihat daftar container 
`docker ps`

6. Jika mau running ulang docker container
`docker start pemesanan-api`

