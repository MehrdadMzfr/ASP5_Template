using ASP5.Template.Core.Entities;
using System.Collections.Generic;

namespace ASP5.Template.Core
{
    public static class LINQExtensions
    {
        public static void AddOrUpdate<T>(this IEnumerable<T> list, Manufacturer manufacturer)
        {
            using (var context = new Context())
            {
            }
        }
    }
}
