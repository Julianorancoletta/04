using Cadastro_API.Core.Interface.Repositorio;
using Cadastro_API.Core.Interface.Service;
using Cadastro_API.Core.Service;
using Cadastro_API.Data.Repositorio;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_API.CrossCutting.Injection
{
    public static class ServiceInjection
    {
        public static void Inject(IServiceCollection? _serviceProvider)
        {
            _serviceProvider?.AddTransient<ICadastroService, CadastroService>();
        }
    }
}
