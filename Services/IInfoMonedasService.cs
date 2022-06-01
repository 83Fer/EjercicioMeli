using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IInfoMonedasService
    {
        Task<InfoMonedas> Get(string symbol);
    }
}
