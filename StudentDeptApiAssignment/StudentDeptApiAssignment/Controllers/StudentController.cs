using AutoMapper;
using StudentDeptApiAssignment.Models.Database;
using StudentDeptApiAssignment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentDeptApiAssignment.Controllers
{
    public class StudentController : ApiController
    {
        [Route ("api/stu/add")]
        [HttpPost]
        public HttpResponseMessage Add(StudentModel studentModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StudentModel, Student>());
            var mapper = new Mapper(config);
            var stu = mapper.Map<Student>(studentModel);

            ApiAssignmentEntities db = new ApiAssignmentEntities();
            db.Students.Add(stu);
            db.SaveChanges();
            return Request.CreateResponse (HttpStatusCode.OK, "Student added");
        }
        
    }
}