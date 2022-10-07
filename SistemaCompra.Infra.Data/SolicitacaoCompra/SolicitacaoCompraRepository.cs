using System;
using System.Collections.Generic;
using System.Text;
using SolicitacaoCompraAggregate = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraRepository : SolicitacaoCompraAggregate.ISolicitacaoCompraRepository
    { 
        public readonly SistemaCompraContext context;
        public SolicitacaoCompraRepository(SistemaCompraContext context)
        {
            this.context = context;
        }
        public void RegistrarCompra(SolicitacaoCompraAggregate.SolicitacaoCompra solicitacaoCompra)
        {
            context.Set<SolicitacaoCompraAggregate.SolicitacaoCompra>().Add(solicitacaoCompra);
        }
    }
}