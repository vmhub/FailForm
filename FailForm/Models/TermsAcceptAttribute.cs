using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailForm.Models
{    /// <summary>
    /// Terms and condition custom validator
    /// </summary>
    public class TermsAcceptAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "termsaccept"
            };
        }
    }
}