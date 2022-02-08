using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;



namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperDepartmentRepository(conn);

            Console.WriteLine($"Type a new Department Name\n");

            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);

            var departments = repo.GetAllDepartments();

            Console.WriteLine($"List of All Departments");
            foreach (var dept in departments)
            {
                Console.WriteLine($"\n{dept.Name}\n");
            }

            //Below follows a notated Implementation of Product Class in Program's Main to create a new product (CreateProduct method)
            //and get all products (GetAllProducts method)

            var productRepo = new DapperProductRepository(conn); //creates new object of DapperProductRepository

            //Take user input for new prodct's name
            Console.WriteLine($"\nType new Product's name.\n");
            var firstNewName = Console.ReadLine();

            //Take user input for new product's price utilizing TryParse() method to convert user string input to double data type,
            //while anticipating user's discretion to be on the safe side
            Console.WriteLine($"\nType new Product's price.\n");
            decimal userFirstNewPrice;
            var firstNewPrice = decimal.TryParse(Console.ReadLine(), out userFirstNewPrice);

            //Take user input for new product's category ID utilizing TryParse() method to convert user string input to int data type,
            //while anticipating user's discretion to be on the safe side
            Console.WriteLine($"\nType new Product's category ID.\n");
            int userFirstNewID;
            var firstNewID = int.TryParse(Console.ReadLine(), out userFirstNewID);

            productRepo.CreateProduct(firstNewName, userFirstNewPrice, userFirstNewID); //Caling CreateProduct method and passing paramters

            //Creates a new variable object to hold GetAllProducts() method call
            var products = productRepo.GetAllProducts();

            //Uses a foreach loop to iterate through and return/display all products from our GetAllProducts() method call
            Console.WriteLine($"\nHERE'S A LIST OF ALL PRODUCTS:");
            foreach (var product in products)
            {
                Console.WriteLine($"\n{product.Name}, {product.Price}, {product.CategoryID}\n");
            }

            //BONUS: Updating a product using UpdateProduct() method based on primary key(PK) ProductID - NOT YET COMPLETE
            //Console.WriteLine($"Type the Product ID being updated.\n");
            //int userUpdateThisID;
            //var updateThisProduct = int.TryParse(Console.ReadLine(), out userUpdateThisID);

            //productRepo.UpdateProduct(userUpdateThisID);

        }
    }
}

