using AutoMapper;
using NetCoreMentoring.App.Infrastructure;

namespace NetCoreMentoring.App.ApiControllers
{
    public class ProductController : ControllerApiBase
    {
        private readonly IMapper _mapper;

        public ProductController(
            IMapper mapper)
            :base(mapper)
        {
            _mapper = mapper;
        }
    }
}
