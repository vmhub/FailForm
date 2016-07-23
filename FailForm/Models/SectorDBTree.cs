using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailForm.Models
{    /// <summary>
    /// Linked list-like Sector class (parent-child relations)
    /// </summary>
    public class SectorDBTree<Sector>
    {
            public Sector item { get; set; }
            public IEnumerable<SectorDBTree<Sector>> childz { get; set; }
    }
}