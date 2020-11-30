using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonnelDepartment.Data;
using PersonnelDepartment.Models;
using PersonnelDepartment.Services;
using PersonnelDepartment.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PersonnelDepartment.Controllers
{
    public class WorkersController : Controller
    {
        private readonly PDContext db;
        private readonly IConfiguration _configuration;

        public WorkersController(PDContext context, IConfiguration configuration)
        {
            db = context;
            _configuration = configuration;
        }

        public ActionResult Index() => View(db.Workers.ToList());

        public ActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(CreateWorkerViewModel model, string act)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }

                Worker worker = new Worker()
                {
                    FullName = model.FullName,
                    Education = model.Education,
                    Status = model.Status,
                    Email = model.Email,
                    EmailConfirmed = model.EmailConfirmed,
                    Position = model.Position,
                    Avatar = imageData
                };

                await db.AddAsync(worker);
                await db.SaveChangesAsync();

                if (act == "Confirm")
                {
                    return RedirectToAction(act, new { id = worker.Id });
                }
                return RedirectToAction(act);
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Worker worker = await db.Workers.FindAsync(id);
                if (worker != null)
                {
                    EditWorkerViewModel workerView = new EditWorkerViewModel()
                    {
                        Id = worker.Id,
                        FullName = worker.FullName,
                        Education = worker.Education,
                        Email = worker.Email,
                        EmailConfirmed = worker.EmailConfirmed,
                        Position = worker.Position,
                        Status = worker.Status,
                        Picture = worker.Avatar
                    };
                    return View(workerView);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditWorkerViewModel model, string act)
        {
            if (ModelState.IsValid)
            {
                Worker worker = await db.Workers.FindAsync(model.Id);

                if (model.Avatar != null)
                {
                    byte[] imageData = null;

                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }

                    worker.Avatar = imageData;
                }

                worker.FullName = model.FullName;
                worker.Email = model.Email;
                worker.EmailConfirmed = model.EmailConfirmed;
                worker.Position = model.Position;
                worker.Status = model.Status;
                worker.Education = model.Education;


                await db.SaveChangesAsync();

                if (act == "Confirm")
                {
                    return RedirectToAction(act, new { id = worker.Id });
                }
                return RedirectToAction(act);
            }
            return View(model);
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (id != null)
            {
                Worker worker = await db.Workers.FindAsync(id);
                if (worker != null)
                {
                    string guid = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("Password", guid);

                    EmailService service = new EmailService();
                    await service.SendEmailAsync(
                        email: worker.Email,
                        subject: "Подтвердите ваш аккаунт",
                        message: $"Пароль: {guid}",
                        configuration: _configuration);

                    ConfirmViewModel model = new ConfirmViewModel() { WorkerId = worker.Id };

                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(ConfirmViewModel model)
        {
            if (ModelState.IsValid)
            {
                string password = HttpContext.Session.GetString("Password");
                Worker worker = await db.Workers.FindAsync(model.WorkerId);

                if (worker != null)
                {
                    if (password == model.Password)
                    {
                        worker.EmailConfirmed = true;
                        await db.SaveChangesAsync();
                    }
                    return View("Confirmed", worker);
                }
                return NotFound();
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
