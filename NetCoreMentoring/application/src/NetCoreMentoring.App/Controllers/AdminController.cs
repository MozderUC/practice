using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.App.Areas.Identity;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.Core.Utilities.ResultFlow;
using NuGet.Protocol.Core.v3;

namespace NetCoreMentoring.App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerMvcBase
    {
        private readonly IdentityContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            IdentityContext context,
            ILogger<AdminController> logger,
            IMapper mapper)
            :base(mapper)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                return RequestResult(Result.Success(_context.Users.ToList()), View().ViewName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(Index));
                return View("Error", e.ToJson());
            }
        }
    }
}
