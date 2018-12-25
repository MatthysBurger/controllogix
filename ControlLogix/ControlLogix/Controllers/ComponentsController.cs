using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlLogix.ProcessConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPA_T3000.Process_Connection;

namespace ControlLogix.Controllers
{
    public class ComponentsController : Controller
    {
        // GET: Components
        public ActionResult Index(string searchString)
        {
            LogicBlock[] lbs = ProcessConnectionManager.GetLogicBlocks();
            if (lbs == null)
            {
                LogicBlock lb_Error = new LogicBlock();
                lb_Error.ID = -1;
                lb_Error.TypeName = "error. nothing found";
                lbs = new LogicBlock[1] { lb_Error };

                return View(lb_Error);
            }
            else
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    lbs = lbs.Where(s => s.ID.ToString().Contains(searchString) || s.TypeName.Contains(searchString)).ToArray();
                }
            }
            return View(lbs);
        }

        // GET: Components/Details/5
        public ActionResult Details(int id)
        {
            LogicBlock lb = ProcessConnectionManager.GetLogicBlock(id);
            if (lb != null)
            {
                return View(lb);
            }
            else
            {
                LogicBlock lb_Error = new LogicBlock();
                lb_Error.ID = -1;
                lb_Error.Item = string.Empty;
                lb_Error.Tag = string.Empty;
                lb_Error.TypeName = "not found";
                lb_Error.Designation = "Component could not be located";

                return View(lb_Error);
            }
        }

        // GET: Components/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Components/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Components/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Components/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Components/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Components/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}