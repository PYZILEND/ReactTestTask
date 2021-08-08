using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReactTestTask.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LaterThanAttribute:ValidationAttribute
    {
        string OtherDateFieldName { get; set; }

        public LaterThanAttribute(string otherDateFieldName)
        {
            OtherDateFieldName = otherDateFieldName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            DateTime laterDate = (DateTime)value;
            DateTime earlyerDate = (DateTime)context.ObjectType.GetProperty(OtherDateFieldName).GetValue(context.ObjectInstance, null);

            if (laterDate > earlyerDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not later");
            }
        }
    }
}
