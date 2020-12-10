using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Daniel_Kasprów_lista6
{
    public class CharacterLengthRule : ValidationRule
    {
        public int CharacterLength { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            try
            {
                long charint = Convert.ToInt64(charString);
                if (charString.Length != CharacterLength)
                    return new ValidationResult(false, $"Required length {CharacterLength}");

                return new ValidationResult(true, null);
            }
            catch
            {
                if (charString.Length != 0)
                {
                    if (charString.Length != CharacterLength)
                        return new ValidationResult(false, $"Required length {CharacterLength} and only numbers");
                    else
                        return new ValidationResult(false, $"Letters not allowed");
                }
                return new ValidationResult(true, null);
            }
        }
    }
}
