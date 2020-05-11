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
    
    public partial class Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Answer()
        {
            this.AnswerReplies = new HashSet<AnswerReply>();
            this.Votes = new HashSet<Vote>();
            this.MinusVotes = new HashSet<MinusVote>();
        }
    
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime createTime { get; set; }
        public string content { get; set; }
        public int askId { get; set; }
    
        public virtual Ask Ask { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerReply> AnswerReplies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vote> Votes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MinusVote> MinusVotes { get; set; }
    }
}
