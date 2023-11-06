# Настройка сервера

## Пользователь

https://linuxize.com/post/how-to-create-a-sudo-user-on-ubuntu/

## прокинуть ключи:
if not exist ~/.ssh/id_rsa.pub then `ssh-keygen`
cat ~/.ssh/id_rsa.pub | ssh admin@45.132.18.97 -p 22 "mkdir -p ~/.ssh && touch ~/.ssh/authorized_keys && chmod -R go= ~/.ssh && cat >> ~/.ssh/authorized_keys"

### docker
`scp deploydocker.bash admin@45.132.18.97:~/`

or manual:
https://docs.docker.com/engine/install/ubuntu/
or
https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-20-04

### docker-compose
sudo curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose
echo "test docker"
sudo docker ps
echo "test compose"
docker-compose --version

or manual
https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-compose-on-ubuntu-20-04

### git (он по сути и не нужен)
sudo apt-get install git
ключи для гита
cd .ssh/
ssh-keygen -t ed25519 -C "gently.whitesnow@outlook.com"

### additional
sudo apt-get install unzip

### nginx optional



it looks like a problem with the folder permissions. Try to execute the following:

chmod -R 755 /usr/share/nginx/html