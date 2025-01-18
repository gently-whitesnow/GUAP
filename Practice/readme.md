## проект

https://github.com/livegrep/livegrep

Сбилдить не вышло, веротяно проблема в OS

Я когда-то делал форк и настроил его на свой docker-hub репозиторий, поэтому билд происходит на CI и мы можем заюзать контейнер

compose для:
docker network create livegrep-network
docker volume create livegrep-data

- индексирования
- бэкенде
- фронтенда

баш скрипт для замера времени

docker run -v livegrep-data:/data ghcr.io/livegrep/livegrep/indexer /livegrep/bin/livegrep-github-reindex -repo livegrep/livegrep -repo dotnet/runtime -repo dotnet/aspnetcore -http -dir /data

docker run -d --rm -v livegrep-data:/data --network livegrep --name livegrep-backend ghcr.io/livegrep/livegrep/base /livegrep/bin/codesearch -load_index /data/livegrep.idx -grpc 0.0.0.0:9999

docker run -d --rm --network livegrep --publish 8910:8910 ghcr.io/livegrep/livegrep/base /livegrep/bin/livegrep -docroot /livegrep/web -listen=0.0.0.0:8910 --connect livegrep-backend:9999

python3 -m venv default  
source default/bin/activate  
deactivate

# большой индекс

python3 indexer.py /Users/gently/Projects/livegrep
python3 searcher.py get

bash test_1.sh /Users/gently/Projects/livegrep get
bash test_100.sh /Users/gently/Projects/livegrep get

# малый

python3 indexer.py /Users/gently/Projects/GUAP/Practice
python3 searcher.py get

bash test_1.sh /Users/gently/Projects/GUAP/Practice фронт

# огромный индекс

python3 indexer.py /Users/gently/Projects/aspnetcore
python3 searcher.py get

bash test_1.sh /Users/gently/Projects/aspnetcore get
bash test_100.sh /Users/gently/Projects/aspnetcore get

# 2gb repo linux

python3 indexer.py /Users/gently/Projects/linux-master
python3 searcher.py get

bash test_1.sh /Users/gently/Projects/linux-master russia
