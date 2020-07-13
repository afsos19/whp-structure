using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Data.Repositories
{
    public class RightAnswerRepository : Repository<RightAnswer>, IRightAnswerRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public RightAnswerRepository(IUnitOfWork unitOfWork,WhpDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool sucesso, string mensagem)> SaveAnswer(List<RightAnswer> rightAnswer)
        {
            try
            {
                foreach(var i in rightAnswer)
                {
                    i.CreatedAt = DateTime.Now;
                     Save(i);
                    await _unitOfWork.CommitAsync();

                }
                return (true, "Cadastrado com sucesso");
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }

        }

      
    }
}
