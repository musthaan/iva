//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IVA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Spec_items
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long SpecID { get; set; }
        public string Remarks { get; set; }
    
        public virtual tbl_Specs tbl_Specs { get; set; }
    }
}
