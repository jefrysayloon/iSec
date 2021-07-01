using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using iSec.Libraries.TextEncryption;

namespace iSec.Libraries.Actions
{
    public class ServiceAction : IConfigureServicesAction
    {
        public int Priority => 0;

        public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
        {
            services.AddScoped(typeof(ITextEncryptionLibrary), typeof(TextEncryptionLibrary));
        }
    }
}
