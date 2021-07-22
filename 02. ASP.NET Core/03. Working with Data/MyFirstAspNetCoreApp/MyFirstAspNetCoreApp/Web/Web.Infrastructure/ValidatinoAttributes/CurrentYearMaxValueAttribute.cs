using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstAspNetCoreApp.Web.Web.Infrastructure.ValidatinoAttributes
{
    public class CurrentYearMaxValueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            if (value is int intValue)
            // ако не е INT ше даде грешка
            // ако е INT ше запише стойността в intValue
            {
                if (intValue < DateTime.UtcNow.Year)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
