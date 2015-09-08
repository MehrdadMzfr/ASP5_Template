using System.Collections.Generic;
using System.Linq;
using ASP5.Template.Core.Entities;

namespace ASP5.Template.Core
{
    public class EfBusinessLayer
    {
        public EfBusinessLayer()
        {
            AddDefaultData();
        }
        private void AddDefaultData()
        {
            using (var context = new Context())
            {
                var manufacturer = new Manufacturer { Id = 1, Name = "Ford" };
                var data = context.Manufacturers.Add(manufacturer);
                context.Manufacturers.AddOrUpdate(manufacturer);
                var listLength = GetData().Count;
                //context.SaveChanges();
            }
        }

        public List<Manufacturer> GetData()
        {
            using (var context = new Context())
            {
                var manufacturers = context.Manufacturers.ToList();
                return manufacturers;
            }
        }
    }
}
