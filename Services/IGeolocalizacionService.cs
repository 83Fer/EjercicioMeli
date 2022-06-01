using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IGeolocalizacionService
    {
        Task<Geolocalizacion> Get(string ip);
    }
}
