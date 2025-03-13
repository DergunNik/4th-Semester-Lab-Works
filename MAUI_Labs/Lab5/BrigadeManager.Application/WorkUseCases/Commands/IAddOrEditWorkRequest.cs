using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.WorkUseCases.Commands
{
    public interface IAddOrEditWorkRequest : IRequest
    {
        Work Work { get; set; }
    }
}
