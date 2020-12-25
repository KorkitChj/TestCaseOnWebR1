using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebTest2.Models;

namespace WebTest2.Controllers
{
    public class list_module_name_tableController : ApiController
    {
        //private DBModels db = new DBModels();

        // modify the type of the db field
        private WebTest2Context db = new DBModels();

        // add these constructors
        public list_module_name_tableController() { }

        public list_module_name_tableController(WebTest2Context context)
        {
            db = context;
        }
        // rest of class not shown

        // GET: api/list_module_name_table
        public IQueryable<list_module_name_table> Getlist_module_name_table()
        {
            return db.list_Module_Name_Tables;
        }

        // GET: api/list_module_name_table/5
        [ResponseType(typeof(list_module_name_table))]
        public IHttpActionResult Getlist_module_name_table(int id)
        {
            list_module_name_table list_module_name_table = db.list_Module_Name_Tables.Find(id);
            if (list_module_name_table == null)
            {
                return NotFound();
            }

            return Ok(list_module_name_table);
        }

        // PUT: api/list_module_name_table/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlist_module_name_table(int id, list_module_name_table list_module_name_table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != list_module_name_table.module_id)
            {
                return BadRequest();
            }

            //db.Entry(list_module_name_table).State = EntityState.Modified;
            db.MarkAsModified(list_module_name_table);

            /*try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!list_module_name_tableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/list_module_name_table
        [ResponseType(typeof(list_module_name_table))]
        public IHttpActionResult Postlist_module_name_table(list_module_name_table list_module_name_tables)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.list_Module_Name_Tables.Add(list_module_name_tables);
            db.SaveChanges();      

            return CreatedAtRoute("DefaultApi", new { id = list_module_name_tables.module_id }, list_module_name_tables);
        }

        // DELETE: api/list_module_name_table/5
        [ResponseType(typeof(list_module_name_table))]
        public IHttpActionResult Deletelist_module_name_table(int id)
        {
            list_module_name_table list_module_name_table = db.list_Module_Name_Tables.Find(id);
            if (list_module_name_table == null)
            {
                return NotFound();
            }

            db.list_Module_Name_Tables.Remove(list_module_name_table);
            db.SaveChanges();

            return Ok(list_module_name_table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool list_module_name_tableExists(int id)
        {
            return db.list_Module_Name_Tables.Count(e => e.module_id == id) > 0;
        }
    }
}