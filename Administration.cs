//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OptagricDataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Administration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Administration()
        {
            this.Panel_CRUD_Process_Logs = new HashSet<Panel_CRUD_Process_Logs>();
        }
    
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Authority { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Panel_CRUD_Process_Logs> Panel_CRUD_Process_Logs { get; set; }
    }
}