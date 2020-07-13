using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class User : BaseEntity
    {
        public UserStatus UserStatus { get; set; }
        public Office Office { get; set; }
        public char CivilStatus { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime? BithDate { get; set; }
        public string Phone { get; set; }
        public string CommercialPhone { get; set; }
        public string CellPhone { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
        public DateTime? PasswordRecoveredAt { get; set; }
        public DateTime? OffIn { get; set; }
        public bool Activated { get; set; }
        public bool PrivacyPolicy { get; set; }
        public string AccessCodeInvite { get; set; }
    }
}
