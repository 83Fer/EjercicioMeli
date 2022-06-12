using Models;
using Newtonsoft.Json;
using Services.Helper;
using System.Text.Json;
using System.Web;
using StackExchange.Redis;
using System.Net;

namespace Services
{
    public class InformacionIPService : IInformacionIPService
    {
        private readonly IGeolocalizacionService _geolocalizacionService;
        private readonly IInfoMonedasService _infoMonedasService;
        private readonly ICacheService _cacheService;

        public InformacionIPService(IGeolocalizacionService geolocalizacionService, IInfoMonedasService infoMonedasService, ICacheService cacheService)
        {
            _geolocalizacionService = geolocalizacionService;
            _infoMonedasService = infoMonedasService;
            _cacheService = cacheService;
        }

        public async Task<IPInformation> Get(string ip)
        {
            IPInformation ipInfo = null;

            if(!ValidIP(ip))
                throw new Exception("La IP es invalida");

            var value = await _cacheService.GetCacheValueAsync(ip);
            if (value == null)
            {
                var geo = await _geolocalizacionService.Get(ip);

                if(geo == null || geo.CountryName == null) 
                    throw new Exception("No hay información de la ip");
                
                ipInfo = await InstanceIPInformationFromService(geo);
            }
            else
            {
                ipInfo = JsonConvert.DeserializeObject<IPInformation>(value);
                
                await InstanceIPInformationFromCache(ipInfo, ip);
            }

            if(ipInfo != null)
            {
                var dic = await _cacheService.GetAllValuesCacheAsync();
                ipInfo.FarDistance = await DistanceCountryHelper.CalculateFarDistance(dic);
                ipInfo.CloseDistance = await DistanceCountryHelper.CalculateCloseDistance(dic);
                ipInfo.AverageDistance = await DistanceCountryHelper.CalculateAverageDistance(dic);
            }
            
            return ipInfo;
        }

        private bool ValidIP(string ip)
        {
            IPAddress ipAux = new IPAddress(new byte[] { 0, 0, 0, 0 });
            return IPAddress.TryParse(ip, out ipAux);
        }

        private async Task<IPInformation> InstanceIPInformationFromService(Geolocalizacion geolocalizacion)
        {
            var ipInfo = new IPInformation();

            ipInfo.CountryName = geolocalizacion.CountryName;
            ipInfo.CountryCode = geolocalizacion.CountryCode;
            ipInfo.CurrentTimeLocal = DateTime.Now;
            ipInfo.Latitude = geolocalizacion.Latitude;
            ipInfo.Longitude = geolocalizacion.Longitude;
            
            if(geolocalizacion.TimeZone != null)
            {
                ipInfo.CurrentTime = geolocalizacion.TimeZone.CurrentTime;
                ipInfo.CurrentTimeFormatted = geolocalizacion.TimeZone.CurrentTime.ToLongDateString();
            }
            
            ipInfo.Invocation = 1;
            ipInfo.Lenguages = new List<Lenguages>();
            geolocalizacion.Location.Lenguages.ForEach(x => ipInfo.Lenguages.Add(x));

            if (geolocalizacion.Currency != null)
            {
                ipInfo.NameCurrency = geolocalizacion.Currency.Name;
                ipInfo.SymbolCurrency = geolocalizacion.Currency.Symbol;

                var infoMoneda = await _infoMonedasService.Get(geolocalizacion.Currency.Symbol);
                ipInfo.DollarQuote = $"1 USD = {infoMoneda.Rates.FirstOrDefault().Value} {infoMoneda.Rates.FirstOrDefault().Key}";
            }
            ipInfo.EstimatedDistance = await DistanceCountryHelper.CalculateEstimatedDistance(ipInfo);
            
            await _cacheService.SetCacheValueAsync(geolocalizacion.IP, JsonConvert.SerializeObject(ipInfo));
            
            return ipInfo;
        }

        private async Task InstanceIPInformationFromCache(IPInformation ipInfo, string ip)
        {
            ipInfo.Invocation++;
            
            if (ipInfo.CurrentTime != DateTime.MinValue)
                ipInfo.CurrentTimeFormatted = DateHelper.CalculateFormattedDate(ipInfo.CurrentTimeLocal, ipInfo.CurrentTime);
            
            if(ipInfo.SymbolCurrency != null)
            {
                var infoMoneda = await _infoMonedasService.Get(ipInfo.SymbolCurrency);
                ipInfo.DollarQuote = $"1 USD = {infoMoneda.Rates.FirstOrDefault().Value} {infoMoneda.Rates.FirstOrDefault().Key}";
            }

            await _cacheService.SetCacheValueAsync(ip, JsonConvert.SerializeObject(ipInfo));
        }
    }
}