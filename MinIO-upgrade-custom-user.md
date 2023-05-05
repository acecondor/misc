Aggiornamento MinIO  

https://dl.min.io/server/minio/release/linux-amd64/  

systemctl stop minio.service  
wget https://dl.min.io/server/minio/release/linux-amd64/archive/minio_yyyyMMddxxxxxx_amd64.deb -O minio.deb  
sudo dpkg -i minio.deb  
*** modifica manuale  
nano /etc/systemd/system/minio.service <-- correggere utente e gruppo  
*** modifica tramite sed  
sed -i 's/minio-user/nome-corretto/g' /etc/systemd/system/minio.service
systemctl daemon-reload  
systemctl start minio.service  
