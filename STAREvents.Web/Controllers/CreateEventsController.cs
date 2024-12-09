﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;
using System.Security.Claims;

namespace STAREvents.Web.Controllers
{
    [Authorize]
    public class CreateEventsController : Controller
    {
        private readonly ICreateEventsService createEventsService;

        public CreateEventsController(ICreateEventsService _createEventsService)
        {
            this.createEventsService = _createEventsService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateEventInputModel model = new CreateEventInputModel();
            model.Categories = await createEventsService.LoadCategoriesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await createEventsService.LoadCategoriesAsync();
                return View(model);
            }

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            var userId = new Guid(userIdValue);
            await createEventsService.CreateEventAsync(model, userId);
            return RedirectToAction("Index", "Home");
        }
    }
}
