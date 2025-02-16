using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.WorkUseCases.Queries
{
    public sealed record GetWorkByIdRequest(int Id) : IRequest<Work?>;

    internal class GetWorkByIdRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetWorkByIdRequest, Work?>
    {
        public async Task<Work?> Handle(GetWorkByIdRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.WorkRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
