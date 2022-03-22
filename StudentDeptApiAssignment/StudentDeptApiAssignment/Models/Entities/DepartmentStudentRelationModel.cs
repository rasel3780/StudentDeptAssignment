using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDeptApiAssignment.Models.Entities
{
    public class DepartmentStudentRelationModel:DepartmentModel
    {
        public List<StudentModel> students { get; set; }

        public DepartmentStudentRelationModel()
        {
            students = new List<StudentModel>();
        }
    }
}