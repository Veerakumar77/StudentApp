using StudentRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentRestApi.Controllers
{
    public class CrudApiController : ApiController
    {
        crudEntities db = new crudEntities();

        [HttpGet]
        [Route ("api/crudApi/subject")]
        public IHttpActionResult GetSubject()
        {
            List<subject> list = db.subjects.ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("api/crudApi/subject/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var x = db.subjects.Where(model => model.SubjectId == id).FirstOrDefault();
            return Ok(x);
        }

        [HttpPost]
        [Route("api/crudApi/subject")]
        public IHttpActionResult create(subject model)
        {
            
            db.subjects.Add(model);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/crudApi/subject/{id}")]
        public IHttpActionResult Edit(subject s)
        {
            var x = db.subjects.Where(model => model.SubjectId == s.SubjectId).FirstOrDefault();
            if (x != null)
            {
                x.SubjectId = s.SubjectId;
                x.Subjects = s.Subjects;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();

            
        }

        [HttpDelete]
        [Route("api/crudApi/subject/{id}")]
        public IHttpActionResult delete(int id)
        {
            var x = db.subjects.Where(model => model.SubjectId == id).FirstOrDefault();
            db.Entry(x).State = System.Data.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}
