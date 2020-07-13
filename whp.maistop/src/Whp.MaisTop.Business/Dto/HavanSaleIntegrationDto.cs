using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class HavanSaleIntegrationDto
    {
        public string Mensagem { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
    }
    public class Lote
    {
        public DateTime DataProcessamento { get; set; }
        public DateTime DataVenda { get; set; }
        public int Situacao { get; set; }
        public IEnumerable<Item> Itens { get; set; }
    }
    public class Item
    {
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }
        public Vendedor Vendedor { get; set; }
    }
    public class Produto
    {
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public string CodigoHavan { get; set; }
        public string Ean { get; set; }
    }
    public class Venda
    {
        public DateTime Data { get; set; }
        public int CaixaSerie { get; set; }
        public int NumeroCupomNota { get; set; }
        public string CnpjEmissao { get; set; }
        public string CnpjLoja { get; set; }
        public int Quantidade { get; set; }
        public char Tipo { get; set; }
    }
    public class Vendedor
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
