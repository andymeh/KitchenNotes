//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Tasks
    {
        public System.Guid TaskId { get; set; }
        public string TaskDetail { get; set; }
        public string AssignedTo { get; set; }
        public System.Guid UserHubId { get; set; }
        public bool Completed { get; set; }
        public bool Hidden { get; set; }
        public System.DateTime DatePosted { get; set; }
    
        public virtual UserHub UserHub { get; set; }
    }
}