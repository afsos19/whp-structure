using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.ToTable("USUARIO");

            modelBuilder
                .Property(p => p.Id)
                .UseSqlServerIdentityColumn()
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Address)
                .HasColumnName("ENDERECO");

            modelBuilder
                .Property(p => p.Uf)
                .HasColumnName("UF");

            modelBuilder
                .Property(p => p.BithDate)
                .HasColumnName("DATA_NASCIMENTO");

            modelBuilder
                .Property(p => p.CellPhone)
                .HasColumnName("CELULAR");

            modelBuilder
                .Property(p => p.Password)
                .HasColumnName("SENHA");

            modelBuilder
                .Property(p => p.Email)
                .HasColumnName("EMAIL");

            modelBuilder
                .Property(p => p.CEP)
                .HasColumnName("CEP");

            modelBuilder
                .Property(p => p.City)
                .HasColumnName("CIDADE");

            modelBuilder
                .Property(p => p.CivilStatus)
                .HasColumnName("ESTADO_CIVIL");

            modelBuilder
                .Property(p => p.CommercialPhone)
                .HasColumnName("TELEFONE_COMERCIAL");

            modelBuilder
                .Property(p => p.Complement)
                .HasColumnName("COMPLEMENTO");

            modelBuilder
                .Property(p => p.Cpf)
                .HasColumnName("CPF");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.Gender)
                .HasColumnName("GENERO");

            modelBuilder
                .Property(p => p.HeartTeam)
                .HasColumnName("TIME_CORACAO");

            modelBuilder
                .Property(p => p.Name)
                .HasColumnName("NOME");

            modelBuilder
                .Property(p => p.Neighborhood)
                .HasColumnName("BAIRRO");

            modelBuilder
                .Property(p => p.Number)
                .HasColumnName("NUMERO");

            modelBuilder
                .Property("OfficeId")
                .HasColumnName("ID_CARGO");

            modelBuilder
                .Property(p => p.OffIn)
                .HasColumnName("DESLIGADO_EM");

            modelBuilder
                .Property(p => p.Phone)
                .HasColumnName("TELEFONE");

            modelBuilder
                .Property(p => p.Photo)
                .HasColumnName("FOTO");

            modelBuilder
                .Property(p => p.ReferencePoint)
                .HasColumnName("PONTO_REFERENCIA");

            modelBuilder
                .Property(p => p.SonAmout)
                .HasColumnName("QUANTIDADE_FILHOS");
            
            modelBuilder
                .Property("UserStatusId")
                .HasColumnName("ID_USUARIO_STATUS");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property(p => p.PrivacyPolicy)
                .HasColumnName("POLITICA_PRIVACIDADE");

            modelBuilder
                .Property(p => p.PasswordRecoveredAt)
                .HasColumnName("RECUPERADO_SENHA_EM");

            modelBuilder
                .Property(p => p.AccessCodeInvite)
                .HasColumnName("CODIGO_ACESSO_CONVITE");

        }
    }
}
