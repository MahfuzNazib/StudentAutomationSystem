using System;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using System.Configuration;

namespace StudentAutomationSystem.Models.Product
{
    public class ProductDBHandel
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public ProductDBHandel(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Add New Product
        public bool AddNewProduct(Product product)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            String sql = "INSERT INTO Products (ProductName, ProductDescription, Price)" +
                "VALUES (@ProductName, @ProductDescription, @Price)";
            
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                int rowAffected = cmd.ExecuteNonQuery();

                if(rowAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                connection.Close();
            }
        }


        // Show All Product
        public List<Product> GetAllProduct()
        {
            List<Product> products = new List<Product>();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = "SELECT * FROM Products ORDER BY ProductId ASC";
            using(SqlCommand cmd = new SqlCommand(sql, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Product product = new Product();
                        product.ProductName = reader["ProductName"].ToString();
                        product.ProductDescription = reader["ProductDescription"].ToString();
                        product.Price = Convert.ToDouble(reader["Price"]);
                        products.Add(product);
                    }
                }
            }
            return products;
        }
    }
}
