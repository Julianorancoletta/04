using Delivery.Email.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Email.Worker.Application.Interfaces
{
    public interface IEmailService
    {
        public void envio(PessoaFisica pessoa);
    }
}
