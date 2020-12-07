using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.App.Areas.Identity;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.Core.Services;
using NetCoreMentoring.Core.Utilities.ResultFlow;
using NetCoreMentoring.Data;

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
            return RequestResult(Result.Success(_context.Users.ToList()), View().ViewName);
        }
    }
}
