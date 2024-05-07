using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;

namespace WebApplication1.Models.FormsData
{
    public class OilFiltersFormData
    {
        public string? Name { get; set; }
        public string? APIClass { get; set; }
        public string[]? Producer { get; set; }
        public string[]? SAEViscosity { get; set; }
        public int[]? Company { get; set; }
        public int VolumeFrom { get; set; }
        public int VolumeTo{ get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo{ get; set; }
        protected object? TrySetValue(string key, IFormCollection Form)
        {
            if (Form.ContainsKey(key))
                return Form[key];
            else
                return null;
        }
        public bool OilSuitsFilter(MotorOilMerch merch)
        {
            bool IsSuitable = true;
            MotorOil oil = merch.MotorOil;
            if(Name!=null)
                IsSuitable = IsSuitable && oil.Name.StartsWith(Name);
            if(APIClass!= null)
                IsSuitable = IsSuitable && oil.APIQualityClass.Name==APIClass;
            if (Producer!=null)
                IsSuitable = IsSuitable && Producer.Contains(oil.Producer);
            if (SAEViscosity != null)
                IsSuitable = IsSuitable && SAEViscosity.Contains(oil.SAEViscosity.ToString());
            if (Company != null)
                IsSuitable = IsSuitable && Company.Contains(merch.Store.Company.id);
            if(!(VolumeFrom==0 && VolumeTo == 0))
            {
                IsSuitable = IsSuitable && oil.Volume>=VolumeFrom && oil.Volume <= VolumeTo;
            }
            if (!(PriceFrom == 0 && PriceTo == 0))
            {
                IsSuitable = IsSuitable && merch.Price >= PriceFrom && merch.Price <= PriceTo;
            }
            return IsSuitable;
        }
        /*
        public override string ToString()
        {
            string s="Name: " +Name;
            s += "\nAPIClass: " + APIClass;
            foreach(string p in Producer)
            {
                s += "\nProducer: " + p;
            }
            foreach (int v in SAEViscosity)
            {
                s += "\nViscosity: " + v;
            }
            foreach(int c in Company)
            {
                s += "\nCompany: " + c;
            }
            s += "VolumeFrom: " + VolumeFrom;
            s += "VolumeTo: " + VolumeTo;
            s += "PriceFrom: " + PriceFrom;
            s += "PriceTo: " + PriceTo;
            return s;
        }
        */
    }
}
