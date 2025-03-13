using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.BrigadeUseCases.Commands
{
    public interface IAddOrEditBrigadeRequest : IRequest
    {
        Brigade Brigade { get; set; }
    }
}
