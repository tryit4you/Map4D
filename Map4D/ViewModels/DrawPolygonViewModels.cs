using Map4D.Models;
using System.Collections.Generic;

namespace Map4D.ViewModels
{
    public class DrawPolygonViewModels
    {
        public string CurrentCityId { get; set; }
        public string CurrentDistrictId { get; set; }
        public IEnumerable<CountriesViewModel> Cities { get; set; }
        public IEnumerable<CountriesViewModel> Districts { get; set; }
        public IEnumerable<CountriesViewModel> Wards { get; set; }
    }
}