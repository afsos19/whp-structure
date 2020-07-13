using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Extensions;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.ViewModels;

namespace Whp.MaisTop.Data.Repositories
{
    public class SaleFileDataRepository : Repository<SaleFileData>, ISaleFileDataRepository
    {
        public SaleFileDataRepository(WhpDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<SKUClassificationVM> Data, int Count)> GetPendingClassification(int start, int limit, int network = 0)
        {
 
            SqlParameter pStart = new SqlParameter("@start", start);
            SqlParameter pLimit = new SqlParameter("@limit", limit);
            SqlParameter pNetwork = new SqlParameter("@network", network);

            var countQuery = await _dbContext.Query<CountVM>().FromSql
                                    (@"SELECT TOP 1 COUNT(*) OVER () as Count
                                     FROM ARQUIVO_VENDA_DADOS AS D
                                    INNER JOIN ARQUIVO_VENDA AS A ON D.ID_ARQUIVO_VENDA = A.ID
                                    INNER JOIN ARQUIVO_VENDA_SKU_STATUS AS S ON S.ID = D.ID_ARQUIVO_VENDA_SKU_STATUS
                                    WHERE ID_ARQUIVO_VENDA_SKU_STATUS NOT IN (5,3,6) AND A.ID_STATUS_ARQUIVO = 2 AND (@network = 0 OR  A.ID_REDE = @network) 
                                    GROUP BY D.DESCRICAO_PRODUTO,REVENDA, S.DESCRICAO, A.ANO_VIGENTE, A.MES_VIGENTE, A.ID_REDE
                                    ORDER BY D.DESCRICAO_PRODUTO", pNetwork).FirstAsync();

            var fullQuery = await _dbContext.Query<SKUClassificationVM>().FromSql
                                    (@"SELECT 
	                                        D.DESCRICAO_PRODUTO as ProductDescription,
	                                        REVENDA as Resale, 
	                                        S.DESCRICAO as StatusSKU, 
	                                        A.ANO_VIGENTE as CurrentYear, 
	                                        A.MES_VIGENTE as CurrentMonth,
                                            A.ID_REDE as Network
                                         FROM ARQUIVO_VENDA_DADOS AS D
                                        INNER JOIN ARQUIVO_VENDA AS A ON D.ID_ARQUIVO_VENDA = A.ID
                                        INNER JOIN ARQUIVO_VENDA_SKU_STATUS AS S ON S.ID = D.ID_ARQUIVO_VENDA_SKU_STATUS
                                        WHERE ID_ARQUIVO_VENDA_SKU_STATUS NOT IN (5,3,6) AND A.ID_STATUS_ARQUIVO = 2 AND (@network = 0 OR  A.ID_REDE = @network)
                                        GROUP BY D.DESCRICAO_PRODUTO,REVENDA, S.DESCRICAO, A.ANO_VIGENTE, A.MES_VIGENTE, A.ID_REDE
                                        ORDER BY D.DESCRICAO_PRODUTO
                                        OFFSET @start ROWS
                                        FETCH NEXT @limit ROWS ONLY",
                               pStart,
                               pLimit,
                               pNetwork).ToListAsync();

            return (fullQuery, countQuery.Count);
        }

        public async Task<(IEnumerable<string[]> Data, int Count)> GetPreProcessingSale(int currentYear, int currentMonth, int start, int limit, int network = 0)
        {

            SqlParameter pYear = new SqlParameter("@year", currentYear);
            SqlParameter pMonth = new SqlParameter("@month", currentMonth);
            SqlParameter pStart = new SqlParameter("@start", start);
            SqlParameter pLimit = new SqlParameter("@limit", limit);
            SqlParameter pNetwork = new SqlParameter("@network", network);
            SqlParameter pStatusFile = new SqlParameter("@statusFile", (int)FileStatusEnum.InProgress);

            var preProcessingCount = await _dbContext.Query<CountVM>().FromSql
                                    (@"SELECT TOP 1 COUNT(*) OVER () as Count
                                FROM ARQUIVO_VENDA AS ARQUIVO
	                                INNER JOIN REDE AS REDE ON REDE.ID = ARQUIVO.ID_REDE
	                                INNER JOIN ARQUIVO_VENDA_DADOS AS DADOS ON DADOS.ID_ARQUIVO_VENDA = ARQUIVO.ID
	                                INNER JOIN ARQUIVO_VENDA_SKU_STATUS AS STATUS ON STATUS.ID = DADOS.ID_ARQUIVO_VENDA_SKU_STATUS
	                                LEFT JOIN PRODUTO AS PRODUTO ON PRODUTO.ID = DADOS.ID_PRODUTO
	                                LEFT JOIN CATEGORIA_PRODUTO AS CATEGORIA ON CATEGORIA.ID = PRODUTO.ID_PRODUTO_CATEGORIA
	                                LEFT JOIN FORNECEDOR AS FABRICANTE ON FABRICANTE.ID = PRODUTO.ID_FORNECEDOR
                                WHERE ARQUIVO.MES_VIGENTE = @month AND 
	                                  ARQUIVO.ANO_VIGENTE = @year AND 
	                                  ARQUIVO.ID_STATUS_ARQUIVO = @statusFile AND
                                      (@network = 0 OR  REDE.ID = @network) AND 
                                      DADOS.ID_ARQUIVO_VENDA_SKU_STATUS IN (3 , 5) AND
									  (SELECT TOP 1 SUPERTOP.PONTUACAO FROM PRODUTO_SUPERTOP AS SUPERTOP
			                                   WHERE SUPERTOP.ID_PRODUTO = DADOS.ID_PRODUTO AND
				                                     SUPERTOP.ID_REDE = ARQUIVO.ID_REDE AND
					                                 SUPERTOP.MES_VIGENTE = ARQUIVO.MES_VIGENTE AND
					                                 SUPERTOP.ANO_VIGENTE = ARQUIVO.ANO_VIGENTE AND
				                                     SUPERTOP.ATIVO = 1) IS NULL AND
									  (SELECT TOP 1 PARTICIPANTE.PONTUACAO FROM PRODUTO_PARTICIPANTE AS PARTICIPANTE
			                                   WHERE PARTICIPANTE.ID_PRODUTO = DADOS.ID_PRODUTO AND
				                                     PARTICIPANTE.ID_REDE = ARQUIVO.ID_REDE AND
					                                 PARTICIPANTE.MES_VIGENTE = ARQUIVO.MES_VIGENTE AND
					                                 PARTICIPANTE.ANO_VIGENTE = ARQUIVO.ANO_VIGENTE AND
				                                     PARTICIPANTE.ATIVO = 1)  IS NULL
								GROUP BY REDE.NOME,PRODUTO.SKU ,
	                                   FABRICANTE.NOME ,
	                                   CATEGORIA.NOME ,
	                                   PRODUTO.NOME ,
	                                   PRODUTO.SKU ,
									   DADOS.ID_PRODUTO,
									   ARQUIVO.ID_REDE,
									   ARQUIVO.MES_VIGENTE,
									   ARQUIVO.ANO_VIGENTE
                               ORDER BY REDE.NOME",
                               pYear,
                               pMonth,
                               pStart,
                               pLimit,
                               pNetwork,
                               pStatusFile).FirstAsync();

            var preProcessing = await _dbContext.Query<PreProcessingVM>().FromSql
                                    (@"SELECT REDE.NOME AS Network,
	                                   FABRICANTE.NOME AS Producer,
	                                   CATEGORIA.NOME AS Category,
	                                   PRODUTO.NOME AS Product,
	                                   PRODUTO.SKU AS Sku,
	                                   ISNULL((SELECT TOP 1 SUPERTOP.PONTUACAO FROM PRODUTO_SUPERTOP AS SUPERTOP
			                                   WHERE SUPERTOP.ID_PRODUTO = DADOS.ID_PRODUTO AND
				                                     SUPERTOP.ID_REDE = ARQUIVO.ID_REDE AND
					                                 SUPERTOP.MES_VIGENTE = ARQUIVO.MES_VIGENTE AND
					                                 SUPERTOP.ANO_VIGENTE = ARQUIVO.ANO_VIGENTE AND
				                                     SUPERTOP.ATIVO = 1), 0) SuperTop,

	                                   ISNULL((SELECT TOP 1 PARTICIPANTE.PONTUACAO FROM PRODUTO_PARTICIPANTE AS PARTICIPANTE
			                                   WHERE PARTICIPANTE.ID_PRODUTO = DADOS.ID_PRODUTO AND
				                                     PARTICIPANTE.ID_REDE = ARQUIVO.ID_REDE AND
					                                 PARTICIPANTE.MES_VIGENTE = ARQUIVO.MES_VIGENTE AND
					                                 PARTICIPANTE.ANO_VIGENTE = ARQUIVO.ANO_VIGENTE AND
				                                     PARTICIPANTE.ATIVO = 1), 0) Participant
                                FROM ARQUIVO_VENDA AS ARQUIVO
	                                INNER JOIN REDE AS REDE ON REDE.ID = ARQUIVO.ID_REDE
	                                INNER JOIN ARQUIVO_VENDA_DADOS AS DADOS ON DADOS.ID_ARQUIVO_VENDA = ARQUIVO.ID
	                                INNER JOIN ARQUIVO_VENDA_SKU_STATUS AS STATUS ON STATUS.ID = DADOS.ID_ARQUIVO_VENDA_SKU_STATUS
	                                LEFT JOIN PRODUTO AS PRODUTO ON PRODUTO.ID = DADOS.ID_PRODUTO
	                                LEFT JOIN CATEGORIA_PRODUTO AS CATEGORIA ON CATEGORIA.ID = PRODUTO.ID_PRODUTO_CATEGORIA
	                                LEFT JOIN FORNECEDOR AS FABRICANTE ON FABRICANTE.ID = PRODUTO.ID_FORNECEDOR
                                WHERE ARQUIVO.MES_VIGENTE = @month AND 
	                                  ARQUIVO.ANO_VIGENTE = @year AND 
	                                  ARQUIVO.ID_STATUS_ARQUIVO = @statusFile AND
                                      (@network = 0 OR  REDE.ID = @network) AND 
                                      DADOS.ID_ARQUIVO_VENDA_SKU_STATUS IN (3 , 5) AND
									  (SELECT TOP 1 SUPERTOP.PONTUACAO FROM PRODUTO_SUPERTOP AS SUPERTOP
			                                   WHERE SUPERTOP.ID_PRODUTO = DADOS.ID_PRODUTO AND
				                                     SUPERTOP.ID_REDE = ARQUIVO.ID_REDE AND
					                                 SUPERTOP.MES_VIGENTE = ARQUIVO.MES_VIGENTE AND
					                                 SUPERTOP.ANO_VIGENTE = ARQUIVO.ANO_VIGENTE AND
				                                     SUPERTOP.ATIVO = 1) IS NULL AND
									  (SELECT TOP 1 PARTICIPANTE.PONTUACAO FROM PRODUTO_PARTICIPANTE AS PARTICIPANTE
			                                   WHERE PARTICIPANTE.ID_PRODUTO = DADOS.ID_PRODUTO AND
				                                     PARTICIPANTE.ID_REDE = ARQUIVO.ID_REDE AND
					                                 PARTICIPANTE.MES_VIGENTE = ARQUIVO.MES_VIGENTE AND
					                                 PARTICIPANTE.ANO_VIGENTE = ARQUIVO.ANO_VIGENTE AND
				                                     PARTICIPANTE.ATIVO = 1)  IS NULL
								GROUP BY REDE.NOME,PRODUTO.SKU ,
	                                   FABRICANTE.NOME ,
	                                   CATEGORIA.NOME ,
	                                   PRODUTO.NOME ,
	                                   PRODUTO.SKU ,
									   DADOS.ID_PRODUTO,
									   ARQUIVO.ID_REDE,
									   ARQUIVO.MES_VIGENTE,
									   ARQUIVO.ANO_VIGENTE
                               ORDER BY REDE.NOME
                               OFFSET @start ROWS
                               FETCH NEXT @limit ROWS ONLY",
                               pYear,
                               pMonth,
                               pStart,
                               pLimit,
                               pNetwork,
                               pStatusFile).ToListAsync();

            return (preProcessing.Select(x =>
            new[] {
                x.Network,
                x.Producer,
                x.Category,
                x.Product,
                x.Sku,
                x.Participant.ToString(),
                x.SuperTop.ToString()
            }).ToArray(), preProcessingCount.Count);

        }
    }
}
