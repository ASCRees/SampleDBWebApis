using SampleDBWebApis.Models;

namespace SampleDBWebApis.ModelBuilders
{
    public interface IProductModelBuilders
    {
        ProductViewModel BuildPutProductModel(ProductViewModel productModel);
        ProductPatchViewModel BuildPatchProductModel(ProductPatchViewModel productModel);
    }
}