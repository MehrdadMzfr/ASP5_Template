using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP5.Template.Security
{
    public class SecurityOptions
    {
        public string Authority { get; set; }
        public bool IsMonoEnvironment { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
    }
}