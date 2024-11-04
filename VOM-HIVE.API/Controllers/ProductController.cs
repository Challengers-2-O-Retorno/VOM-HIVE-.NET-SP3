using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sp2.Models;
using VOM_HIVE.API.DTO.Product;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.Product;

namespace VOM_HIVE.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface _productInterface;

        public ProductController(IProductInterface productInterface)
        {
            _productInterface = productInterface;
        }

        [HttpGet("ListarProdutos")]
        [EndpointDescription("Endpoint responsável por listar todos os produtos cadastrados.")]
        public async Task<ActionResult<ResponseModel<List<ProductModel>>>> ListarProdutos()
        {
            var produtos = await _productInterface.ListarProdutos();
            return Ok(produtos);
        }

        [HttpGet("BuscarProdutoPorId/{id_product}")]
        [EndpointDescription("Endpoint responsável por listar um produto específico de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> BuscarProdutoPorId(int id_product)
        {
            var produto = await _productInterface.BuscaProdutoPorId(id_product);
            return Ok(produto);
        }

        [HttpGet("BuscarProdutoPorIdCampaign/{id_campaign}")]
        [EndpointDescription("Endpoint responsável por listar um produto específico de acordo com o Id de campanha.")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> BuscarProdutoPorIdCampaign(int id_campaign)
        {
            var campanha = await _productInterface.BuscarProdutoPorIdCampaign(id_campaign);
            return Ok(campanha);
        }

        [AllowAnonymous]
        [HttpPost("CreateProduct")]
        [EndpointDescription("Endpoint responsável por criar um novo produto.")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var produtos = await _productInterface.CreateProduct(productCreateDto);
            return Ok(produtos);
        }

        [HttpPut("EditProduct/{id_product}")]
        [EndpointDescription("Endpoint responsável por editar um produto de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> EditProduct(int id_product, [FromBody] ProductEditDto productEditDto)
        {
            if (id_product != productEditDto.id_product)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var product = await _productInterface.EditProduct(productEditDto);

            if (product.Dados == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("DeleteProduct/{id_product}")]
        [EndpointDescription("Endpoint responsável por deletar um produto de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> DeleteProduct(int id_product)
        {
            var product = await _productInterface.DeleteProduct(id_product);

            if (product.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
