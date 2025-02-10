using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Model;

namespace Lab1.Services
{
    public interface IRateService
    {
        Task<IEnumerable<Rate>> GetRatesAsync(DateTime date);
    }
}
