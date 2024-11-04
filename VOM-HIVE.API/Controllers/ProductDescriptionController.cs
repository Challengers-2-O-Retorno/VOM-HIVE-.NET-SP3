using Microsoft.AspNetCore.Mvc;
using VOM_HIVE.API.Services.ProductDescription;

namespace VOM_HIVE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDescriptionController : ControllerBase
    {
        private readonly ProductDescriptionService _productDescriptionService;

        public ProductDescriptionController(ProductDescriptionService productDescriptionService)
        {
            _productDescriptionService = productDescriptionService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateDescription([FromBody] GenerateDescriptionRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Category) || string.IsNullOrWhiteSpace(request.Features))
            {
                return BadRequest("Categoria e características são obrigatórias.");
            }

            var description = _productDescriptionService.GenerateDescription(request.Category, request.Features);
            return Ok(new { Description = description });
        }
    }
    public class GenerateDescriptionRequest
    {
        public string Category { get; set; }
        public string Features { get; set; }
    }
}