using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.BrigadeUseCases.Queries
{
    public sealed record GetBrigadeByIdRequest(int Id) : IRequest<Brigade?>;

    internal class GetBrigadeByIdRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBrigadeByIdRequest, Brigade?>
    {
        public async Task<Brigade?> Handle(GetBrigadeByIdRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.BrigadeRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
