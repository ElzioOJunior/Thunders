Para rodar o backend:


Clone o repositório e acesse o diretorio raiz do projeto ThundersTeste.

Abra o terminal e cole o seguinte comando, para subir o banco de dados mongoDB: 

docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo


Rode o projeto and enjoy :)

