﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sp2.Models;
using VOM_HIVE.API.DTO.Product;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.Product;

namespace VOM_HIVE.API.Controllers
{
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
        public async Task<ActionResult<ResponseModel<List<ProductModel>>>> ListarProdutos()
        {
            var produtos = await _productInterface.ListarProdutos();
            return Ok(produtos);
        }

        [HttpGet("BuscarProdutoPorId/{id_product}")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> BuscarProdutoPorId(int id_product)
        {
            var produto = await _productInterface.BuscaProdutoPorId(id_product);
            return Ok(produto);
        }

        [HttpGet("BuscarProdutoPorIdCampaign/{id_campaign}")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> BuscarProdutoPorIdCampaign(int id_campaign)
        {
            var campanha = await _productInterface.BuscarProdutoPorIdCampaign(id_campaign);
            return Ok(campanha);
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var produtos = await _productInterface.CreateProduct(productCreateDto);
            return Ok(produtos);
        }

        [HttpPut("EditProduct")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> EditProduct(ProductEditDto productEditDto)
        {
            var product = await _productInterface.EditProduct(productEditDto);
            return Ok(product);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> DeleteProduct(int id_product)
        {
            var product = await _productInterface.DeleteProduct(id_product);
            return Ok(product);
        }
    }
}
