﻿using Identity.Areas.Admin.Models.Email;
using Identity.Entities;
using Identity.Utilities.EmailHandler.Abstract;
using Identity.Utilities.EmailHandler.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Identity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public EmailController(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(EmailVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var users = new List<User>();
            if (model.ReceiverType == Constants.ReceiverType.AllUsers)
                users = _userManager.Users.ToList();
            else if (model.ReceiverType == Constants.ReceiverType.Subscribers)
                users = _userManager.Users.Where(u => u.IsSubscribed).ToList();
            else
                return NotFound();

            var url = Url.Action("Index", "Home", new { area = "" }, Request.Scheme);
            var messageContent = $"{model.Content}\n\n{url}";

            foreach (var user in users)
            {
                _emailService.SendMessage(new Message(new List<string> { user.Email }, model.Subject, messageContent));
            }

            TempData["Message"] = $"Emails sent to {users.Count} {((model.ReceiverType == Constants.ReceiverType.AllUsers) ? "users" : "subscribers")}.";
            return RedirectToAction("SendEmail");
        }
    }
}
