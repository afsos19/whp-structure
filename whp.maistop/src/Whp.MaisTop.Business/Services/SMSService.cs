using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Whp.MaisTop.Business.Configurations;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Utils;

namespace Whp.MaisTop.Business.Services
{
    public class SMSService : ISMSService
    {

        private readonly SMSConfiguration _SMSConfiguration;

        public SMSService(SMSConfiguration SMSConfiguration)
        {
            _SMSConfiguration = SMSConfiguration;
        }

        public (string Id, string Code, string Text) Send(string celular, string link)
        {
            var url = _SMSConfiguration.UrlAkna;
            var user = _SMSConfiguration.User;
            var pass = _SMSConfiguration.Pass;

            var mensagem = "Olá, você foi indicado para fazer parte do Programa Mais Top. Para participar basta clicar neste link #LINK#";
            mensagem = mensagem.Replace("#LINK#", link);

            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);

            var xml = EncondingUtf8(String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                    "<main>" +
                                    "<emkt trans=\"40.01\">" +
                                    "<remetente>WHP</remetente>" +
                                    "<sms>" +
                                    "<telefone>{0}</telefone>" +
                                    "<mensagem>{1}</mensagem>" +
                                    "</sms>" +
                                    "</emkt>" +
                                    "</main>", celular, mensagem));

