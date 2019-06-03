//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Z119.Model.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DayOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> PositionId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Deleted { get; set; }
    
        public virtual Position Position { get; set; }
        public virtual Role Role { get; set; }
        public virtual Unit Unit { get; set; }
    }
}