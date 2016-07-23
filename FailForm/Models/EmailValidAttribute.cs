using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailForm.Models
{
    public class EmailValidAttribute : ValidationAttribute, IClientValidatable
    {   /// <summary>
        /// Server side email validation (not working as expected on client side)
        /// </summary>
        public override bool IsValid(object value)
        {
            InfoStorage back;
            using (MyContext cont = new MyContext())
            {
                back = cont.infoStore.Where(x => x.Email == (string)value).FirstOrDefault();
            }
            return back == null ? true : false;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "emailvalid"
            };
        }
    }
}