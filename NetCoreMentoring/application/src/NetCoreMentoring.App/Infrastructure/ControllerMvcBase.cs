using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Core.Utilities.ResultFlow;
using NuGet.Protocol.Core.v3;

namespace NetCoreMentoring.App.Infrastructure
{
    public class ControllerMvcBase : Controller
    {
        private readonly IMapper _mapper;

        public ControllerMvcBase(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        [NonAction]
        public IActionResult RequestResult<TResult>(
            Result<TResult> result,
            string viewName) 
            where TResult : class
        {
            return result.IsSuccess ? View(viewName, result.Value) : View("Error", result.Error.ToJson());
        }

        [NonAction]
        public IActionResult RequestResult<TResult, TViewModel>(
            Result<TResult> result,
            string viewName)
            where TResult : class
            where TViewModel : class
        {
            return result.IsSuccess ? View(viewName, _mapper.Map<TViewModel>(result.Value)) : View("Error", result.Error.ToJson());
        }

        [NonAction]
        public IActionResult RedirectToAction(
            Result result,
            string actionName)
        {
            return result.IsSuccess ? (IActionResult)RedirectToAction(actionName) : View("Error", result.Error.ToJson());
        }

        [NonAction]
        public IActionResult File(
            Result<byte[]> result,
            string contentType)
        {
            return result.IsSuccess ? (IActionResult)File(result.Value, contentType) : View("Error", result.Error.ToJson());
        }
    }
}
