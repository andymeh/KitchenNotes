﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniversalAppService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KitchNotesDatabaseEntities : DbContext
    {
        public KitchNotesDatabaseEntities()
            : base("name=KitchNotesDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Hub> Hubs { get; set; }
        public virtual DbSet<HubEvent> HubEvents { get; set; }
        public virtual DbSet<Notes> Notes1 { get; set; }
        public virtual DbSet<Tasks> Tasks1 { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserHub> UserHubs { get; set; }
    }
}