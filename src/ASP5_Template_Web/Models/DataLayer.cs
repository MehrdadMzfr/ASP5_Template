namespace ASP5_Template_Web.Models
{
    public class DataLayer: IDataLayer
    {
        private readonly string _connectionString;

        public DataLayer(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
