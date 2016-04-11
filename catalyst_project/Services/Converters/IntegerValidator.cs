using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace catalyst_project.Services.Converters
{
    class IntegerValidator : ValidationRule
    {

        public override ValidationResult Validate
         (object value, System.Globalization.CultureInfo cultureInfo)
        {
            double result=9 ;
            if (value != null && !Double.TryParse(value.ToString(), out result))
                return new ValidationResult(false, "Only number is accepted!");
            else
            return ValidationResult.ValidResult;
        }
    }
}
