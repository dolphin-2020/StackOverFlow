﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DolphinQueryEntities : DbContext
    {
        public DolphinQueryEntities()
            : base("name=DolphinQueryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AnswerReply> AnswerReplies { get; set; }
        public virtual DbSet<Ask> Asks { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<QuestionTypeAsk> QuestionTypeAsks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<MITTStudent> MITTStudents { get; set; }
        public virtual DbSet<MinusVote> MinusVotes { get; set; }
    }
}
