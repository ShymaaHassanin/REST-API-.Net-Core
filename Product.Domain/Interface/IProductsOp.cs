using Product.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Interface
{
    public interface IProductsOp
    {
        List<Model.Product> GetProducts();
        Model.Product GetProductById(int Id);
        Model.Product GetProductByCategoryId(int Id);
        Model.Product AddProduct(ProducToBeSaved product);
        Model.Product UpdateProduct(int ProductId, ProducToBeSaved product);
    }
}
