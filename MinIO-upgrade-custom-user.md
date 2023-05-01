Aggiornamento MinIO  
  
systemctl stop minio.service  
wget https://dl.min.io/server/minio/release/linux-amd64/archive/minio_20230428181117.0.0_amd64.deb -O minio.deb  
sudo dpkg -i minio.deb  
nano /etc/systemd/system/minio.service <-- correggere utente e gruppo  
systemctl daemon-reload  
systemctl start minio.service  
