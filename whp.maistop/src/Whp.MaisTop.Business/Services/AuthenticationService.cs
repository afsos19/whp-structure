using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using Ssg.MaisSamsung.Business.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Dto.BrasilCt;
using Whp.MaisTop.Business.Dto.TrainingAcademy;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Utils;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IUserAccessCodeConfirmationRepository _userAccessCodeConfirmationRepository;
        private readonly ISMSService _SMSService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IHostingEnvironment _env;
        private readonly ILogger _logger;
        private readonly IUserStatusLogRepository _userStatusLogRepository;
        private readonly IUserAccessLogRepository _userAccessLogRepository;
        private readonly IUserAccessCodeInviteRepository _userAccessCodeInviteRepository;
        private readonly IUserPunctuationSourceRepository _userPunctuationSourceRepository;
        private readonly IUserPunctuationRepository _userPunctuationRepository;
        private readonly IEaiSingleSignOnService _eaiSingleSignOnService;

        public AuthenticationService(
            IEaiSingleSignOnService eaiSingleSignOnService,
            IMapper mapper,
            IUserPunctuationRepository userPunctuationRepository,
            IUserPunctuationSourceRepository userPunctuationSourceRepository,
            IUserAccessCodeInviteRepository userAccessCodeInviteRepository,
            IUserStatusLogRepository userStatusLogRepository,
            IUserRepository userRepository,
            IUserAccessLogRepository userAccessLogRepository,
            IUserStatusRepository userStatusRepository,
            IUserAccessCodeConfirmationRepository userAccessCodeConfirmationRepository,
            ISMSService SMSService,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IShopUserRepository shopUserRepository,
            ILogger logger,
            IHostingEnvironment env)
        {
            _eaiSingleSignOnService = eaiSingleSignOnService;
            _mapper = mapper;
            _userAccessCodeInviteRepository = userAccessCodeInviteRepository;
            _userPunctuationSourceRepository = userPunctuationSourceRepository;
            _userPunctuationRepository = userPunctuationRepository;
            _userAccessLogRepository = userAccessLogRepository;
            _userStatusLogRepository = userStatusLogRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
            _userAccessCodeConfirmationRepository = userAccessCodeConfirmationRepository;
            _SMSService = SMSService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailService = emailService;
            _shopUserRepository = shopUserRepository;
            _env = env;
        }

        public async Task<(UserDto user, bool authenticated, string messageReturning, string link)> DoLogin(LoginDto loginDto, string ip)
        {

            var userDto = new UserDto();

            var userAuthenticated = await _userRepository.AuthenticateUser(loginDto.Cpf, Crypto.Encrypt(loginDto.Password, Crypto.Key256, 256));

            if (userAuthenticated == null)
                return (userDto, false, "Usuário ou senha inválido", "");

            _mapper.Map(userAuthenticated, userDto);

            var shopUser = (await _shopUserRepository.CustomFind(x => x.User.Id == userDto.Id, x => x.Shop, x => x.Shop.Network)).FirstOrDefault();

            if (shopUser == null)
                return (userDto, false, "Usuário sem loja cadastrada", "");

            userDto.Network = shopUser.Shop.Network.Id;

            userDto.Shop = shopUser.Shop.Id;

            if (userAuthenticated.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog)
                return (userDto, false, "Usuario somente catalogo", await GetShopAuthenticate(userDto.Id, userDto.Network));

            if (userAuthenticated.UserStatus.Id == (int)UserStatusEnum.Active)
                return (userDto, true, "Usuário encontrado com sucesso", "");
            else if (userAuthenticated.UserStatus.Id == (int)UserStatusEnum.PasswordExpired)
                return (userDto, true, "Senha expirada!", "");
            else
                return (userDto, false, "Usuario inativo ou desligado", "");

        }
        private async Task FriendInviteSetPunctuation(User user)
        {
            var friendInviteSource = await _userPunctuationSourceRepository.GetById((int)UserPunctuationSourceEnum.FriendInvitation);

            _userPunctuationRepository.Save(new UserPunctuation
            {
                CreatedAt = DateTime.Now,
                CurrentMonth = DateTime.Now.Month,
                CurrentYear = DateTime.Now.Year,
                Description = "CONVITE DE AMIGOS - CONVIDADO",
                OperationType = 'C',
                Punctuation = 5,
                ReferenceEntityId = 0,
                User = user,
                UserPunctuationSource = friendInviteSource

            });

            var participant = (await _userAccessCodeInviteRepository.CustomFind(x => x.Code.Equals(user.AccessCodeInvite), x => x.User)).Select(x => x.User).FirstOrDefault();

            _userPunctuationRepository.Save(new UserPunctuation
            {
                CreatedAt = DateTime.Now,
                CurrentMonth = DateTime.Now.Month,
                CurrentYear = DateTime.Now.Year,
                Description = "CONVITE DE AMIGOS - PARTICIPANTE",
                OperationType = 'C',
                Punctuation = 10,
                ReferenceEntityId = 0,
                User = participant,
                UserPunctuationSource = friendInviteSource
            });
        }
        public async Task<(UserDto user, bool authenticated, string messageReturning)> DoPreRegistration(UserDto userDto, IFormFile file)
        {
            var user = (await _userRepository.CustomFind(x => x.Id == userDto.Id, x => x.Office, x => x.UserStatus)).First();
            var lastStatus = user.UserStatus;
            var accessCodeUser = await _userAccessCodeConfirmationRepository.GetAccessCode(user.Id);

            if (accessCodeUser == null)
                return (userDto, false, "Código de acesso não encontrado");

            if (accessCodeUser.Code != userDto.AccessCode)
                return (userDto, false, "Código de acesso inválido");

            if (user.UserStatus.Id != (int)UserStatusEnum.PreRegistration)
                return (userDto, false, "Usuário não encontra-se no estado de pré cadastro!");

            if (string.IsNullOrEmpty(userDto.Password))
                return (userDto, false, "informe uma senha");

            if (file != null)
            {
                if (file.Length > 0 && file.Length <= 2000000)
                {
                    var extensions = Path.GetFileName(file.FileName).Split('.').Last();

                    if (extensions.ToUpper() != "JPG" && extensions.ToUpper() != "JPEG" && extensions.ToUpper() != "PNG")
                    {

                        return (userDto, false, "Foto perfil formato inválido");
                    }

                    var imageName = $"{DateTime.Now.Year}" +
                        $"{DateTime.Now.Month.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Day.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Minute.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Second.ToString().PadLeft(2, '0')}.{extensions}";

                    var path = Path.Combine(_env.WebRootPath, $"Content/PhotoPerfil/{Path.GetFileName(imageName)}");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    userDto.Photo = imageName;

                }
                else
                {
                    return (userDto, false, "Foto perfil tem limite de tamanho 2MB");
                }
            }

            _mapper.Map(userDto, user);

            user.Password = Crypto.Encrypt(userDto.Password, Crypto.Key256, 256);
            user.Activated = true;
            user.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.Active);

            _userStatusLogRepository.Save(new UserStatusLog
            {
                CreatedAt = DateTime.Now,
                User = user,
                UserStatusTo = user.UserStatus,
                UserStatusFrom = lastStatus,
                Description = "Pre cadastro"
            });

            var shopUser = (await _shopUserRepository.CustomFind(x => x.User.Id == userDto.Id, x => x.Shop, x => x.Shop.Network)).FirstOrDefault();

            if (shopUser == null)
                return (userDto, false, "Usuario sem loja cadastrada!");

            userDto.Network = shopUser.Shop.Network.Id;

            await _unitOfWork.CommitAsync();

            _mapper.Map(user, userDto);

            _emailService.SendConfirmation(user.Cpf, user.Name, user.Email);

            if (!string.IsNullOrEmpty(user.AccessCodeInvite))
                await FriendInviteSetPunctuation(user);

            return (userDto, true, "Usuario atualizado com sucesso!");

        }

        public async Task<(bool found, string message, UserDto user)> FirstAccess(string cpf)
        {
            var user = (await _userRepository.CustomFind(x => x.Cpf == cpf, x => x.UserStatus, x => x.Office)).FirstOrDefault();
            var userDto = new UserDto();

            if (user == null)
                return (false, "Usuario não encontrado", userDto);

            if (user.UserStatus.Id != (int)UserStatusEnum.PreRegistration)
                return (false, "Usuário não encontra-se no estado de pré cadastro!", userDto);

            _mapper.Map(user, userDto);

            return (true, "Usuario encontrado", userDto);
        }

        public async Task<(bool sent, string message)> ForgotPassword(string cpf)
        {
            var user = (await _userRepository.CustomFind(x => x.Cpf.Equals(cpf), x => x.UserStatus)).FirstOrDefault();

            if (user == null)
            {
                _logger.Warn($"Tentativa de recuperacao de senha com cpf {cpf} - CPF não encontrado");
                return (false, "CPF não encontrado");
            }

            if (user.PasswordRecoveredAt != null && user.PasswordRecoveredAt != DateTime.MinValue && (DateTime.Now - (DateTime)user.PasswordRecoveredAt).Minutes < 5)
            {
                _logger.Warn($"Tentativa de recuperacao de senha com cpf {cpf} - usuario ja solicitou envio em menos de 5 minutos");
                return (false, "Você já solicitou o envio do token para recuperar sua nova senha. Aguarde o recebimento ou tente novamente dentro de alguns minutos.");
            }

            if (user.UserStatus.Id == (int)UserStatusEnum.PreRegistration)
            {
                _logger.Warn($"Tentativa de recuperacao de senha com cpf {cpf} - Usuario em pre cadastro, faça o primeiro acesso!");
                return (false, "Usuario em pre cadastro, faça o primeiro acesso!");
            }


            if (string.IsNullOrEmpty(user.Cpf) || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                _logger.Warn($"Tentativa de recuperacao de senha com cpf {cpf} - usuario com informações faltantes no cadastro!");
                return (false, "Usuario com informações faltantes no cadastro!");
            }

            bool emailSent = false;

            try
            {
                emailSent = _emailService.SenderForgotPassword(user.Cpf, user.Name, user.Email, user.Password);
            }
            catch
            {
                _logger.Fatal($"Tentativa de recuperacao de senha com cpf {cpf} - falha ao tentar enviar Email. ");

                return (true, $"Tentativa de recuperacao de senha com cpf {cpf} - falha ao tentar enviar Email. ");
            }

            if (!emailSent)
                return (false, "Erro ao tenta enviar email");

            try
            {
                _SMSService.SendForgotPassword(user.CellPhone, Crypto.Decrypt(user.Password, Crypto.Key256, 256));
            }
            catch
            {
                _logger.Fatal($"Tentativa de recuperacao de senha com cpf {cpf} - falha ao tentar enviar SMS. Enviado senha apenas para email {user.Email}");

                return (true, $"Tentativa de recuperacao de senha com cpf {cpf} - falha ao tentar enviar SMS. Enviado senha apenas para email {user.Email}");
            }

            user.PasswordRecoveredAt = DateTime.Now;

            await _unitOfWork.CommitAsync();

            return (true, $"Senha enviada para o numero {user.CellPhone} e para o email {user.Email}");
        }

        public async Task<(string code, string message, bool sent)> SendSMSAccessCodeConfirmation(UserDto user)
        {
            if (string.IsNullOrEmpty(user.CellPhone))
                return ("", "Celular não cadastrado", false);

            if ((await _userRepository.CellphoneUsed(user.CellPhone)))
                return ("", "Celular ja esta em uso", false);

            var accessCodeObject = await _userAccessCodeConfirmationRepository.GetAccessCode(user.Id);

            if (accessCodeObject != null && (DateTime.Now - accessCodeObject.CreatedAt).Minutes < 5)
                return ("", "Você já solicitou o envio do token para cadastrar sua nova senha. Aguarde o recebimento ou tente novamente dentro de alguns minutos.", false);

            if (accessCodeObject != null)
                _userAccessCodeConfirmationRepository.Delete(accessCodeObject);

            var newAccessCodeObject = new UserAccessCodeConfirmation
            {
                Code = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 6).Select(s => s[new Random().Next(s.Length)]).ToArray()),
                CreatedAt = DateTime.Now,
                User = await _userRepository.GetById(user.Id)

            };

            _userAccessCodeConfirmationRepository.Save(newAccessCodeObject);

            await _unitOfWork.CommitAsync();


            _SMSService.SendAccessConfirmation(user.CellPhone, newAccessCodeObject.Code);

            return (newAccessCodeObject.Code, "Código gerado com sucesso!", true);
        }

        public async Task<string> GetShopAuthenticate(int userId,int network)
        {

            var usuario = (await _userRepository.CustomFind(x => x.Id == userId, x => x.Office)).FirstOrDefault();

            if (usuario == null)
                throw new Exception("Usuario não encontrado");

            var url = await _eaiSingleSignOnService.Authenticate(usuario);

            return url;
        }

        public async Task<string> GetTrainingAuthenticate(int userId, int network, string ip)
        {
            var usuario = (await _userRepository.CustomFind(x => x.Id == userId, x => x.Office)).FirstOrDefault();

            if (usuario == null)
                throw new Exception("Usuario não encontrado");

            var url = String.Empty;

            var username = "Fullbar";
            var password = "fullbar#@!";

            var token = GenerateTokenTrainingAcademy(username, password);

            if (token != null && !String.IsNullOrEmpty(token.access_token))
            {
                var response = AuthenticateTrainingAcademy(token.access_token, usuario, ip);

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var responseAutentica = JsonConvert.DeserializeObject<ResponseAutentica>(response.Content);

                    if (responseAutentica.Sucesso && responseAutentica.Data != null && responseAutentica.Data.idUsuario > 0)
                    {

                        url = RetornaUrlTrainingAcademy(responseAutentica.Data.idUsuario.ToString());
                    }
                    else
                    {
                        var responseCadastro = RegisterTrainingACademy(token.access_token, usuario, ip);

                        if (responseCadastro != null && response.StatusCode == HttpStatusCode.OK)
                        {
                            var retornoAutentica = AuthenticateTrainingAcademy(token.access_token, usuario, ip);


                            if (retornoAutentica != null && retornoAutentica.StatusCode == HttpStatusCode.OK)
                            {
                                var autentica = JsonConvert.DeserializeObject<ResponseAutentica>(retornoAutentica.Content);

                                if (autentica.Sucesso && autentica.Data != null && autentica.Data.idUsuario > 0)
                                {
                                    url = RetornaUrlTrainingAcademy(autentica.Data.idUsuario.ToString());
                                }
                                else
                                {
                                    url = String.Empty;
                                }
                            }
                        }
                        else
                        {
                            url = String.Empty;
                        }
                    }
                }
            }

            return url;
        }

        public TokenViewModel GenerateTokenTrainingAcademy(string UserName, string Password)
        {


            var token = string.Empty;
            var UrlServico = "https://whpservicemaistop.academiadovarejowhirlpool.com.br/";
            var client = new RestClient(UrlServico);

            var request = new RestRequest("/token", Method.POST);

            request.AddParameter("grant_type", "password");
            request.AddParameter("username", UserName);
            request.AddParameter("password", Password);

            var response = client.Execute<TokenViewModel>(request);

            if (!response.IsSuccessful)
                _logger.Fatal("Não foi possivel conectar na plataforma de treinamento, entre em contato com o administrador!");

            var Token = JsonConvert.DeserializeObject<TokenViewModel>(response.Content);

            var tokenViewModel = new TokenViewModel()
            {
                access_token = Token.access_token
                ,
                expires_in = Token.expires_in
                ,
                token_type = Token.token_type

            };

            return tokenViewModel;
        }

        public IRestResponse<ResponseAutentica> AuthenticateTrainingAcademy(string access_token, User user, string ip)
        {

            var usuario = user;
            var url = String.Format("{0}{1}", "https://whpservicemaistop.academiadovarejowhirlpool.com.br/api/", "autentica");
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            var jsonDadosAcesso = JsonConvert.SerializeObject(new AutenticaRequest { Login = usuario.Cpf, Ip = ip });
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", jsonDadosAcesso, ParameterType.RequestBody);
            request.AddHeader("Authorization", String.Format("bearer {0}", access_token));

            IRestResponse<ResponseAutentica> response = client.Execute<ResponseAutentica>(request);

            if (!response.IsSuccessful)
                _logger.Fatal("Não foi possivel conectar na plataforma de treinamento, entre em contato com o administrador!");

            return response;
        }

        public IRestResponse<ResponseCadastro> RegisterTrainingACademy(string access_token, User user, string ip)
        {
            var usuario = user;

            var url = String.Format("{0}{1}", "https://whpservicemaistop.academiadovarejowhirlpool.com.br/api/", "Cadastrar");
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            var jsonDadosAcesso = JsonConvert.SerializeObject(new UsuarioCadastroRequest
            {
                Login = usuario.Cpf.Replace("-", "").Replace(".", "")
                ,
                Ip = ip
                ,
                Email = usuario.Email
                ,
                Nome = usuario.Name,
                IdPerfil = usuario.Office.Id
            });
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", jsonDadosAcesso, ParameterType.RequestBody);
            request.AddHeader("Authorization", String.Format("bearer {0}", access_token));

            IRestResponse<ResponseCadastro> response = client.Execute<ResponseCadastro>(request);

            return response;
        }

        public string RetornaUrlTrainingAcademy(string idUsuario)
        {

            var sessaoEncrypt = Crypto.Encrypt(idUsuario, Crypto.Key256, 256);

            var urlSite = String.Format("{0}{1}", "https://academiadovarejowhirlpool.com.br/external/Login?SessaoId=", Uri.EscapeDataString(sessaoEncrypt));

            return urlSite;
        }
        

        public async Task DoSaveUserAccessLog(string cpf, string description, string ip, string device)
        {

            var user = (await _userRepository.CustomFind(x => x.Cpf.Equals(cpf))).FirstOrDefault();

            if (user != null)
            {
                _userAccessLogRepository.Save(new UserAccessLog
                {
                    CreatedAt = DateTime.Now,
                    Description = description,
                    Device = device,
                    Ip = ip,
                    User = user
                });

                await _unitOfWork.CommitAsync();
            }

        }
    }
}
