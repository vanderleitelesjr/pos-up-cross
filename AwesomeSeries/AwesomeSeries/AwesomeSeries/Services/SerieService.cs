using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AwesomeSeries.Models;

namespace AwesomeSeries.Services
{
    public class SerieService : ISerieService
    {
        public Task<IEnumerable<SerieResponse>> GetSeriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
