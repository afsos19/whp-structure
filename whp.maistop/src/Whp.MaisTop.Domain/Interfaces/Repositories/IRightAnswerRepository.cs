 using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IRightAnswerRepository
    {
        Task<(bool sucesso, string mensagem)> SaveAnswer(List<RightAnswer> rightAnswer);
    }
}
