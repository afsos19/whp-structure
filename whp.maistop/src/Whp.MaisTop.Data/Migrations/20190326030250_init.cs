using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ARQUIVO_VENDA_SKU_STATUS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARQUIVO_VENDA_SKU_STATUS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CARGO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARGO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIA_PRODUTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA_PRODUTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CONFIGURACAO_PONTOS_EXPIRADO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EXPIRA_EM = table.Column<DateTime>(nullable: false),
                    ATIVADO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONFIGURACAO_PONTOS_EXPIRADO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CONFIGURACAO_ROBO_PONTOS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_ROBO_PONTOS = table.Column<int>(nullable: false),
                    ID_MECANISMO_ROBO_PONTOS = table.Column<int>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONFIGURACAO_ROBO_PONTOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FORNECEDOR",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDOR", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NOVIDADES",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TITULO = table.Column<string>(nullable: true),
                    SUBTITULO = table.Column<string>(nullable: true),
                    DESCRICAO = table.Column<string>(nullable: true),
                    PHOTO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false),
                    ORDENACAO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOVIDADES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_CANCELAMENTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TOKEN_ACESSO = table.Column<string>(nullable: true),
                    LOGIN = table.Column<string>(nullable: true),
                    ID_PEDIDO_EXTERNO = table.Column<long>(nullable: false),
                    CODIGO_AUTORIZACAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_CANCELAMENTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_STATUS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(nullable: true),
                    CODIGO = table.Column<string>(nullable: true),
                    METODO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_STATUS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_GRUPO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_GRUPO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "REGIONAL",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(nullable: true),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGIONAL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SAC_ASSUNTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAC_ASSUNTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SAC_STATUS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAC_STATUS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SAC_TIPO_CONTATO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAC_TIPO_CONTATO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SAC_TIPO_MENSAGEM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAC_TIPO_MENSAGEM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STATUS_ARQUIVO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATUS_ARQUIVO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_STATUS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_STATUS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ORIGEM_PONTUACAO_USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_CONFIGURACAO_ROBO_PONTOS = table.Column<int>(nullable: true),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORIGEM_PONTUACAO_USUARIO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORIGEM_PONTUACAO_USUARIO_CONFIGURACAO_ROBO_PONTOS_ID_CONFIGURACAO_ROBO_PONTOS",
                        column: x => x.ID_CONFIGURACAO_ROBO_PONTOS,
                        principalTable: "CONFIGURACAO_ROBO_PONTOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_FORNECEDOR = table.Column<int>(nullable: true),
                    ID_PRODUTO_CATEGORIA = table.Column<int>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    NOME = table.Column<string>(nullable: true),
                    FOTO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTO_CATEGORIA_PRODUTO_ID_PRODUTO_CATEGORIA",
                        column: x => x.ID_PRODUTO_CATEGORIA,
                        principalTable: "CATEGORIA_PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTO_FORNECEDOR_ID_FORNECEDOR",
                        column: x => x.ID_FORNECEDOR,
                        principalTable: "FORNECEDOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REDE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_REGIONAL = table.Column<int>(nullable: true),
                    NOME = table.Column<string>(nullable: true),
                    SITE_NOME = table.Column<string>(nullable: true),
                    SITE_IMAGEM = table.Column<string>(nullable: true),
                    SITE_NOME_CURTO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REDE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REDE_REGIONAL_ID_REGIONAL",
                        column: x => x.ID_REGIONAL,
                        principalTable: "REGIONAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO_STATUS = table.Column<int>(nullable: true),
                    ID_CARGO = table.Column<int>(nullable: true),
                    ESTADO_CIVIL = table.Column<string>(nullable: false),
                    NOME = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    DATA_NASCIMENTO = table.Column<DateTime>(nullable: true),
                    TELEFONE = table.Column<string>(nullable: true),
                    TELEFONE_COMERCIAL = table.Column<string>(nullable: true),
                    CELULAR = table.Column<string>(nullable: true),
                    GENERO = table.Column<string>(nullable: false),
                    EMAIL = table.Column<string>(nullable: true),
                    SENHA = table.Column<string>(nullable: true),
                    QUANTIDADE_FILHOS = table.Column<int>(nullable: false),
                    TIME_CORACAO = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    UF = table.Column<string>(nullable: true),
                    CIDADE = table.Column<string>(nullable: true),
                    BAIRRO = table.Column<string>(nullable: true),
                    ENDERECO = table.Column<string>(nullable: true),
                    NUMERO = table.Column<int>(nullable: false),
                    COMPLEMENTO = table.Column<string>(nullable: true),
                    PONTO_REFERENCIA = table.Column<string>(nullable: true),
                    FOTO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    DESLIGADO_EM = table.Column<DateTime>(nullable: true),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_CARGO_ID_CARGO",
                        column: x => x.ID_CARGO,
                        principalTable: "CARGO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USUARIO_USUARIO_STATUS_ID_USUARIO_STATUS",
                        column: x => x.ID_USUARIO_STATUS,
                        principalTable: "USUARIO_STATUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LOJA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_REDE = table.Column<int>(nullable: true),
                    CODIGO_LOJA = table.Column<string>(nullable: true),
                    NOME = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    CIDADE = table.Column<string>(nullable: true),
                    BAIRRO = table.Column<string>(nullable: true),
                    ENDERECO = table.Column<string>(nullable: true),
                    NUMERO = table.Column<int>(nullable: false),
                    COMPLEMENTO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOJA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LOJA_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NOVIDADES_RELACIONADA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_NOVIDADE = table.Column<int>(nullable: true),
                    ID_CARGO = table.Column<int>(nullable: true),
                    ID_REDE = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOVIDADES_RELACIONADA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOVIDADES_RELACIONADA_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NOVIDADES_RELACIONADA_NOVIDADES_ID_NOVIDADE",
                        column: x => x.ID_NOVIDADE,
                        principalTable: "NOVIDADES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NOVIDADES_RELACIONADA_CARGO_ID_CARGO",
                        column: x => x.ID_CARGO,
                        principalTable: "CARGO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_PARTICIPANTE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_PRODUTO = table.Column<int>(nullable: true),
                    ID_REDE = table.Column<int>(nullable: true),
                    PONTUACAO = table.Column<decimal>(nullable: false),
                    MES_VIGENTE = table.Column<int>(nullable: false),
                    ANO_VIGENTE = table.Column<int>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_PARTICIPANTE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTO_PARTICIPANTE_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTO_PARTICIPANTE_PRODUTO_ID_PRODUTO",
                        column: x => x.ID_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_SUPERTOP",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_PRODUTO = table.Column<int>(nullable: true),
                    ID_REDE = table.Column<int>(nullable: true),
                    ID_PRODUTO_GRUPO = table.Column<int>(nullable: true),
                    PONTUACAO = table.Column<decimal>(nullable: false),
                    MES_VIGENTE = table.Column<int>(nullable: false),
                    ANO_VIGENTE = table.Column<int>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_SUPERTOP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTO_SUPERTOP_PRODUTO_GRUPO_ID_PRODUTO_GRUPO",
                        column: x => x.ID_PRODUTO_GRUPO,
                        principalTable: "PRODUTO_GRUPO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTO_SUPERTOP_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTO_SUPERTOP_PRODUTO_ID_PRODUTO",
                        column: x => x.ID_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ARQUIVO_HIERARQUIA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_REDE = table.Column<int>(nullable: true),
                    ID_STATUS_ARQUIVO = table.Column<int>(nullable: true),
                    NOME_ARQUIVO = table.Column<string>(nullable: true),
                    MES_VIGENTE = table.Column<int>(nullable: false),
                    ANO_VIGENTE = table.Column<int>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARQUIVO_HIERARQUIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_HIERARQUIA_STATUS_ARQUIVO_ID_STATUS_ARQUIVO",
                        column: x => x.ID_STATUS_ARQUIVO,
                        principalTable: "STATUS_ARQUIVO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_HIERARQUIA_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_HIERARQUIA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ARQUIVO_VENDA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_REDE = table.Column<int>(nullable: true),
                    ID_STATUS_ARQUIVO = table.Column<int>(nullable: true),
                    NOME_ARQUIVO = table.Column<string>(nullable: true),
                    MES_VIGENTE = table.Column<int>(nullable: false),
                    ANO_VIGENTE = table.Column<int>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARQUIVO_VENDA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_VENDA_STATUS_ARQUIVO_ID_STATUS_ARQUIVO",
                        column: x => x.ID_STATUS_ARQUIVO,
                        principalTable: "STATUS_ARQUIVO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_VENDA_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_VENDA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TOKEN_ACESSO = table.Column<string>(nullable: true),
                    LOGIN = table.Column<string>(nullable: true),
                    ID_PEDIDO_EXTERNO = table.Column<long>(nullable: false),
                    CODIGO_AUTORIZACAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_PEDIDO_STATUS = table.Column<int>(nullable: true),
                    TOTAL_PEDIDO = table.Column<decimal>(nullable: false),
                    VALOR_PEDIDO = table.Column<decimal>(nullable: false),
                    FRETE = table.Column<decimal>(nullable: false),
                    TAXA_CONVERSAO = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_PEDIDO_STATUS_ID_PEDIDO_STATUS",
                        column: x => x.ID_PEDIDO_STATUS,
                        principalTable: "PEDIDO_STATUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PEDIDO_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PONTUACAO_USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_ORIGEM_PONTUACAO = table.Column<int>(nullable: true),
                    TIPO_OPERACAO = table.Column<string>(nullable: false),
                    ID_ENTIDADE_REFERENTE = table.Column<int>(nullable: false),
                    MES_VIGENTE = table.Column<int>(nullable: false),
                    ANO_VIGENTE = table.Column<int>(nullable: false),
                    DESCRICAO = table.Column<string>(nullable: true),
                    PONTUACAO = table.Column<decimal>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PONTUACAO_USUARIO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PONTUACAO_USUARIO_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PONTUACAO_USUARIO_ORIGEM_PONTUACAO_USUARIO_ID_ORIGEM_PONTUACAO",
                        column: x => x.ID_ORIGEM_PONTUACAO,
                        principalTable: "ORIGEM_PONTUACAO_USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SAC",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_SAC_STATUS = table.Column<int>(nullable: true),
                    ID_SAC_ASSUNTO = table.Column<int>(nullable: true),
                    ID_SAC_TIPO_CONTATO = table.Column<int>(nullable: true),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    CODIGO = table.Column<string>(nullable: true),
                    ARQUIVO = table.Column<string>(nullable: true),
                    BRASILCT_CHAMADO = table.Column<string>(nullable: true),
                    CRITICO = table.Column<bool>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAC", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SAC_SAC_TIPO_CONTATO_ID_SAC_TIPO_CONTATO",
                        column: x => x.ID_SAC_TIPO_CONTATO,
                        principalTable: "SAC_TIPO_CONTATO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SAC_SAC_STATUS_ID_SAC_STATUS",
                        column: x => x.ID_SAC_STATUS,
                        principalTable: "SAC_STATUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SAC_SAC_ASSUNTO_ID_SAC_ASSUNTO",
                        column: x => x.ID_SAC_ASSUNTO,
                        principalTable: "SAC_ASSUNTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SAC_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_CODIGO_CONFIRMACAO_ACESSO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    CODIGO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_CODIGO_CONFIRMACAO_ACESSO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_CODIGO_CONFIRMACAO_ACESSO_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_PONTOS_TREINAMENTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_TREINAMENTO = table.Column<int>(nullable: false),
                    TREINAMENTO_DESCRICAO = table.Column<string>(nullable: true),
                    TREINAMENTO_STATUS = table.Column<string>(nullable: true),
                    ID_RESULTADO = table.Column<int>(nullable: false),
                    DATA_INICIO = table.Column<DateTime>(nullable: false),
                    DATA_LIMITE = table.Column<DateTime>(nullable: false),
                    CONCLUIDO_EM = table.Column<DateTime>(nullable: false),
                    PONTO = table.Column<decimal>(nullable: false),
                    PERCENTUAL = table.Column<decimal>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_PONTOS_TREINAMENTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_PONTOS_TREINAMENTO_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_STATUS_LOG",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_USUARIO_STATUS = table.Column<int>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_STATUS_LOG", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_STATUS_LOG_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USUARIO_STATUS_LOG_USUARIO_STATUS_ID_USUARIO_STATUS",
                        column: x => x.ID_USUARIO_STATUS,
                        principalTable: "USUARIO_STATUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_LOJA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_LOJA = table.Column<int>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_LOJA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_LOJA_LOJA_ID_LOJA",
                        column: x => x.ID_LOJA,
                        principalTable: "LOJA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USUARIO_LOJA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VENDA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_REDE = table.Column<int>(nullable: true),
                    ID_LOJA = table.Column<int>(nullable: true),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_PRODUTO = table.Column<int>(nullable: true),
                    QUANTIDADE = table.Column<int>(nullable: false),
                    DATA_VENDA = table.Column<DateTime>(nullable: false),
                    MES_VIGENTE = table.Column<int>(nullable: false),
                    ANO_VIGENTE = table.Column<int>(nullable: false),
                    VALOR_UNIDADE = table.Column<decimal>(nullable: false),
                    VALOR_TOTAL = table.Column<decimal>(nullable: false),
                    PONTUACAO = table.Column<decimal>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENDA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VENDA_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VENDA_PRODUTO_ID_PRODUTO",
                        column: x => x.ID_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VENDA_LOJA_ID_LOJA",
                        column: x => x.ID_LOJA,
                        principalTable: "LOJA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VENDA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ARQUIVO_HIERARQUIA_DADOS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_ARQUIVO_HIERARQUIA = table.Column<int>(nullable: true),
                    REVENDA = table.Column<string>(nullable: true),
                    CODIGO_LOJA = table.Column<string>(nullable: true),
                    NOME = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    CARGO = table.Column<string>(nullable: true),
                    DESLIGADO = table.Column<string>(nullable: true),
                    PLANILHA = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARQUIVO_HIERARQUIA_DADOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_HIERARQUIA_DADOS_ARQUIVO_HIERARQUIA_ID_ARQUIVO_HIERARQUIA",
                        column: x => x.ID_ARQUIVO_HIERARQUIA,
                        principalTable: "ARQUIVO_HIERARQUIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ARQUIVO_HIERARQUIA_DADOS_ERRO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_ARQUIVO_HIERARQUIA = table.Column<int>(nullable: true),
                    LINHA = table.Column<int>(nullable: false),
                    DESCRICAO = table.Column<string>(nullable: true),
                    PLANILHA = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARQUIVO_HIERARQUIA_DADOS_ERRO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_HIERARQUIA_DADOS_ERRO_ARQUIVO_HIERARQUIA_ID_ARQUIVO_HIERARQUIA",
                        column: x => x.ID_ARQUIVO_HIERARQUIA,
                        principalTable: "ARQUIVO_HIERARQUIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ARQUIVO_VENDA_DADOS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_ARQUIVO_VENDA = table.Column<int>(nullable: true),
                    ID_PRODUTO = table.Column<int>(nullable: false),
                    ID_ARQUIVO_VENDA_SKU_STATUS = table.Column<int>(nullable: true),
                    REVENDA = table.Column<string>(nullable: true),
                    CODIGO_LOJA = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    CPF_VENDEDOR = table.Column<string>(nullable: true),
                    NOME_VENDEDOR = table.Column<string>(nullable: true),
                    CATEGORIA = table.Column<string>(nullable: true),
                    DESCRICAO_PRODUTO = table.Column<string>(nullable: true),
                    QUANTIDADE = table.Column<int>(nullable: false),
                    DATA_VENDA = table.Column<DateTime>(nullable: false),
                    NUMERO_PEDIDO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARQUIVO_VENDA_DADOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_VENDA_DADOS_ARQUIVO_VENDA_ID_ARQUIVO_VENDA",
                        column: x => x.ID_ARQUIVO_VENDA,
                        principalTable: "ARQUIVO_VENDA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_VENDA_DADOS_ARQUIVO_VENDA_SKU_STATUS_ID_ARQUIVO_VENDA_SKU_STATUS",
                        column: x => x.ID_ARQUIVO_VENDA_SKU_STATUS,
                        principalTable: "ARQUIVO_VENDA_SKU_STATUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ARQUIVO_VENDA_DADOS_ERRO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_ARQUIVO_VENDA = table.Column<int>(nullable: true),
                    LINHA = table.Column<int>(nullable: false),
                    DESCRICAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARQUIVO_VENDA_DADOS_ERRO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ARQUIVO_VENDA_DADOS_ERRO_ARQUIVO_VENDA_ID_ARQUIVO_VENDA",
                        column: x => x.ID_ARQUIVO_VENDA,
                        principalTable: "ARQUIVO_VENDA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_CONFIRMACAO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TOKEN_ACESSO = table.Column<string>(nullable: true),
                    LOGIN = table.Column<string>(nullable: true),
                    ID_PEDIDO_EXTERNO = table.Column<long>(nullable: false),
                    CODIGO_AUTORIZACAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false),
                    ID_PEDIDO = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_CONFIRMACAO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_CONFIRMACAO_PEDIDO_ID_PEDIDO",
                        column: x => x.ID_PEDIDO,
                        principalTable: "PEDIDO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_EXTORNO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TOKEN_ACESSO = table.Column<string>(nullable: true),
                    LOGIN = table.Column<string>(nullable: true),
                    ID_PEDIDO_EXTERNO = table.Column<long>(nullable: false),
                    CODIGO_AUTORIZACAO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false),
                    ID_PEDIDO = table.Column<int>(nullable: true),
                    VALOR = table.Column<decimal>(nullable: false),
                    MENSAGEM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_EXTORNO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_EXTORNO_PEDIDO_ID_PEDIDO",
                        column: x => x.ID_PEDIDO,
                        principalTable: "PEDIDO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_ITEM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_PEDIDO = table.Column<int>(nullable: true),
                    ID_PEDIDO_EXTERNO = table.Column<long>(nullable: false),
                    CODIGO_ITEM = table.Column<int>(nullable: false),
                    NOME_PRODUTO = table.Column<string>(nullable: true),
                    PARCEIRO = table.Column<string>(nullable: true),
                    DEPARTAMENTO = table.Column<string>(nullable: true),
                    CATEGORIA = table.Column<string>(nullable: true),
                    VALOR_UNITARIO = table.Column<decimal>(nullable: false),
                    VALOR_TOTAL = table.Column<decimal>(nullable: false),
                    QUANTIDADE = table.Column<int>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false),
                    DATA_PREVISAO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_ITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_ITEM_PEDIDO_ID_PEDIDO",
                        column: x => x.ID_PEDIDO,
                        principalTable: "PEDIDO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SAC_MENSAGEM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_SAC = table.Column<int>(nullable: true),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_SAC_TIPO_MENSAGEM = table.Column<int>(nullable: true),
                    MENSAGEM = table.Column<string>(nullable: true),
                    ARQUIVO = table.Column<string>(nullable: true),
                    MENSAGEM_INTERNA = table.Column<bool>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAC_MENSAGEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SAC_MENSAGEM_SAC_ID_SAC",
                        column: x => x.ID_SAC,
                        principalTable: "SAC",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SAC_MENSAGEM_SAC_TIPO_MENSAGEM_ID_SAC_TIPO_MENSAGEM",
                        column: x => x.ID_SAC_TIPO_MENSAGEM,
                        principalTable: "SAC_TIPO_MENSAGEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SAC_MENSAGEM_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_HIERARQUIA_ID_STATUS_ARQUIVO",
                table: "ARQUIVO_HIERARQUIA",
                column: "ID_STATUS_ARQUIVO");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_HIERARQUIA_ID_REDE",
                table: "ARQUIVO_HIERARQUIA",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_HIERARQUIA_ID_USUARIO",
                table: "ARQUIVO_HIERARQUIA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_HIERARQUIA_DADOS_ID_ARQUIVO_HIERARQUIA",
                table: "ARQUIVO_HIERARQUIA_DADOS",
                column: "ID_ARQUIVO_HIERARQUIA");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_HIERARQUIA_DADOS_ERRO_ID_ARQUIVO_HIERARQUIA",
                table: "ARQUIVO_HIERARQUIA_DADOS_ERRO",
                column: "ID_ARQUIVO_HIERARQUIA");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_VENDA_ID_STATUS_ARQUIVO",
                table: "ARQUIVO_VENDA",
                column: "ID_STATUS_ARQUIVO");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_VENDA_ID_REDE",
                table: "ARQUIVO_VENDA",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_VENDA_ID_USUARIO",
                table: "ARQUIVO_VENDA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_VENDA_DADOS_ID_ARQUIVO_VENDA",
                table: "ARQUIVO_VENDA_DADOS",
                column: "ID_ARQUIVO_VENDA");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_VENDA_DADOS_ID_ARQUIVO_VENDA_SKU_STATUS",
                table: "ARQUIVO_VENDA_DADOS",
                column: "ID_ARQUIVO_VENDA_SKU_STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_ARQUIVO_VENDA_DADOS_ERRO_ID_ARQUIVO_VENDA",
                table: "ARQUIVO_VENDA_DADOS_ERRO",
                column: "ID_ARQUIVO_VENDA");

            migrationBuilder.CreateIndex(
                name: "IX_LOJA_ID_REDE",
                table: "LOJA",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_NOVIDADES_RELACIONADA_ID_REDE",
                table: "NOVIDADES_RELACIONADA",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_NOVIDADES_RELACIONADA_ID_NOVIDADE",
                table: "NOVIDADES_RELACIONADA",
                column: "ID_NOVIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_NOVIDADES_RELACIONADA_ID_CARGO",
                table: "NOVIDADES_RELACIONADA",
                column: "ID_CARGO");

            migrationBuilder.CreateIndex(
                name: "IX_ORIGEM_PONTUACAO_USUARIO_ID_CONFIGURACAO_ROBO_PONTOS",
                table: "ORIGEM_PONTUACAO_USUARIO",
                column: "ID_CONFIGURACAO_ROBO_PONTOS");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ID_PEDIDO_STATUS",
                table: "PEDIDO",
                column: "ID_PEDIDO_STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ID_USUARIO",
                table: "PEDIDO",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_CONFIRMACAO_ID_PEDIDO",
                table: "PEDIDO_CONFIRMACAO",
                column: "ID_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_EXTORNO_ID_PEDIDO",
                table: "PEDIDO_EXTORNO",
                column: "ID_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ITEM_ID_PEDIDO",
                table: "PEDIDO_ITEM",
                column: "ID_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_PONTUACAO_USUARIO_ID_USUARIO",
                table: "PONTUACAO_USUARIO",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PONTUACAO_USUARIO_ID_ORIGEM_PONTUACAO",
                table: "PONTUACAO_USUARIO",
                column: "ID_ORIGEM_PONTUACAO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_ID_PRODUTO_CATEGORIA",
                table: "PRODUTO",
                column: "ID_PRODUTO_CATEGORIA");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_ID_FORNECEDOR",
                table: "PRODUTO",
                column: "ID_FORNECEDOR");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_PARTICIPANTE_ID_REDE",
                table: "PRODUTO_PARTICIPANTE",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_PARTICIPANTE_ID_PRODUTO",
                table: "PRODUTO_PARTICIPANTE",
                column: "ID_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_SUPERTOP_ID_PRODUTO_GRUPO",
                table: "PRODUTO_SUPERTOP",
                column: "ID_PRODUTO_GRUPO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_SUPERTOP_ID_REDE",
                table: "PRODUTO_SUPERTOP",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_SUPERTOP_ID_PRODUTO",
                table: "PRODUTO_SUPERTOP",
                column: "ID_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_REDE_ID_REGIONAL",
                table: "REDE",
                column: "ID_REGIONAL");

            migrationBuilder.CreateIndex(
                name: "IX_SAC_ID_SAC_TIPO_CONTATO",
                table: "SAC",
                column: "ID_SAC_TIPO_CONTATO");

            migrationBuilder.CreateIndex(
                name: "IX_SAC_ID_SAC_STATUS",
                table: "SAC",
                column: "ID_SAC_STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_SAC_ID_SAC_ASSUNTO",
                table: "SAC",
                column: "ID_SAC_ASSUNTO");

            migrationBuilder.CreateIndex(
                name: "IX_SAC_ID_USUARIO",
                table: "SAC",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_SAC_MENSAGEM_ID_SAC",
                table: "SAC_MENSAGEM",
                column: "ID_SAC");

            migrationBuilder.CreateIndex(
                name: "IX_SAC_MENSAGEM_ID_SAC_TIPO_MENSAGEM",
                table: "SAC_MENSAGEM",
                column: "ID_SAC_TIPO_MENSAGEM");

            migrationBuilder.CreateIndex(
                name: "IX_SAC_MENSAGEM_ID_USUARIO",
                table: "SAC_MENSAGEM",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_ID_CARGO",
                table: "USUARIO",
                column: "ID_CARGO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_ID_USUARIO_STATUS",
                table: "USUARIO",
                column: "ID_USUARIO_STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_CODIGO_CONFIRMACAO_ACESSO_ID_USUARIO",
                table: "USUARIO_CODIGO_CONFIRMACAO_ACESSO",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_LOJA_ID_LOJA",
                table: "USUARIO_LOJA",
                column: "ID_LOJA");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_LOJA_ID_USUARIO",
                table: "USUARIO_LOJA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_PONTOS_TREINAMENTO_ID_USUARIO",
                table: "USUARIO_PONTOS_TREINAMENTO",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_STATUS_LOG_ID_USUARIO",
                table: "USUARIO_STATUS_LOG",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_STATUS_LOG_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                column: "ID_USUARIO_STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_VENDA_ID_REDE",
                table: "VENDA",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_VENDA_ID_PRODUTO",
                table: "VENDA",
                column: "ID_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_VENDA_ID_LOJA",
                table: "VENDA",
                column: "ID_LOJA");

            migrationBuilder.CreateIndex(
                name: "IX_VENDA_ID_USUARIO",
                table: "VENDA",
                column: "ID_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ARQUIVO_HIERARQUIA_DADOS");

            migrationBuilder.DropTable(
                name: "ARQUIVO_HIERARQUIA_DADOS_ERRO");

            migrationBuilder.DropTable(
                name: "ARQUIVO_VENDA_DADOS");

            migrationBuilder.DropTable(
                name: "ARQUIVO_VENDA_DADOS_ERRO");

            migrationBuilder.DropTable(
                name: "CONFIGURACAO_PONTOS_EXPIRADO");

            migrationBuilder.DropTable(
                name: "NOVIDADES_RELACIONADA");

            migrationBuilder.DropTable(
                name: "PEDIDO_CANCELAMENTO");

            migrationBuilder.DropTable(
                name: "PEDIDO_CONFIRMACAO");

            migrationBuilder.DropTable(
                name: "PEDIDO_EXTORNO");

            migrationBuilder.DropTable(
                name: "PEDIDO_ITEM");

            migrationBuilder.DropTable(
                name: "PONTUACAO_USUARIO");

            migrationBuilder.DropTable(
                name: "PRODUTO_PARTICIPANTE");

            migrationBuilder.DropTable(
                name: "PRODUTO_SUPERTOP");

            migrationBuilder.DropTable(
                name: "SAC_MENSAGEM");

            migrationBuilder.DropTable(
                name: "USUARIO_CODIGO_CONFIRMACAO_ACESSO");

            migrationBuilder.DropTable(
                name: "USUARIO_LOJA");

            migrationBuilder.DropTable(
                name: "USUARIO_PONTOS_TREINAMENTO");

            migrationBuilder.DropTable(
                name: "USUARIO_STATUS_LOG");

            migrationBuilder.DropTable(
                name: "VENDA");

            migrationBuilder.DropTable(
                name: "ARQUIVO_HIERARQUIA");

            migrationBuilder.DropTable(
                name: "ARQUIVO_VENDA_SKU_STATUS");

            migrationBuilder.DropTable(
                name: "ARQUIVO_VENDA");

            migrationBuilder.DropTable(
                name: "NOVIDADES");

            migrationBuilder.DropTable(
                name: "PEDIDO");

            migrationBuilder.DropTable(
                name: "ORIGEM_PONTUACAO_USUARIO");

            migrationBuilder.DropTable(
                name: "PRODUTO_GRUPO");

            migrationBuilder.DropTable(
                name: "SAC");

            migrationBuilder.DropTable(
                name: "SAC_TIPO_MENSAGEM");

            migrationBuilder.DropTable(
                name: "PRODUTO");

            migrationBuilder.DropTable(
                name: "LOJA");

            migrationBuilder.DropTable(
                name: "STATUS_ARQUIVO");

            migrationBuilder.DropTable(
                name: "PEDIDO_STATUS");

            migrationBuilder.DropTable(
                name: "CONFIGURACAO_ROBO_PONTOS");

            migrationBuilder.DropTable(
                name: "SAC_TIPO_CONTATO");

            migrationBuilder.DropTable(
                name: "SAC_STATUS");

            migrationBuilder.DropTable(
                name: "SAC_ASSUNTO");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "CATEGORIA_PRODUTO");

            migrationBuilder.DropTable(
                name: "FORNECEDOR");

            migrationBuilder.DropTable(
                name: "REDE");

            migrationBuilder.DropTable(
                name: "CARGO");

            migrationBuilder.DropTable(
                name: "USUARIO_STATUS");

            migrationBuilder.DropTable(
                name: "REGIONAL");
        }
    }
}
