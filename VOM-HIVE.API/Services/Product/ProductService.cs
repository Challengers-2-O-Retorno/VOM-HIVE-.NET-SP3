using Microsoft.EntityFrameworkCore;
using Sp2.Models;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.DTO.Product;
using VOM_HIVE.API.Models;

namespace VOM_HIVE.API.Services.Product
{
    public class ProductService : IProductInterface
    {
        public readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<ProductModel>> BuscaProdutoPorId(int id_product)
        {
            ResponseModel<ProductModel> resposta = new ResponseModel<ProductModel>();
            try
            {
                var produto = await _context.Product.FirstOrDefaultAsync(produtoBanco => produtoBanco.id_product  == id_product);

                if (produto == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado!";
                    return resposta;
                }

                resposta.Dados = produto;
                resposta.Mensagem = "Produto localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ProductModel>> BuscarProdutoPorIdCampaign(int id_campaign)
        {
            ResponseModel<ProductModel> resposta = new ResponseModel<ProductModel>();
            try
            {
                var campanha = await _context.Campaign
                    .Include(p => p.Product)
                    .FirstOrDefaultAsync(campaignBanco => campaignBanco.id_campaign == id_campaign);

                if (campanha == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado!";
                    return resposta;
                }

                resposta.Dados = campanha.Product;
                resposta.Mensagem = "Autor localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ProductModel>>> CreateProduct(ProductCreateDto productCreateDto)
        {
            ResponseModel<List<ProductModel>> resposta = new ResponseModel<List<ProductModel>>();
            try
            {
                var product = new ProductModel()
                {
                    nm_product = productCreateDto.nm_product,
                    category_product = productCreateDto.category_product
                };

                _context.Add(product);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Product.ToListAsync();
                resposta.Mensagem = "Autor criado com sucesso!";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ProductModel>>> DeleteProduct(int id_product)
        {
            ResponseModel<List<ProductModel>> resposta = new ResponseModel<List<ProductModel>>();

            try
            {
                var product = await _context.Product.FirstOrDefaultAsync(productBanco => productBanco.id_product == id_product);

                if (product == null)
                {
                    resposta.Mensagem = "Nenhum autor localizado!";
                    return resposta;
                }

                _context.Remove(product);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Product.ToListAsync();
                resposta.Mensagem = "Produto removido com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ProductModel>>> EditProduct(ProductEditDto productEditDto)
        {
            ResponseModel<List<ProductModel>> resposta = new ResponseModel<List<ProductModel>>();

            try
            {
                var produto = await _context.Product
                    .FirstOrDefaultAsync(
                    productbanco => productbanco.id_product == productEditDto.id_product
                    );

                if (produto == null)
                {
                    resposta.Mensagem = "Nenhum produto localizado";
                    return resposta;
                }
                produto.nm_product = productEditDto.nm_product;
                produto.category_product = productEditDto.category_product;

                _context.Update(produto);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Product.ToListAsync();
                resposta.Mensagem = "Produto editado com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ProductModel>>> ListarProdutos()
        {
            ResponseModel<List<ProductModel>> resposta = new ResponseModel<List<ProductModel>>();
            try
            {
                var produtos = await _context.Product.ToListAsync();

                resposta.Dados = produtos;
                resposta.Mensagem = "Todos os produtos foram coletados!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
