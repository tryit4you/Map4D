using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map4D.ViewModels
{
    public class DuLieuDoiTuongViewModel
    {
        public string type { get; set; }
        public properties properties { get; set; }
        public geometry geometry { get; set; }
        public buildings buildings { get; set; }
    }
    public class geometry
    {
        public string type { get; set; }
        public List<coordinates> coordinates { get; set; }
    }
    public class coordinates {
        public string lng { get; set; }
        public string lat { get; set; }
    }
    public class properties
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("stroke")]
        public string stroke { get; set; }
        [JsonProperty("stroke-opacity")]
        public string strokeopacity { get; set; }
        [JsonProperty("fill-opacity")]
        public string fillopacity { get; set; }
        [JsonProperty("stroke-width")]
        public string strokewidth { get; set; }
        public string fill { get; set; }
        public string templatecategory { get; set; }
    }
    public class buildings
    {
        public string id { get; set; }
        public string name { get; set; }
        public string prototypeId { get; set; }
        public string scale { get; set; }
        public string scaleY { get; set; }
        public string elevation { get; set; }
        public string rotation { get; set; }
        public string minZoom { get; set; }
        public string maxZoom { get; set; }
      
    }
}