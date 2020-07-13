using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class UserExcelDto
    {
        [DisplayName("STATUS USUARIO")]
        public string UserStatus { get; set; }
        [DisplayName("CNPJ")]
        public string Cnpj { get; set; }
        [DisplayName("REDE")]
        public string Network { get; set; }
        [DisplayName("CARGO")]
        public string Office { get; set; }
        [DisplayName("ESTADO CIVIL")]
        public string CivilStatus { get; set; }
        [DisplayName("NOME")]
        public string Name { get; set; }
        [DisplayName("CPF")]
        public string Cpf { get; set; }
        [DisplayName("DATA NASCIMENTO")]
        public string BithDate { get; set; }
        [DisplayName("TELEFONE")]
        public string Phone { get; set; }
        [DisplayName("TELEFONE COMERCIAL")]
        public string CommercialPhone { get; set; }
        [DisplayName("CELULAR")]
        public string CellPhone { get; set; }
        [DisplayName("SEXO")]
        public string Gender { get; set; }
        [DisplayName("EMAIL")]
        public string Email { get; set; }
        [DisplayName("SENHA")]
        public string Password { get; set; }
        [DisplayName("QT. FILHOS")]
        public int SonAmout { get; set; }
        [DisplayName("TIME")]
        public string HeartTeam { get; set; }
        [DisplayName("CEP")]
        public string CEP { get; set; }
        [DisplayName("ESTADO")]
        public string Uf { get; set; }
        [DisplayName("CIDADE")]
        public string City { get; set; }
        [DisplayName("BAIRRO")]
        public string Neighborhood { get; set; }
        [DisplayName("ENDERECO")]
        public string Address { get; set; }
        [DisplayName("NUMERO")]
        public int Number { get; set; }
        [DisplayName("COMPLEMENTO")]
        public string Complement { get; set; }
        [DisplayName("PONTO REFERENCIA")]
        public string ReferencePoint { get; set; }
        [DisplayName("FOTO")]
        public string Photo { get; set; }
        [DisplayName("CRIADO EM")]
        public string CreatedAt { get; set; }
        [DisplayName("SENHA RECUPERADA EM")]
        public string PasswordRecoveredAt { get; set; }
        [DisplayName("DESLIGADO EM")]
        public string OffIn { get; set; }
        [DisplayName("ATIVO")]
        public string Activated { get; set; }
        [DisplayName("ACEITO TERMO")]
        public string PrivacyPolicy { get; set; }
        [DisplayName("COD. CONVITE AMIGO")]
        public string AccessCodeInvite { get; set; }
    }
}
