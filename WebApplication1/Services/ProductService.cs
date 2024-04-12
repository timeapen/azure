using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{

    public class ProductService: IProductService
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductService> _logger;


        public ProductService(IConfiguration configuration, ILogger<ProductService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private SqlConnection GetConnection()
        {
            _logger.LogInformation("Getting SQL Connection");
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> _product_lst = new List<Product>();

            string statement = "SELECT ProductId,ProductName,Quantity from Products";

            conn.Open();

            SqlCommand command = new SqlCommand(statement, conn);

            using (SqlDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    _product_lst.Add(product);
                }
            }

            conn.Close();

            return _product_lst;

        }

    }

}
