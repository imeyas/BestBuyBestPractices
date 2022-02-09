using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        //Constructor
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CreateProduct(string newProductName, decimal newProductPrice, int newProductCategoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@Name, @Price, @CategoryID);", 
                new { Name = newProductName, Price = newProductPrice, CategoryID = newProductCategoryID });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;").ToList();
        }

        public void UpdateProduct(double newProductPrice, int newProductID)
        {
            _connection.Execute("UPDATE Products SET Price = @Price WHERE ProductID = @ProdID;", 
                new {Price = newProductPrice, ProdID = newProductID });
        }

        public void DeleteProduct(int deleteProductCategoryID)
        {
            _connection.Execute("DELETE FROM PRODUCTS WHERE ProductID = @ProdID;",
                new { ProdID = deleteProductCategoryID });
        }
    }
}
