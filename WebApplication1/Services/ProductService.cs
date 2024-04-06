using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{

    public class ProductService
    {

        private static string db_source = "sidhusanto.database.windows.net";
        private static string db_user = "santo";
        private static string db_password = "Sidissmall13#";
        private static string db_database = "santodb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;

            return new SqlConnection(_builder.ConnectionString);
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
