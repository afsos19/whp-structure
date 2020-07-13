using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public UserStatusDto UserStatus { get; set; }
        public int UserStatusId { get; set; }
        public OfficeDto Office { get; set; }
        public int OfficeId { get; set; }
        public char CivilStatus { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime BithDate { get; set; }
        public string Phone { get; set; }
        public string CommercialPhone { get; set; }
        public string CellPhone { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public int SonAmout { get; set; }
        public string HeartTeam { get; set; }
        public string CEP { get; set; }
        public string Uf { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string ReferencePoint { get; set; }
        public string Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime OffIn { get; set; }
        public string AccessCode { get; set; }
        public string AccessCodeInvite { get; set; }
        public int Network { get; set; }
        public int Shop { get; set; }
        public bool PrivacyPolicy { get; set; }
    }
}
