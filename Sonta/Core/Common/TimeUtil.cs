using System;

namespace Core.Common
{
    public class TimeUtil
    {
        /// <summary>
        /// Convert string format dd/MM/yyyy to DateTime
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime? ConvertDate(string datetime)
        {
            try
            {
                DateTime dtime = DateTime.ParseExact(datetime, "dd/MM/yyyy", null);
                if (dtime.Year < 1970)
                {
                    return null;
                }
                return dtime;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
