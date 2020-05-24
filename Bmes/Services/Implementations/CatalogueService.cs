﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bmes.Models.Product;
using Bmes.Repositories;
using Bmes.ViewModels.Catalogue;
using Microsoft.AspNetCore.Http;

namespace Bmes.Services.Implementations
{
    public class CatalogueService : ICatalogueService
    {
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private readonly HttpContext _httpContext;
        private const int _productPerPage = 9;

        public CatalogueService(IHttpContextAccessor httpContextAccessor,
            IBrandRepository brandRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public PagedProductViewModel FetchProducts(string categorySlug, string brandSlug)
        {
            var brands = _brandRepository.GetAllBrands().Where(brand => brand.IsDeleted == false);
            var categories = _categoryRepository.GetAllCategories().Where(category => category.IsDeleted == false);

            var productPage = GetCurrentPage();
            IEnumerable<Product> products = new List<Product>();
            int productCount = 0;

            if (categorySlug == "all-categories" && brandSlug == "all-brands")
            {
                productCount = _productRepository.GetAllProducts().Count();
                products = _productRepository.GetAllProducts()
                   .Where(product => product.ProductStatus == ProductStatus.Active)
                   .Skip((productPage - 1) * _productPerPage)
                   .Take(_productPerPage);
            }

            if (categorySlug != "all-categories" && brandSlug != "all-brands")
            {
                var filteredProducts = _productRepository.GetAllProducts()
                                                         .Where(product => product.ProductStatus == ProductStatus.Active &&
                                                                           product.Category.Slug == categorySlug &&
                                                                           product.Brand.Slug == brandSlug);
                productCount = filteredProducts.Count();
                products = filteredProducts.Skip((productPage - 1) * _productPerPage)
                                           .Take(_productPerPage);
            }

            if (categorySlug != "all-categories" && brandSlug == "all-brands")
            {
                var filteredProducts = _productRepository.GetAllProducts()
                                                         .Where(product => product.ProductStatus == ProductStatus.Active &&
                                                                           product.Category.Slug == categorySlug);
                productCount = filteredProducts.Count();
                products = filteredProducts.Skip((productPage - 1) * _productPerPage)
                                           .Take(_productPerPage);
            }

            if (categorySlug == "all-categories" && brandSlug != "all-brands")
            {
                var filteredProducts = _productRepository.GetAllProducts()
                                                         .Where(product => product.ProductStatus == ProductStatus.Active &&
                                                                           product.Brand.Slug == brandSlug);
                productCount = filteredProducts.Count();
                products = filteredProducts.Skip((productPage - 1) * _productPerPage)
                                           .Take(_productPerPage);
            }

            var totalPages = (int)Math.Ceiling((decimal)productCount / _productPerPage);

            int[] pages = Enumerable.Range(1, totalPages).ToArray();


            var pagedProduct = new PaginationViewModel
            {
                Products = products,
                HasPreviousPages = (productPage > 1),
                CurrentPage = productPage,
                HasNextPages = (productPage < totalPages),
                Pages = pages
            };

            var pagedProducts = new PagedProductViewModel
            {
                PagedProducts = pagedProduct,
                Brands = brands,
                Categories = categories
            };

            return pagedProducts;
        }

        public int GetCurrentPage()
        {
            var defaultPage = 1;
            if (_httpContext.Request.Path.HasValue)
            {
                var pathValues = _httpContext.Request.Path.Value.Split('/');
                var pageFragment = pathValues[pathValues.Length - 1];

                if (!string.IsNullOrWhiteSpace(pageFragment) && pageFragment.Contains("page")) //page4
                {
                    var pageNumber = pageFragment.Last().ToString();
                    defaultPage = Convert.ToInt32(pageNumber);
                }
            }
            return defaultPage;
        }
    }
}
