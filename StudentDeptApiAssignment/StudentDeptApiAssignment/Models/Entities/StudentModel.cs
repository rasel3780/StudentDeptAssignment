using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDeptApiAssignment.Models.Entities
{
    public class StudentModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Department { get; set; }  
        public int DeptID { get; set; } 
    }
}