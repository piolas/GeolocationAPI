using System;
using System.Collections.Generic;

namespace Geolocation.Domain.Domain
{
    public class RootObject
    {
        public Guid Id { get; set; }
        public string URLValue { get; set; }
        public string ip { get; set; }
        public string type { get; set; }
        public string continent_code { get; set; }
        public string continent_name { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Location location { get; set; }
        public TimeZone time_zone { get; set; }
        public Currency currency { get; set; }
        public Connection connection { get; set; }
    }

    public class Language
    {
        public Guid Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string native { get; set; }
    }

    public class Location
    {
        public Guid Id { get; set; }
        public int geoname_id { get; set; }
        public string capital { get; set; }
        public List<Language> languages { get; set; }
        public string country_flag { get; set; }
        public string country_flag_emoji { get; set; }
        public string country_flag_emoji_unicode { get; set; }
        public string calling_code { get; set; }
        public bool is_eu { get; set; }
    }

    public class TimeZone
    {
        public string id { get; set; }
        public DateTime current_time { get; set; }
        public int gmt_offset { get; set; }
        public string code { get; set; }
        public bool is_daylight_saving { get; set; }
    }

    public class Currency
    {
        public Guid Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string plural { get; set; }
        public string symbol { get; set; }
        public string symbol_native { get; set; }
    }

    public class Connection
    {
        public Guid Id { get; set; }
        public int asn { get; set; }
        public string isp { get; set; }
    }
}
