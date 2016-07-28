using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailForm.Models
{
    /// <summary>
    /// InfoStorage class based on which Entity maps its objects
    /// </summary>
    public class InfoStorage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be empty"),
        StringLength(100, MinimumLength = 2, ErrorMessage = "Length error"),
        RegularExpression(@"^[a-zA-Z]+[a-zA-Z\s-]*[a-zA-Z]+$", ErrorMessage = "Name contains invalid characters")]
        public string Name { get; set; }
        [TermsAccept(ErrorMessage = "Accept terms and conditions")]
        public bool Terms { get; set; }
        [NotMapped, Required(ErrorMessage = "Select a value")]
        public Int16[] secVals { get; set; }
        public string dbSecVals { get; set; }
        //https://en.wikipedia.org/wiki/Email_address more or less fits the standard
        [Required(ErrorMessage = "Email cannot be empty"),
        StringLength(100, MinimumLength = 5, ErrorMessage = "Length error"),
        RegularExpression(@"^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)$", ErrorMessage = "Email contains invalid characters"),
        EmailValid(ErrorMessage = "Email taken"),
        Remote("MailInvalid", "Home", HttpMethod = "POST", ErrorMessage = "Email address already registered.")] //using remote instead of EmailValid on client side
        public string Email { get; set; }
    }
}