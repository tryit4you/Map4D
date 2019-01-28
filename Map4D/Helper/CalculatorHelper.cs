using Map4D.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map4D.Helper
{
    public class CalculatorHelper
    {
        public static List<PointViewModel> ConvertObjectDataToListPoint(string ObjectData)
        {
            List<PointViewModel> listPoint = new List<PointViewModel>();
            ObjectData = ObjectData.Substring(ObjectData.IndexOf("[[") + 3, ObjectData.LastIndexOf("]]") - ObjectData.IndexOf("[[") - 4);
            string[] listSplit = ObjectData.Split('[', ']');

            foreach (string split in listSplit)
            {
                if (split.Length > 1)
                {
                    double Lng = double.Parse(split.Split(',')[0]);
                    double Lat = double.Parse(split.Split(',')[1]);
                    listPoint.Add(new PointViewModel() { Lng = Lng, Lat = Lat });
                }
            }

            return listPoint;
        }
    }
}