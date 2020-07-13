using eaiSingleSignOn;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using static eaiSingleSignOn.eaiSingleSignOnSoapClient;

namespace Whp.MaisTop.Business.Services
{
    public class EaiSingleSignOnService : IEaiSingleSignOnService
    {
        public async Task<string> Authenticate(User user)
        {
            string token = "PROkdVW7NQwJp==";
            int idCampanha = 24;

            
            wsParticipante participante = new wsParticipante();
            participante.CPF = user.Cpf;
            participante.DataNascimento = $"{(user.BithDate.Value.Day < 10 ? "0"+user.BithDate.Value.Day.ToString() : user.BithDate.Value.Day.ToString())}/{(user.BithDate.Value.Month < 10 ? "0" + user.BithDate.Value.Month.ToString() : user.BithDate.Value.Month.ToString())}/{user.BithDate.Value.Year}";
            participante.Nome = user.Name;
            participante.Email = user.Email;
            participante.CEP = user.CEP;
            participante.Logradouro = user.Address;
            participante.Numero = user.Number.ToString();
            participante.Complemento = user.Complement;
            participante.Bairro = user.Neighborhood;
            participante.Cidade = user.City;
            participante.UF = user.Uf;
            participante.Sexo = user.Gender.ToString();
            participante.Telefone = user.Phone;
            participante.Celular = user.CellPhone;

            string hash = await CalculateSignature(participante.CPF, token);
            eaiSingleSignOnSoapClient client = new eaiSingleSignOnSoapClient(EndpointConfiguration.eaiSingleSignOnSoap);

             var retornoWS = await client.AutenticarAsync(hash, idCampanha,participante);

            if (retornoWS.Body.AutenticarResult.hasError)
                return "";
           
            return "http://www.eaipremiospp.com.br/CatalogoOnline/SSO/"+ retornoWS.Body.AutenticarResult.token;
        }

        public async Task<string> CalculateSignature(string stringToSign, string key)
        {
            return await Task.Run(() =>
             {
                 byte[] key_byte = Encoding.UTF8.GetBytes(key);
                 byte[] stringToSign_byte = Encoding.UTF8.GetBytes(stringToSign);
                 
                 HMACSHA256 hmac = new HMACSHA256(key_byte);
                 byte[] hashValue = hmac.ComputeHash(stringToSign_byte);
                 return BitConverter.ToString(hashValue).Replace("-", "");
             });
        }
    }
}
