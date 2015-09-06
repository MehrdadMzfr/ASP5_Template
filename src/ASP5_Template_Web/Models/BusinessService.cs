namespace ASP5_Template_Web.Models
{
    public class BusinessService: IBusinessService
    {
        private readonly IDataLayer _dataLayer;

        public BusinessService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
    }
}
