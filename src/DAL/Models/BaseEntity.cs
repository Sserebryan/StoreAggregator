using System;

namespace DAL.Models
{
    public abstract class BaseEntity
    {
        public Int32 Id { get; set; }
        
        public Boolean Deleted { get; set; }
    }
}