using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FailForm.Models
{   /// <summary>
    ///Sector class based on which Entity maps its objects
    /// </summary>
    public class Sector
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; }
        [Required]
        public byte parentId { get; set; }
        [Required]
        public Int16 Value { get; set; }
        [NotMapped]
        public string htmlName { get; set; }
    }
}