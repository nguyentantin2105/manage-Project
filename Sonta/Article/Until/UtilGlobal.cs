using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Article.Until
{
    public class UtilGlobal
    {
        public static readonly string CMS_URL = ConfigurationManager.AppSettings["CMS_URL"];

        //FileController fileController;
        public static readonly List<ValueModel> MaLoaiHoSo_SONHA = new List<ValueModel> {
            new ValueModel{Value="CAPDOI", Text="Cấp đổi"},
            new ValueModel{Value="TAOMOI", Text="Cấp mới"},
            //new ValueModel{Value="album", Text="Album"},          
        };

        public static T ConvertToObject<T>(string value)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(value);
                if (obj == null)
                {
                    return Activator.CreateInstance<T>();
                }
                return obj;
            }
            catch (Exception)
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
    public class ValueModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

}