            var postData = String.Format("User={0}&Pass={1}&XML={2}", user, pass, xml);
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);

            var response = client.Execute(request).Content;
            var objRetorno = XDocument.Parse(response).Descendants("RETURN").FirstOrDefault();
            var id = (objRetorno != null) ? objRetorno.Attribute("ID").Value : String.Empty;
            var codigo = (objRetorno != null) ? objRetorno.Value : String.Empty;

            return (id, codigo, mensagem );

        }

        public (string Id, string Code, string Text) SendConfirmation(string celular, bool isIndicador)
        {
            var url = _SMSConfiguration.UrlAkna;
            var user = _SMSConfiguration.User;
            var pass = _SMSConfiguration.Pass;

            var mensagem = (!isIndicador) ? "Parabéns, uma indicação sua finalizou o cadastro no Mais Top! Você recebeu 10 pontos, continue indicando novos participantes!" : /*"Bem vindo ao Mais Top! Você recebeu 5 pontos por efetuar seu cadastro."*/ "Bem-vindo ao +TOP! Acesse o site ou o aplicativo com o CPF e a senha cadastrada, e saiba tudo sobre o Programa: https://programamaistop.com.br/";

            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);

            var xml = EncondingUtf8(String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                    "<main>" +
                                    "<emkt trans=\"40.01\">" +
                                    "<remetente>WHP</remetente>" +
                                    "<sms>" +
                                    "<telefone>{0}</telefone>" +
                                    "<mensagem>{1}</mensagem>" +
                                    "</sms>" +
                                    "</emkt>" +
                                    "</main>", celular, mensagem));


            var postData = String.Format("User={0}&Pass={1}&XML={2}", user, pass, xml);
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);

            var response = client.Execute(request).Content;
            var objRetorno = XDocument.Parse(response).Descendants("RETURN").FirstOrDefault();
            var id = (objRetorno != null) ? objRetorno.Attribute("ID").Value : String.Empty;
            var codigo = (objRetorno != null) ? objRetorno.Value : String.Empty;

            return (id, codigo, mensagem);

        }

        public (string Phone, string Status) VerifySend(string codigoEnvio)
        {
            var url = _SMSConfiguration.UrlAkna;
            var user = _SMSConfiguration.User;
            var pass = _SMSConfiguration.Pass;

            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);

            var xml = EncondingUtf8(String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                    "<main>" +
                                    "<emkt trans=\"40.02\">" +
                                    "<sms>" +
                                    "<codigo>{0}</codigo>" +
                                    "</sms>" +
                                    "</emkt>" +
                                    "</main>", codigoEnvio));


            var postData = String.Format("User={0}&Pass={1}&XML={2}", user, pass, xml);
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);

            var response = client.Execute(request).Content;

            var objTelefone = XDocument.Parse(response).Descendants("TELEFONE").FirstOrDefault();
            var telefone = (objTelefone != null) ? objTelefone.Value : String.Empty;

            var objStatus = XDocument.Parse(response).Descendants("STATUS").FirstOrDefault();
            var status = (objStatus != null) ? objStatus.Value.ToUpper() : String.Empty;

            return ( telefone, status);

        }

        public (string Id, string Code, string Text) SendForgotPassword(string celular, string senha)
        {
            var url = _SMSConfiguration.UrlAkna;
            var user = _SMSConfiguration.User;
            var pass = _SMSConfiguration.Pass;

            var mensagem = "Você solicitou o reenvio de sua senha no Mais Top. Acesse e continue participando! #SENHA#";
            mensagem = mensagem.Replace("#SENHA#", senha);

            var client = new RestClient(url);
            var id = "";
            var codigo = "";
            var request = new RestRequest(Method.POST);

            var xml = EncondingUtf8(String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                    "<main>" +
                                    "<emkt trans=\"40.01\">" +
                                    "<remetente>WHP</remetente>" +
                                    "<sms>" +
                                    "<telefone>{0}</telefone>" +
                                    "<mensagem>{1}</mensagem>" +
                                    "</sms>" +
                                    "</emkt>" +
                                    "</main>", Regex.Replace(celular, @"[^\d]", ""), mensagem));

            var postData = String.Format("User={0}&Pass={1}&XML={2}", user, pass, xml.Replace("&", "&amp;"));
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var content = response.Content;

                XmlTextReader reader = new XmlTextReader(new StringReader(content));
                var objRetorno = XDocument.Parse(content).Descendants("RETURN").FirstOrDefault();
                id = (objRetorno != null) ? objRetorno.Attribute("ID").Value : String.Empty;
                codigo = (objRetorno != null) ? objRetorno.Value : String.Empty;
            }
            
            return ( id, codigo, mensagem );

        }

        public static string EncondingUtf8(string texto)
        {
            var enc = new UTF8Encoding(true, true);
            var bytes = enc.GetBytes(texto);
            return enc.GetString(bytes);
        }

        public string EncurtadorUrl(string url)
        {
            var key = _SMSConfiguration.ShortUrl;
            var post = "{\"longUrl\": \"" + url + "\"}";
            var shortUrl = url;
            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url?key=" + key);

            try
            {
                request.ServicePoint.Expect100Continue = false;
                request.Method = "POST";
                request.ContentLength = post.Length;
                request.ContentType = "application/json";
                request.Headers.Add("Cache-Control", "no-cache");

                using (var requestStream = request.GetRequestStream())
                {
                    var postBuffer = Encoding.ASCII.GetBytes(post);
                    requestStream.Write(postBuffer, 0, postBuffer.Length);
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var responseReader = new StreamReader(responseStream))
                        {
                            var json = responseReader.ReadToEnd();
                            shortUrl = Regex.Match(json, @"""id"": ?""(?<id>.+)""").Groups["id"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if Google's URL Shortner is down...
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return shortUrl;
        }


        public (string Id, string Code, string Text) SendAccessExpiration(string celular, string codigo)
        {
            var url = _SMSConfiguration.UrlAkna;
            var user = _SMSConfiguration.User;
            var pass = _SMSConfiguration.Pass;

            var mensagem = $"WHP: ATUALIZACAO DE CADASTRO +TOP: digite o codigo {codigo} para validar o cadastro da sua nova senha e continuar participando do Programa!";

            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);

            var xml = EncondingUtf8(String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                    "<main>" +
                                    "<emkt trans=\"40.01\">" +
                                    "<remetente>WHP</remetente>" +
                                    "<sms>" +
                                    "<telefone>{0}</telefone>" +
                                    "<mensagem>{1}</mensagem>" +
                                    "</sms>" +
                                    "</emkt>" +
                                    "</main>", celular, mensagem));

            var postData = String.Format("User={0}&Pass={1}&XML={2}", user, pass, xml);
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);

            var response = client.Execute(request).Content;
            var objRetorno = XDocument.Parse(response).Descendants("RETURN").FirstOrDefault();
            var id = (objRetorno != null) ? objRetorno.Attribute("ID").Value : String.Empty;
            var codigoRetorno = (objRetorno != null) ? objRetorno.Value : String.Empty;

            return (id, codigoRetorno, mensagem);

        }

        public (string Id, string Code, string Text) SendAccessConfirmation(string celular, string codigo)
        {
            var url = _SMSConfiguration.UrlAkna;
            var user = _SMSConfiguration.User;
            var pass = _SMSConfiguration.Pass;

            var mensagem = $"ATUALIZAÇÃO DE CADASTRO Mais TOP: digite o código {codigo} para validar seu cadastro e garantir sua participação no Programa!";
            
            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);

            var xml = EncondingUtf8(String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                    "<main>" +
                                    "<emkt trans=\"40.01\">" +
                                    "<remetente>WHP</remetente>" +
                                    "<sms>" +
                                    "<telefone>{0}</telefone>" +
                                    "<mensagem>{1}</mensagem>" +
                                    "</sms>" +
                                    "</emkt>" +
                                    "</main>", celular, mensagem));

            var postData = String.Format("User={0}&Pass={1}&XML={2}", user, pass, xml);
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);

            var response = client.Execute(request).Content;
            var objRetorno = XDocument.Parse(response).Descendants("RETURN").FirstOrDefault();
            var id = (objRetorno != null) ? objRetorno.Attribute("ID").Value : String.Empty;
            var codigoRetorno = (objRetorno != null) ? objRetorno.Value : String.Empty;

            return (id, codigoRetorno, mensagem);

        }

        public (string Id, string Code, string Text) SendAccessInvite(string celular, string codigo)
        {
            var url = _SMSConfiguration.UrlAkna;
            var user = _SMSConfiguration.User;
            var pass = _SMSConfiguration.Pass;

            var mensagem = $"+TOP INFORMA: Codigo de indicacao {codigo} comece agora seu cadastro em www.programamaistop.com.br/CadastroConvidado";

            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);

            var xml = EncondingUtf8(String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                    "<main>" +
                                    "<emkt trans=\"40.01\">" +
                                    "<remetente>WHP</remetente>" +
                                    "<sms>" +
                                    "<telefone>{0}</telefone>" +
                                    "<mensagem>{1}</mensagem>" +
                                    "</sms>" +
                                    "</emkt>" +
                                    "</main>", celular, mensagem));

            var postData = String.Format("User={0}&Pass={1}&XML={2}", user, pass, xml);
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);

            var response = client.Execute(request).Content;
            var objRetorno = XDocument.Parse(response).Descendants("RETURN").FirstOrDefault();
            var id = (objRetorno != null) ? objRetorno.Attribute("ID").Value : String.Empty;
            var codigoRetorno = (objRetorno != null) ? objRetorno.Value : String.Empty;

            return ( id,  codigoRetorno,  mensagem );

        }


    }
}
