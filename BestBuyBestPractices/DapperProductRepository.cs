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
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@Name, @Price, @CategoryID);", new { Name = newProductName, Price = newProductPrice, CategoryID = newProductCategoryID });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;").ToList();
        }

        public void UpdateProduct(decimal newHoodiePrice)
        {
            _connection.Execute("UPDATE PRODUCTS SET Price = @Price WHERE ProductID = @prodID;");
        }
    }
}
