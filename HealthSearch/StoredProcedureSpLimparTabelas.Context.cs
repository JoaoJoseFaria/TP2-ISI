﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthSearch
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HealthSearchEntitiesSpLimparTabelas : DbContext
    {
        public HealthSearchEntitiesSpLimparTabelas()
            : base("name=HealthSearchEntitiesSpLimparTabelas")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int SpLimparTabelas(string deleteDate)
        {
            var deleteDateParameter = deleteDate != null ?
                new ObjectParameter("deleteDate", deleteDate) :
                new ObjectParameter("deleteDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpLimparTabelas", deleteDateParameter);
        }
    }
}