using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Core.Utilities.ResultFlow;
using NuGet.Protocol.Core.v3;

namespace NetCoreMentoring.App.Infrastructure
{
    public class ControllerApiBase : ControllerBase
    {
        private readonly IMapper _mapper;

        public ControllerApiBase(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        [NonAction]
        public IActionResult RequestResult(
            Result result,
            HttpStatusCode status = HttpStatusCode.OK)
        {
            return result.IsSuccess ? StatusCode((int)status) : GetError(result);
        }

        [NonAction]
        public IActionResult RequestResult<TResult, TViewModel>(
            Result<TResult> result,
            HttpStatusCode status = HttpStatusCode.OK)
            where TResult : class
            where TViewModel : class
        {
            return result.IsSuccess ? StatusCode((int)status, _mapper.Map<TViewModel>(result.Value)) : GetError(result);
        }

        [NonAction]
        public IActionResult File(
            Result<byte[]> result,
            string contentType)
        {
            return result.IsSuccess ? File(result.Value, contentType) : GetError(result);
        }

        private IActionResult GetError(Result result)
        {
            return result.Status switch
            {
                ResultStatus.NotFound => NotFound(result.Error.ToJson()),
                ResultStatus.ValidationError => BadRequest(result.Error.ToJson()),
                ResultStatus.Failed => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.ToJson()),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }
    }
}
