#!/bin/bash

SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

# delivery back not working on mac with m2
#rm -rf ./image.tar
#ssh admin@45.132.18.97 'rm -rf ~/image.tar'
#docker build -t "howto-api" $SCRIPT_DIR/../../back/HowTo
#
#docker save -o ./image.tar howto-api
#scp ./image.tar admin@45.132.18.97:~/ 
#ssh admin@45.132.18.97 'docker load -i ~/image.tar'
#
#rm -rf ./image.tar

# delivery front
ssh admin@45.132.18.97 'rm -rf ~/build'
ssh admin@45.132.18.97 'rm -rf ~/build.zip'
ssh admin@45.132.18.97 'rm -rf ~/nginx.conf'
ssh admin@45.132.18.97 'rm -rf ~/ci-prod.bash'
rm -rf build.zip
rm -rf build

npm run build --prefix $SCRIPT_DIR/../../front/how-to-ui
cp -R $SCRIPT_DIR/../../front/how-to-ui/build build
zip -r ./build.zip ./build/*
scp ./build.zip admin@45.132.18.97:~/ 
ssh admin@45.132.18.97 'unzip build.zip -d ./'

rm -rf build.zip
rm -rf build

# delivery scripts
scp ./ci-prod.bash admin@45.132.18.97:~/ 
scp ./docker-compose.yml admin@45.132.18.97:~/ 
scp ./nginx.conf admin@45.132.18.97:~/ 