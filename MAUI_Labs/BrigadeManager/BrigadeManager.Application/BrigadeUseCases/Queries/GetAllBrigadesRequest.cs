using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.BrigadeUseCases.Queries
{
    public sealed record GetAllBrigadesRequest() : IRequest<IEnumerable<Brigade>>;

    internal class GetAllBrigadesRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllBrigadesRequest, IEnumerable<Brigade>>
    {
        public async Task<IEnumerable<Brigade>> Handle(
            GetAllBrigadesRequest request,
            CancellationToken cancellationToken)
        {
            return await unitOfWork.BrigadeRepository.ListAllAsync(cancellationToken);
        }
    }
}
