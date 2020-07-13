using System;

namespace Whp.MaisTop.Domain.Entities
{
    public class LogsPunctuation : BaseEntity
    {
       
        public int? ID_USUARIO { get; set; }
        public int? ID_ORIGEM_PONTUACAO { get; set; }
        public string TIPO_OPERACAO { get; set; }
        public int? ID_ENTIDADE_REFERENTE { get; set; }
        public int? MES_VIGENTE { get; set; }
        public int? ANO_VIGENTE { get; set; }
        public string DESCRICAO { get; set; }
        public decimal? PONTUACAO { get; set; }
        public DateTime? CRIADO_EM { get; set; }
        public string USUARIO_BANCO { get; set; }
        public string OPERACAO { get; set; }
    }
}
