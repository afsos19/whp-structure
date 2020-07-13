using System;
using System.Collections.Generic;
using System.Text;

namespace Ssg.MaisSamsung.Business.Dto
{
    public class ShopAuthenticateDto
    {
        public int ClientId { get; set; }
        public int ProfileId { get; set; }
        public int ParticipantStatusId { get; set; }
        public int LogonTypeId { get; set; }
        public int ProjectId { get; set; }
        public Int64 ProjectConfigurationId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string CompanyName { get; set; }
        public string FantasyName { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; set; }
        public string IM { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public bool Active { get; set; }
        public string BirthDate { get; set; }
        public string InsertDate { get; set; }
        public string UpdateDate { get; set; }

        public int ExternalId { get; set; }
        public int LogonType { get; set; }
        public int ParticipantStatus { get; set; }
        public int PersonType { get; set; }

        public AddressDto Addresses { get; set; }
        public List<TelephonesDto> Telephones { get; set; }
        public List<EmailsDto> Emails { get; set; }
        public List<AcceptsDto> Accepts { get; set; }

    }
}
