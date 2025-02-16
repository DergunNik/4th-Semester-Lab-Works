using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.BrigadeUseCases.Commands
{
    public sealed class AddBrigadeRequest() : IAddOrEditBrigadeRequest
    {
        public Brigade Brigade { get; set; }
    }

    internal class AddBrigadeRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddBrigadeRequest>
    {
        public async Task Handle(AddBrigadeRequest request, CancellationToken cancellationToken)
        {
            await unitOfWork.BrigadeRepository.AddAsync(request.Brigade, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }

}
