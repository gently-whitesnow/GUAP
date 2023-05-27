# Настройка сервера

## Пользователь

https://linuxize.com/post/how-to-create-a-sudo-user-on-ubuntu/

## прокинуть ключи:
if not exist ~/.ssh/id_rsa.pub then `ssh-keygen`
cat ~/.ssh/id_rsa.pub | ssh admin@45.132.18.97 -p 22 "mkdir -p ~/.ssh && touch ~/.ssh/authorized_keys && chmod -R go= ~/.ssh && cat >> ~/.ssh/authorized_keys"

### docker
https://docs.docker.com/engine/install/ubuntu/
or
https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-20-04
sudo usermod -aG docker ${USER}
su - ${USER}
groups

### docker-compose
https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-compose-on-ubuntu-20-04

### git
sudo apt-get install git
ключи для гита
cd .ssh/
ssh-keygen -t ed25519 -C "gently.whitesnow@outlook.com"

### поднятие
docker-compose up -d

### порты
прокинуть порт 8500 и 8600

### безопасность
доступ только через vpn
(в случае vps поменять enp0s3 на eth0)

#  sudo iptables -I DOCKER-USER 1 -i enp0s3 -s 109.196.164.182 -j ACCEPT
#  sudo iptables -I DOCKER-USER 2 -i enp0s3 -s 109.196.164.182 -j ACCEPT
#  sudo iptables -I DOCKER-USER 3 -i enp0s3 -j DROP
#  sudo iptables -I DOCKER-USER 4 -i enp0s3 -j DROP

очистка
##### sudo iptables -D DOCKER-USER 2

проверка
##### sudo iptables -L DOCKER-USER --line-numbers -v -n

#### сохранить
sudo /sbin/iptables-save

secret =  31194cf3-9324-1877-e9c9-f910c902689f

docker network create -d bridge server_network




curl \
--header "X-Consul-Token: 31194cf3-9324-1877-e9c9-f910c902689f" \
http://195.133.197.230:8500/v1/agent/members

curl \
--header "X-Consul-Token: 31194cf3-9324-1877-e9c9-f910c902689f" \
consul-agent:8500/v1/agent/members

curl \
--header "X-Consul-Token: 31194cf3-9324-1877-e9c9-f910c902689f" \
http://localhost:8500/v1/agent/members

curl \
--header "X-Consul-Token: 31194cf3-9324-1877-e9c9-f910c902689f" \
http://localhost:8500/v1/health/state/any

apt-get update && apt-get install -y iputils-ping && apt-get install -y curl

docker-compose stop && docker-compose rm -f

не забывай 

"addresses": {
"http" : "0.0.0.0" // друг не забывай это, для консула в контейнере
},

