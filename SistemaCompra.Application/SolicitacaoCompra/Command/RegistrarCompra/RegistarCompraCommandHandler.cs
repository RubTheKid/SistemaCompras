using MediatR;
using SistemaCompra.Application.Produto.Command.RegistrarProduto;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Infra.Data.Produto;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProdutoAggregate = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoCompraAggregate = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.Produto;


namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        SolicitacaoCompraAggregate.ISolicitacaoCompraRepository SolicitacaoCompraRepository;
        ProdutoAggregate.IProdutoRepository ProdutoRepository;

        public RegistrarCompraCommandHandler(
            SolicitacaoCompraAggregate.ISolicitacaoCompraRepository solicitacaoCompraRepository,
            ProdutoAggregate.IProdutoRepository produtoRepository,
            IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
                SolicitacaoCompraRepository = solicitacaoCompraRepository;
                ProdutoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var compra = new SolicitacaoCompraAggregate.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);
            compra.RegistrarCompra(FetchItems(request.Items));
            SolicitacaoCompraRepository.RegistrarCompra(compra);

            Commit();

            return Task.FromResult(true);

        }
        private List<SolicitacaoCompraAggregate.Item> FetchItems(List<RegistrarCompraCommand.Item> requestItems)
        {
            var items = new List<SolicitacaoCompraAggregate.Item>();
            foreach (var item in requestItems)
            {
                var produto = ProdutoRepository.Obter(item.Id);
                if(produto == null)
                {
                    throw new Exception("Produto não encontrado!");
                }
                items.Add(new SolicitacaoCompraAggregate.Item(produto, item.Qtd));
            }
            return items;
        }
    }
}
