using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlLogix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlLogix.Controllers
{
    public class LibrariesController : Controller
    {
        private static Dictionary<int, Library> _libraries = new Dictionary<int, Library>();
        static LibrariesController()
        {
            _libraries.Add(1, new Library { PrimaryKey = 1, Name = "Basic", Description = "Basic control/logic blocks" });

            _libraries[1].ControlBlocks.Add(1, new ControlBlock() { Description = "Add two values", PrimaryKey = 1, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(2, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 2, Name = "PID" });
            _libraries[1].ControlBlocks.Add(3, new ControlBlock() { Description = "Add two values", PrimaryKey = 3, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(4, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 4, Name = "PID" });
            _libraries[1].ControlBlocks.Add(5, new ControlBlock() { Description = "Add two values", PrimaryKey = 5, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(6, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 6, Name = "PID" });
            _libraries[1].ControlBlocks.Add(7, new ControlBlock() { Description = "Add two values", PrimaryKey = 7, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(8, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 8, Name = "PID" });
            _libraries[1].ControlBlocks.Add(9, new ControlBlock() { Description = "Add two values", PrimaryKey = 9, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(10, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 10, Name = "PID" });
            _libraries[1].ControlBlocks.Add(11, new ControlBlock() { Description = "Add two values", PrimaryKey = 11, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(12, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 12, Name = "PID" });
            _libraries[1].ControlBlocks.Add(13, new ControlBlock() { Description = "Add two values", PrimaryKey = 13, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(14, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 14, Name = "PID" });
            _libraries[1].ControlBlocks.Add(15, new ControlBlock() { Description = "Add two values", PrimaryKey = 15, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(16, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 16, Name = "PID" });
            _libraries[1].ControlBlocks.Add(17, new ControlBlock() { Description = "Add two values", PrimaryKey = 17, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(18, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 18, Name = "PID" });
            _libraries[1].ControlBlocks.Add(19, new ControlBlock() { Description = "Add two values", PrimaryKey = 19, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(20, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 20, Name = "PID" });
            _libraries[1].ControlBlocks.Add(21, new ControlBlock() { Description = "Add two values", PrimaryKey = 21, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(22, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 22, Name = "PID" });
            _libraries[1].ControlBlocks.Add(23, new ControlBlock() { Description = "Add two values", PrimaryKey = 23, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(24, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 24, Name = "PID" });
            _libraries[1].ControlBlocks.Add(25, new ControlBlock() { Description = "Add two values", PrimaryKey = 25, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(26, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 26, Name = "PID" });
            _libraries[1].ControlBlocks.Add(27, new ControlBlock() { Description = "Add two values", PrimaryKey = 27, Name = "ADD" });
            _libraries[1].ControlBlocks.Add(28, new ControlBlock() { Description = "Simple PID Controller", PrimaryKey = 28, Name = "PID" });
            _libraries[1].ControlBlocks.Add(29, new ControlBlock() { Description = "Add two values", PrimaryKey = 29, Name = "ADD" });

            _libraries.Add(101, new Library { PrimaryKey = 101, Name = "Advanced", Description = "Basic control/logic blocks" });

            _libraries[101].ControlBlocks.Add(1, new ControlBlock() { Description = "Advanced PID Controller", PrimaryKey = 1, Name = "Adv. PID" });
        }
    
        // GET: Find
        public ActionResult Index()
        {
            return View(_libraries.Values.ToArray());
        }

        // GET: Find/Details/5
        public ActionResult Details(int id)
        {
            if (_libraries.ContainsKey(id))
            {
                return View(_libraries[id]);
            }
            else
            {
                return View();
            }
        }

        // GET: Find/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Find/Create
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

        // GET: Find/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Find/Edit/5
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

        // GET: Find/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Find/Delete/5
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