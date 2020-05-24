﻿using Bmes.Database;
using System.Collections.Generic;
using Bmes.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Bmes.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {

        private BmesDbContext _context;

        public ProductRepository(BmesDbContext context)
        {
            _context = context;
        }

        public Product FindProductById(long id)
        {
            var note = _context.Products.Find(id);
            return note;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var notes = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand);
            return notes;
        }

        public void SaveProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
