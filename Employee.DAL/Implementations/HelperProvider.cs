using Castle.Core.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using AWE.Employee.DAL.Abstractions;

namespace AWE.Employee.DAL.Implementations
{
    public class HelperProvider : IHelperProvider
    {
        private readonly IConfiguration _Config;
     
        public HelperProvider(IConfiguration Config)
        {
           
            _Config = Config;
        }

        public string FormatAmount(string strAmount)
        {
            string str = String.Empty;
            try
            {
                if (strAmount.Trim().Length > 0)
                {
                    if (strAmount.ToUpper().EndsWith("DOLLARS"))
                    {
                        //return FormatAmountCurrency(strAmount.Replace("Dollars", string.Empty));
                    }
                    else
                    {
                        strAmount = strAmount.Replace(".00", string.Empty);
                        return strAmount;
                    }
                }
                if (strAmount.Trim() == "0.00")
                {
                    strAmount = "";
                }
                return strAmount;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

     
    }
}