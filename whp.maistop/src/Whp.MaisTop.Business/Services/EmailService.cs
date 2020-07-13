using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Whp.MaisTop.Business.Configurations;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Utils;

namespace Whp.MaisTop.Business.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailConfiguration _emailConfiguration;
        private readonly IHostingEnvironment _env;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public EmailService(EmailConfiguration emailConfiguration, IHostingEnvironment env)
        {
            _emailConfiguration = emailConfiguration;
            _env = env;
        }

        public bool SenderForgotPassword(string login, string name, string email, string senha)
        {
            var retorno = false;

            var html = System.IO.File.ReadAllText(Path.Combine(_env.WebRootPath, $"Content/EmailHtml/AlteracaoSenha/AlteracaoSenha.html"));

            html = html.Replace("#NOME#", name).Replace("#SENHA#", Crypto.Decrypt(senha, Crypto.Key256, 256));

            using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
            {

                client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                client.EnableSsl = true;

                var msg = new MailMessage(_emailConfiguration.HostSender, email, "Programa +TOP - Recuperação de Senha", html) { IsBodyHtml = true };

                client.Send(msg);
                retorno = true;

                return retorno;
            }
        }


        public bool SendAccessCodeExpiration(string code, string name, string email)
        {
            var retorno = false;

            var html = System.IO.File.ReadAllText(Path.Combine(_env.WebRootPath, $"Content/EmailHtml/EmailTokenSenha/EmailTokenSenha.html"));

            html = html.Replace("#NOME#", name);
            html = html.Replace("#CODIGO#", code);

            using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
            {

                client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                client.EnableSsl = true;

                var msg = new MailMessage(_emailConfiguration.HostSender, email, "Programa +TOP - Validação de Segurança", html) { IsBodyHtml = true };

                client.Send(msg);
                retorno = true;

                return retorno;
            }
        }

        public bool SendAPasswordExpiration(string password, string name, string email)
        {
            var retorno = false;

            var html = System.IO.File.ReadAllText(Path.Combine(_env.WebRootPath, $"Content/EmailHtml/AlteracaoSenha/AlteracaoSenha.html"));

            html = html.Replace("#NOME#", name);
            html = html.Replace("#SENHA#", password);

            using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
            {

                client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                client.EnableSsl = true;

                var msg = new MailMessage(_emailConfiguration.HostSender, email, "Programa +TOP - Senha Cadastrada", html) { IsBodyHtml = true };

                client.Send(msg);
                retorno = true;

                return retorno;
            }
        }

        public bool SendConfirmation(string login, string name, string email)
        {
            var retorno = false;

            var html = System.IO.File.ReadAllText(Path.Combine(_env.WebRootPath, $"Content/EmailHtml/Confirmation/EmailConfirmation.html"));

            html = html.Replace("[NOME]", name);

            using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
            {

                client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                client.EnableSsl = true;

                var msg = new MailMessage(_emailConfiguration.HostSender, email, "Confirmação de cadastro", html) { IsBodyHtml = true };

                client.Send(msg);
                retorno = true;

                return retorno;
            }
        }
        public bool SendSKUEnabled(string networkName)
        {
            var retorno = false;

            using (var smtpClient = new SmtpClient())
            {

                var html = System.IO.File.ReadAllText(Path.Combine("D:/Producao/programamaistop.com.br/api", $"wwwroot/Content/EmailHtml/SendSKUEnabled/SendSKUEnabled.html"));
                html = html.Replace("#REDE#", networkName);

                using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
                {

                    client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                    client.EnableSsl = true;

                    var msg = new MailMessage(_emailConfiguration.HostSender, "rhuscaya.silva@fullbar.com.br", "Planilha de vendas processada com sucesso", html) { IsBodyHtml = true };
                    msg.Bcc.Add("atendimentogi@programamaistop.com.br");
                    msg.Bcc.Add("amanda.silva@fullbar.com.br");
                    msg.Bcc.Add("atendimentogi@programamaistop.com.br");

                    client.Send(msg);
                    retorno = true;

                    return retorno;
                }
            }

        }
        public bool SendSaleError(string email)
        {
            var retorno = false;

            using (var smtpClient = new SmtpClient())
            {

                var html = System.IO.File.ReadAllText(Path.Combine("D:/Producao/programamaistop.com.br/api", $"wwwroot/Content/EmailHtml/SendSaleError/SendSaleError.html"));

                using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
                {

                    client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                    client.EnableSsl = true;

                    var msg = new MailMessage(_emailConfiguration.HostSender, email, "Erro no processamento da planilha de vendas", html) { IsBodyHtml = true };
                    msg.Bcc.Add("atendimentogi@programamaistop.com.br");

                    client.Send(msg);
                    retorno = true;

                    return retorno;
                }
            }

        }
        public bool SendSaleSuccess(string monthYear, string email)
        {
            var retorno = false;

            using (var smtpClient = new SmtpClient())
            {

                var html = System.IO.File.ReadAllText(Path.Combine("D:/Producao/programamaistop.com.br/api", $"wwwroot/Content/EmailHtml/SendSaleSuccess/SendSaleSuccess.html"));


                html = html.Replace("#MESANO#", monthYear);


                using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
                {

                    client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                    client.EnableSsl = true;

                    var msg = new MailMessage(_emailConfiguration.HostSender, email, "Importação da planilha concluida", html) { IsBodyHtml = true };
                    msg.Bcc.Add("atendimentogi@programamaistop.com.br");

                    client.Send(msg);
                    retorno = true;

                    return retorno;
                }
            }

        }
        public bool SendHierarchyError(string email)
        {
            var retorno = false;

            using (var smtpClient = new SmtpClient())
            {

                var html = System.IO.File.ReadAllText(Path.Combine("D:/Producao/programamaistop.com.br/api", $"wwwroot/Content/EmailHtml/SendHierarchyError/SendHierarchyError.html"));

                using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
                {

                    client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                    client.EnableSsl = true;

                    var msg = new MailMessage(_emailConfiguration.HostSender, email, "Erro no processamento da planilha de hierarquia", html) { IsBodyHtml = true };
                    msg.Bcc.Add("atendimentogi@programamaistop.com.br");

                    client.Send(msg);
                    retorno = true;

                    return retorno;
                }
            }

        }
        public bool SendHierarchySuccess(string monthYear, string email)
        {
            var retorno = false;

            using (var smtpClient = new SmtpClient())
            {

                var html = System.IO.File.ReadAllText(Path.Combine("D:/Producao/programamaistop.com.br/api", $"wwwroot/Content/EmailHtml/SendHierarchySuccess/SendHierarchySuccess.html"));


                html = html.Replace("#MESANO#", monthYear);


                using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
                {

                    client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                    client.EnableSsl = true;

                    var msg = new MailMessage(_emailConfiguration.HostSender, email, "Importação da planilha concluida", html) { IsBodyHtml = true };
                    msg.Bcc.Add("atendimentogi@programamaistop.com.br");

                    client.Send(msg);
                    retorno = true;

                    return retorno;
                }
            }

        }
        public bool SendSale(string month, int stepId, string email)
        {
            var retorno = false;

            using (var smtpClient = new SmtpClient())
            {

                var html = System.IO.File.ReadAllText("caminhoDoHtml");

                if (stepId == 7)
                    html = html.Replace("#MES#", month);


                using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
                {

                    client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                    client.EnableSsl = true;


                    var msg = new MailMessage(_emailConfiguration.HostSender, email, "assunto", html) { IsBodyHtml = true };
                    msg.Bcc.Add("atendimentogi@programamaistop.com.br");

                    if (stepId == 7)
                        msg.Bcc.Add("EmailDaWhirpool");

                    client.Send(msg);
                    retorno = true;

                    return retorno;
                }
            }


        }
        public bool SendSaleWhirpool(string network)
        {
            var retorno = false;

            using (var smtpClient = new SmtpClient())
            {

                var html = System.IO.File.ReadAllText("caminhoDoHtmlAqui");

                html = html.Replace("#REDE#", network);

                using (var client = new SmtpClient(_emailConfiguration.HostSMTP, Convert.ToInt16(_emailConfiguration.HostPORT)))
                {

                    client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPassword);
                    client.EnableSsl = true;


                    var msg = new MailMessage(_emailConfiguration.HostSender, "emailDaWhirpool", "assunto aqui", html) { IsBodyHtml = true };
                    msg.Bcc.Add("atendimentogi@programamaistop.com.br");

                    client.Send(msg);
                    retorno = true;

                    return retorno;
                }
            }
        }
    }
}
