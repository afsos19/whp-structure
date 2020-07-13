using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Business.Services
{
    public class LogsPunctuationService : ILogsPunctuationService
    {
        private ILogsPunctuationRepository _logsPunctuationRepository;

        public LogsPunctuationService(ILogsPunctuationRepository logsPunctuationRepository)
        {
            _logsPunctuationRepository = logsPunctuationRepository;
        }
        public LogsPunctuationDto GetDailyLogsPunctuation()
        {
            var logsReturn = new LogsPunctuationDto();
            try
            {
                DateTime yesterday = DateTime.Now.AddDays(-1);
                DateTime yesterdayInit = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day, 0, 0, 0);
                DateTime yesterdayLimit = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day, 23, 59, 59);

                List<LogsPunctuation> logs = _logsPunctuationRepository.CustomFind(x => x.CRIADO_EM <= yesterdayLimit && x.CRIADO_EM >= yesterdayInit).Result.ToList();

                if (logs.Any())
                {
                    logsReturn.Credits = logs.Where(x => x.TIPO_OPERACAO.ToUpper() == "C").Sum(y => y.PONTUACAO);
                    logsReturn.Debits = logs.Where(x => x.TIPO_OPERACAO.ToUpper() == "D").Sum(y => y.PONTUACAO);
                }
                else
                {
                    logsReturn.Credits = 0;
                    logsReturn.Debits = 0;
                }

                logsReturn.Message = $"Log de pontuação gerada fora da aplicação, no dia {yesterday.ToString("dd/MM/yyyy")}";

                return logsReturn;
            }
            catch (Exception e)
            {
                logsReturn.Message = e.Message;
                logsReturn.Credits = 0;
                logsReturn.Debits = 0;
                return logsReturn;
            }

        }

        public LogsPunctuationDto GetLastHourLogsPunctuation()
        {
            var logsReturn = new LogsPunctuationDto();

            try
            {
                DateTime hourLimit = DateTime.Now;
                DateTime hourInit = DateTime.Now.AddHours(-1);

                List<LogsPunctuation> logs = _logsPunctuationRepository.CustomFind(x => x.CRIADO_EM <= hourLimit && x.CRIADO_EM >= hourInit).Result.ToList();

                if (logs.Any())
                {
                    logsReturn.Credits = logs.Where(x => x.TIPO_OPERACAO.ToUpper() == "C").Sum(y => y.PONTUACAO);
                    logsReturn.Debits = logs.Where(x => x.TIPO_OPERACAO.ToUpper() == "D").Sum(y => y.PONTUACAO);
                }
                else
                {
                    logsReturn.Credits = 0;
                    logsReturn.Debits = 0;
                }

                logsReturn.Message = $"Log de pontuação gerada fora da aplicação, na última hora de {hourInit.ToString("dd/MM/yyyy hh:mm")} às {hourLimit.ToString("dd/MM/yyyy hh:mm")}";

                return logsReturn;
            }
            catch (Exception e)
            {
                logsReturn.Message = e.Message;
                logsReturn.Credits = 0;
                logsReturn.Debits = 0;
                return logsReturn;
            }
            
        }
    }
}
