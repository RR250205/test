using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace STOMS.Common
{
   partial class Constant
    {
        public static ToutDate ConvertToUSDateTime<ToutDate,TinDate>(TinDate date)
        {
            try
            {               
                 System.Globalization.CultureInfo cinfo = new CultureInfo("en-US");
                 return (ToutDate)Convert.ChangeType(Convert.ToDateTime(date, cinfo), typeof(ToutDate));
                //return (ToutDate)Convert.ChangeType(Convert.ToDateTime(date, cinfo), typeof(ToutDate))
            }
            catch (Exception formatException)
            {
                return (ToutDate)Convert.ChangeType(date, typeof(string));
            }
            
        }
    }
}
