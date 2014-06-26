using Grit.CQRS.Demo.Model.Investments.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Investments.CommandHandlers
{
    public class InvestCommandHandler : ICommandHandler<InvestCommand>
    {
        static InvestCommandHandler()
        {
            AutoMapper.Mapper.CreateMap<InvestCommand, Investment>();
        }
        private IInvestmentWriteRepository _repository;
        public InvestCommandHandler(IInvestmentWriteRepository repository)
        {
            _repository = repository;
        }

        public void Execute(InvestCommand command)
        {
            _repository.Add(AutoMapper.Mapper.Map<Investment>(command));
        }
    }
}
