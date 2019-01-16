using System;

namespace Map4D.Models
{
    public class CountriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public string Type { get; set; }
        public int ParentId { get; set; }
        public int ModuleId { get; set; }
        public int IsVisible { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedOnDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public bool IsState { get; set; }
        public string Code { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string NameKhongDau { get; set; }
    }
}