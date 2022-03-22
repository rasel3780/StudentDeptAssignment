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
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentModel, Department>());
                var mapper = new Mapper(config);
                var dept = mapper.Map<Department>(departmentModel);

                ApiAssignmentEntities db = new ApiAssignmentEntities();
                db.Departments.Add(dept);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Department added");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

        }
   
    }
}