//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_page
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ask()
        {
            this.Answers = new HashSet<Answer>();
            this.QuestionTypeAsks = new HashSet<QuestionTypeAsk>();
        }
    
        public int Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public string picturePath { get; set; }
        public Nullable<int> questiontypeAskId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionTypeAsk> QuestionTypeAsks { get; set; }
    }
}
