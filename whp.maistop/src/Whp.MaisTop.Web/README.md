# +Top Brastemp Consul

## Pré-requisitos
Necessário ter o node e o gulp instalados. Verificar se estão através dos comandos:
```
node -v e gulp -v
```
Links para instalação:
[Node](https://nodejs.org/en/) / [Gulp](https://gulpjs.com/)
## Rodar o projeto
Para rodar o projeto, primeiro instale as dependências:
```
npm i
```
Após isso, na pasta Whp.MaisTop.Web é só digitar o comando :
```
npm run start
```
Para buidar o projeto é só digitar o comando :
```
npm run build
```

## Estrutura das pastas
O desenvolvimento do projeto possui três pastas principais:

    .
    ├── build                   * Arquivos compilados que serão usados
    ├                             para subir em produção
    ├── dev                     * Pasta de desenvolvimento (única a ser usada)
    └── server                  * Compilação da pasta dev para rodar
                                  localmente ( ignorar :D )

Detalhes da pasta dev:

    .
    ├── _assets                 * Possui as imagens, JS, SASS e e-mails
    ├── _logadas                * Estrutura das páginas que precisam de
    ├                             permissão para serem acessadas
    ├── _modais                 * Os modais que serão carregados
    ├── _shared                 * Todos os templates e componentes das páginas
    └── paginas                 * Os index de cada página

## Build

Antes de iniciar o build, ficar atento aos caminhos utilizados da API (**DEV**, **HOMOLOG** e **PROD**) em:

    .
    dev
      └─_assets
        └─ js
          └─ controller
            └─ _api.js

O build do projeto é feito através do comando:
```
gulp build
```
A pasta "build" será gerada na raíz do projeto e o conteúdo dela que será usado para subir os arquivos no FTP.
