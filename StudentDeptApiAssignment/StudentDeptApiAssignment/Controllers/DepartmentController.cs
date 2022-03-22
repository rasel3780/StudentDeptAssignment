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
    public class DepartmentController : ApiController
    {
        [Route("api/dept/add")]
        [HttpPost]

        public HttpResponseMessage Add(DepartmentModel departmentModel)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentModel, Department>());
            var mapper = new Mapper(config);
            var dept = mapper.Map<Department>(departmentModel);

            ApiAssignmentEntities db = new ApiAssignmentEntities();
            db.Departments.Add(dept);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Department added");

        }
        [Route("api/dept/list")]
        [HttpGet]
        public HttpResponseMessage ShowList()
        {
            ApiAssignmentEntities db = new ApiAssignmentEntities();
            var dept = db.Departments.ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Department, DepartmentModel>());
            var mapper = new Mapper(config);
            var deptModel = mapper.Map<List<DepartmentModel>>(dept);
            return Request.CreateResponse(HttpStatusCode.OK, deptModel);
        }

        [Route("api/dept/delete/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            ApiAssignmentEntities db = new ApiAssignmentEntities();
            var department = (from dept in db.Departments
                           where dept.Id.Equals(id)
                           select dept).FirstOrDefault();
            db.Departments.Remove(department);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }
    }
}