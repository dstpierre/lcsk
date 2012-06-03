/*
 * THIS WAS TAKEN FROM Scott Hanselman's blog post
 * http://www.hanselman.com/blog/TheWeeklySourceCode37GeolocationGeotargetingReverseIPAddressLookupInASPNETMVCMadeEasy.aspx
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace LCSK
{
    public class LocationInfo
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
    }

    public class GeoLocationService
    {
        public static LocationInfo Get(string ipaddress)
        {
            LocationInfo v = new LocationInfo();

            if (ipaddress != "127.0.0.1")
                v = GeoLocationService.GetLocationInfo(ipaddress);
            else //debug locally
                v = new LocationInfo()
                {
                    Name = "Sugar Grove, IL",
                    CountryCode = "US",
                    CountryName = "UNITED STATES",
                    Latitude = 41.7696F,
                    Longitude = -88.4588F
                };
            return v;
        }

        private static Dictionary<string, LocationInfo> cachedIps = new Dictionary<string, LocationInfo>();

        private static LocationInfo GetLocationInfo(string ipParam)
        {
            LocationInfo result = null;
            IPAddress i = System.Net.IPAddress.Parse(ipParam);
            string ip = i.ToString();
            if (!cachedIps.ContainsKey(ip))
            {
                string r;
                using (var w = new WebClient())
                {
                    r = w.DownloadString(String.Format("http://api.hostip.info/?ip={0}&position=true", ip));
                }

                var xmlResponse = XDocument.Parse(r);
                var gml = (XNamespace)"http://www.opengis.net/gml";
                var ns = (XNamespace)"http://www.hostip.info/api";

                try
                {
                    result = (from x in xmlResponse.Descendants(ns + "Hostip")
                              select new LocationInfo
                              {
                                  CountryCode = x.Element(ns + "countryAbbrev").Value,
                                  CountryName = x.Element(ns + "countryName").Value,
                                  Latitude = float.Parse(x.Descendants(gml + "coordinates").Single().Value.Split(',')[0]),
                                  Longitude = float.Parse(x.Descendants(gml + "coordinates").Single().Value.Split(',')[1]),
                                  Name = x.Element(gml + "name").Value
                              }).SingleOrDefault();
                }
                catch (NullReferenceException)
                {
                    //Looks like we didn't get what we expected.
                }
                if (result != null)
                {
                    cachedIps.Add(ip, result);
                }
            }
            else
            {
                result = cachedIps[ip];
            }
            return result;
        }
    }
}
