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
    
    public partial class tbl_Product_Specs
    {
        public long ID { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> SpecID { get; set; }
        public string Value { get; set; }
    
        public virtual tbl_Products tbl_Products { get; set; }
        public virtual tbl_Specs tbl_Specs { get; set; }
    }
}
