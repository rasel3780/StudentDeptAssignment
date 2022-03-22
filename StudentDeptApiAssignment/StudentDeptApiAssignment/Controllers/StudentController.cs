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
        [Route ("api/stu/list")]
        [HttpGet]
        public HttpResponseMessage ShowList()
        {
            ApiAssignmentEntities db = new ApiAssignmentEntities ();
            var studnets = db.Students.ToList();

            var config = new MapperConfiguration(cfg=>cfg.CreateMap<StudentModel, Student>());
            var mapper = new Mapper(config);
            var stu = mapper.Map<List<StudentModel>>(studnets);
            return Request.CreateResponse(HttpStatusCode.OK, stu);
        }

        [Route("api/stu/delete/{id}")]
        [HttpDelete]
        
        public HttpResponseMessage Delete(int id)
        {
            ApiAssignmentEntities db = new ApiAssignmentEntities();
            var student = (from stu in db.Students
                       where stu.Id.Equals(id)
                       select stu).FirstOrDefault();
            db.Students.Remove(student);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }
        [Route ("api/stu/update/")]
        [HttpPut]
        public HttpResponseMessage Update(StudentModel studentModel)
        {
            ApiAssignmentEntities db = new ApiAssignmentEntities();
            var student = (from stu in db.Students
                           where stu.Id.Equals (studentModel.Id)
                           select stu).FirstOrDefault ();
            var config = new MapperConfiguration(cfg => cfg.CreateMap < StudentModel, Student >());
            var mapper = new Mapper(config);
            var newValues = mapper.Map<Student>(studentModel);

            db.Entry(student).CurrentValues.SetValues(newValues);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Updated");
        }
        [Route("api/dept/details/{id}")]
        [HttpGet]
        public HttpResponseMessage DeptDetails(int id)
        {
            ApiAssignmentEntities db = new ApiAssignmentEntities();
            var students = (from stu in db.Students
                            where stu.DeptId.Equals(id)
                            select stu).ToList();

            var department = (from dept in db.Departments
                        where dept.Id.Equals(id)
                        select dept).FirstOrDefault();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentModel>());
            var mapper = new Mapper(config);
            var studentList = mapper.Map<List<StudentModel>>(students);

            DepartmentStudentRelationModel deptStuRelation = new DepartmentStudentRelationModel();
            deptStuRelation.Id = department.Id;
            deptStuRelation.Name = department.Name;
            deptStuRelation.students = studentList;
            return Request.CreateResponse(HttpStatusCode.OK, deptStuRelation);
        }
    }
}