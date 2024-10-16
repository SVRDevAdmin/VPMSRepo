using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models.CustomValidation
{
    public class NotEqualAttribute : ValidationAttribute
    {
        private string OtherProperty { get; set; }

        public NotEqualAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get other property value
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            // verify values
            if (value != null && value.ToString().Equals(otherValue.ToString()))
                //return new ValidationResult(string.Format("{0} should not be equal to {1}.", validationContext.DisplayName, OtherProperty));
                return new ValidationResult("New password should be the same as old password.");
            else
                return ValidationResult.Success;
        }
    }
}
