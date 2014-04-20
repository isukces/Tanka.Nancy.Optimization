using Nancy;
using Nancy.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanka.Nancy.Optimization
{
    public class Startup : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            // throw new NotImplementedException();
        }
        public Startup(IRootPathProvider rootPathProvider)
        {
            Bundle.rootPathProvider = rootPathProvider;
        }
    }
}
