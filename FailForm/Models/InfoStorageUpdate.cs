using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FailForm.Models
{   /// <summary>
    /// Modified class without email validation to pass into edit view
    /// </summary>    
    public class InfoStorageUpdate
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be empty"),
        StringLength(100, MinimumLength = 2, ErrorMessage = "Length error"),
        RegularExpression(@"^[a-zA-Z]+[a-zA-Z\s-]*[a-zA-Z]+$", ErrorMessage = "Name contains invalid characters")]
        public virtual string Name { get; set; }
        [TermsAccept(ErrorMessage = "Accept terms and conditions")]
        public virtual bool Terms { get; set; }
        [Required(ErrorMessage = "Select a value")]
        public virtual Int16[] secVals { get; set; }
        public virtual string dbSecVals { get; set; }
        public virtual string Email { get; set; }
    }
}