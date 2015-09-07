using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                context.Manufacturers.Add(new Manufacturer
                {
                    Name = "Ford"
                });
                context.SaveChanges();
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
