Aggiornamento MinIO  

https://dl.min.io/server/minio/release/linux-amd64/  

wget https://dl.min.io/server/minio/release/linux-amd64/archive/minio_yyyyMMddxxxxxx_amd64.deb -O minio.deb  
systemctl stop minio.service  
sudo dpkg -i minio.deb  
*** modifica manuale  
nano /lib/systemd/system/minio.service <-- correggere utente e gruppo  
*** modifica tramite sed  
sed -i 's/minio-user/nome-corretto/g' /lib/systemd/system/minio.service  
systemctl daemon-reload  
systemctl start minio.service  
