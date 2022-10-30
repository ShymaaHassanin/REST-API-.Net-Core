using AutoMapper;
using Product.Domain.Interface;
using Product.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product.Domain.Handlers
{
    public class ProductsOp : IProductsOp
    {
        #region Variables  
        private readonly IRepository<Model.Product> _productModelRepository;
        private readonly IRepository<Category> _categoryModelRepository;
        private readonly IMapper _mapper;
        #endregion
        public ProductsOp(IRepository<Model.Product> productModelRepository, IRepository<Category> categoryModelRepository,
             IMapper mapper)
        {
            _productModelRepository = productModelRepository;
            _categoryModelRepository = categoryModelRepository;
            _mapper = mapper;
        }
        public List<Model.Product> GetProducts()
        {
            return _productModelRepository.GetAll().Select(_ => new Product.Domain.Model.Product() { Id = _.Id, Name = _.Name, Category = _.Category, ImgURL = _.ImgURL, Price = _.Price, Quantity = _.Quantity }).Distinct().OrderBy(s => s.Name).ToList();
        }

        public Model.Product GetProductByCategoryId(int Id)
        {
            var category = _categoryModelRepository.GetAll().Where(_ => _.Id == Id).FirstOrDefault(); 
            if(category == null)
            {
               throw new ArgumentException("Category Id not found");
            }
            var selectProduct = _productModelRepository.GetAll().Where(_ => _.CategoryID == Id).Select(_ => new Product.Domain.Model.Product() { Id = _.Id, Name = _.Name, Category = _.Category, ImgURL = _.ImgURL, Price = _.Price, Quantity = _.Quantity }).FirstOrDefault();
            if (selectProduct == null) throw new ArgumentNullException(nameof(selectProduct));
            return selectProduct;
        }

        public Model.Product GetProductById(int Id)
        {
            if (Id < 1) throw new ArgumentException("product Id can't less than 1");

            var selectProduct = _productModelRepository.GetAll().Where(_ => _.Id == Id).Select(_ => new Product.Domain.Model.Product() { Id = _.Id, Name = _.Name, Category = _.Category, ImgURL = _.ImgURL, Price = _.Price, Quantity = _.Quantity }).FirstOrDefault();
            if (selectProduct == null) throw new ArgumentNullException(nameof(selectProduct));
            return selectProduct;
        }

        public Model.Product AddProduct(ProducToBeSaved productToBeSaved)
        {
            if (productToBeSaved == null) throw new ArgumentNullException(nameof(productToBeSaved));
            if (string.IsNullOrEmpty(productToBeSaved.Name)) throw new ArgumentNullException("Product Name is mandatory");
            var category = _categoryModelRepository.GetAll().Where(_ => _.Id == productToBeSaved.CategoryID).FirstOrDefault();
            if (category == null)
            {
                throw new ArgumentException("Category Id not found");
            }
            var createdProduct  = new Model.Product
            {
                Name = productToBeSaved.Name,
                ImgURL = productToBeSaved.ImgURL,
                Price = productToBeSaved.Price,
                Quantity = productToBeSaved.Quantity,
                CategoryID = productToBeSaved.CategoryID
            };
            _productModelRepository.Add(createdProduct);
            _productModelRepository.UnitOfWork.Commit();
            return GetProductByCategoryId(createdProduct.Id);
        }

        public Model.Product UpdateProduct(int ProductId, ProducToBeSaved productToBeSaved)
        {
            if (productToBeSaved == null) throw new ArgumentNullException(nameof(productToBeSaved));
           
            var savedProduct = _productModelRepository.GetAll().Where(_ => _.Id == ProductId).FirstOrDefault();
            if (savedProduct == null)
            {
                throw new ArgumentException("Product is not found");
            }
            //var category = _categoryModelRepository.GetAll().Where(_ => _.Id == productToBeSaved.CategoryID).FirstOrDefault();
            //if (category == null)
            //{
            //    throw new ArgumentException("Category Id not found");
            //}
            //if (string.IsNullOrEmpty(productToBeSaved.Name)) throw new ArgumentNullException("Product Name is mandatory");

            //savedProduct.ImgURL = productToBeSaved.ImgURL;
            //savedProduct.Name = productToBeSaved.Name;
            //savedProduct.Price = productToBeSaved.Price;
            //savedProduct.Quantity = productToBeSaved.Quantity;
            //savedProduct.CategoryID = productToBeSaved.CategoryID;

            _mapper.Map(productToBeSaved, savedProduct);
            _productModelRepository.Update(savedProduct);
            _productModelRepository.UnitOfWork.Commit();
            return GetProductByCategoryId(savedProduct.Id);
        }
    }
}
