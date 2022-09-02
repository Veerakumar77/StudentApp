using StudentRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentRestApi.Controllers
{
    public class StudentController : ApiController
    {
        crudEntities db = new crudEntities();

        [HttpGet]
        [Route("api/crudApi/student")]
        public IHttpActionResult GetStudents()
        {
            List<student> list = db.students.ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("api/crudApi/student/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var x = db.students.Where(model => model.RegNo == id).FirstOrDefault();
            return Ok(x);
        }

        [HttpPost]
        [Route("api/crudApi/student")]
        public HttpResponseMessage create(student model)
        {
            if (ModelState.IsValid)
            {
                if (db.students.Any(x => x.StudentName == model.StudentName))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "StudentName is Already Exists");
                }
                else
                {
                    db.students.Add(model);
                    db.SaveChanges(); 
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            
        }

        [HttpPut]
        [Route("api/crudApi/student/{id}")]
        public IHttpActionResult Edit(student s)
        {
            var x = db.students.Where(model => model.RegNo == s.RegNo).FirstOrDefault();
            if (x != null)
            {
                x.RegNo = s.RegNo;
                x.StudentName = s.StudentName;
                x.SubjectId = s.SubjectId;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();


        }

        [HttpDelete]
        [Route("api/crudApi/student/{id}")]
        public IHttpActionResult delete(int id)
        {
            var x = db.students.Find(id);
            db.students.Remove(x);
            db.SaveChanges();
            return Ok();
        }
    }
}
