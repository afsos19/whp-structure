using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLog;
using Whp.MaisTop.Api.Extensions;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.ViewModel;

namespace Whp.MaisTop.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "VENDEDOR,GERENTE,GERENTE REGIONAL,GESTOR DA INFORMACAO,PREMIO IDEAL")]
    public class QuizController : MainController
    {

        private readonly IQuizService _quizService;
        private readonly ILogger _logger;

        public QuizController(IConfiguration configuration, IQuizService quizService, ILogger logger) : base(configuration)
        {
            _quizService = quizService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SaveQuiz([FromBody] AnswerUserQuizVM answerUserQuizVM)
        {
            try
            {
                var result = await _quizService.SaveQuiz(answerUserQuizVM.AnswerUserQuizDto, answerUserQuizVM.RightAnswers, UserId);

                if (result)
                {
                    _logger.Info($"Usuario com id {UserId} respondeu o questionario com sucesso!");
                    return Ok("Questionario respondido com sucesso!");
                }
                else
                {
                    _logger.Warn($"Usuario com id {UserId} - não conseguiu responder o questionario, ocorreu um erro inesperado!");
                    return BadRequest("Ocorreu um erro ao tentar responder o questionario!");
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif

                return BadRequest($"Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentQuiz()
        {
            try
            {
                string auth = Request.Headers["Authorization"];
                var token = auth.Split(' ')[1];
                var jwtD = GetJWTData(token);
                var result = await _quizService.GetCurrentQuiz(jwtD.id, jwtD.network, jwtD.office, jwtD.shop);

                if (result.Question.Any())
                    return Ok(new { result.Question, result.Answer });
                else
                    return NotFound("Nenhum questionario ativo no momento");

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif

                return BadRequest($"Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActivatedQuiz()
        {
            try
            {
                var result = await _quizService.GetActivatedQuiz();

                if (result.Question.Any())
                    return Ok(new { result.Question, result.Answer });
                else
                    return NotFound("Nenhum questionario ativo no momento");

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif

                return BadRequest($"Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}