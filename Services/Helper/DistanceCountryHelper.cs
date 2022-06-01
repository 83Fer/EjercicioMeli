using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class DistanceCountryHelper
    {

        public static async Task<string> CalculateEstimatedDistance(IPInformation ipInfo)
        {
            return $"{GetDistance(ipInfo.Latitude, ipInfo.Longitude)} Km";
        }

        public static async Task<string> CalculateFarDistance(Dictionary<string, IPInformation> dic)
        {
            var maxValue = dic.Aggregate((l, r) => GetDistance(l.Value.Latitude, l.Value.Longitude) > GetDistance(r.Value.Latitude, r.Value.Longitude) ? l : r).Value;

            return $"Distancia mayor calculada: Pais : {maxValue.CountryName} ; Distancia: {GetDistance(maxValue.Latitude, maxValue.Longitude)} Km ";
        }

        public static async Task<string> CalculateCloseDistance(Dictionary<string, IPInformation> dic)
        {
            var minValue = dic.Aggregate((l, r) => GetDistance(l.Value.Latitude, l.Value.Longitude) < GetDistance(r.Value.Latitude, r.Value.Longitude) ? l : r).Value;

            return $"Distancia menor calculada: Pais : {minValue.CountryName} ; Distancia: {GetDistance(minValue.Latitude, minValue.Longitude)} Km ";
        }

        public static async Task<string> CalculateAverageDistance(Dictionary<string, IPInformation> dic)
        {
            var sumValues = dic.Sum(x => GetDistance(x.Value.Longitude, x.Value.Latitude) * x.Value.Invocation);
            var sumInvocation = dic.Sum(x => x.Value.Invocation);

            return $"Distancia promedio calculada: {Math.Round(sumValues / sumInvocation, 0)} Km";
        }

        private static double GetDistance(double latitude2, double longitude2)
        {
            const double latitudeBsAs = -34.611778259277344;
            const double longitudeBsAs = -58.41730880737305;
            const double EarthRadius = 6371;

            double distance = 0;
            double Lat = (latitude2 - latitudeBsAs) * (Math.PI / 180);
            double Lon = (longitude2 - longitudeBsAs) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(latitudeBsAs * (Math.PI / 180))
                * Math.Cos(latitude2 * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;
            return Math.Round(distance, 0);
        }
    }
}
