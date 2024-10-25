using Sp2.Models;
using VOM_HIVE.API.DTO.Product;
using VOM_HIVE.API.Models;

namespace VOM_HIVE.API.Services.Product
{
    public interface IProductInterface
    {
        Task<ResponseModel<List<ProductModel>>> ListarProdutos();
        Task<ResponseModel<ProductModel>> BuscaProdutoPorId(int id_product);
        Task<ResponseModel<ProductModel>> BuscarProdutoPorIdCampaign(int id_campaign);
        Task<ResponseModel<ProductModel>> CreateProduct(ProductCreateDto productCreateDto);
        Task<ResponseModel<ProductModel>> EditProduct(ProductEditDto productEditDto);
        Task<ResponseModel<ProductModel>> DeleteProduct(int id_product);
    }
}
