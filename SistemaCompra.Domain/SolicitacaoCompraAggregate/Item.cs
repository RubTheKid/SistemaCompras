using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class Item : Entity
    {
        public Produto Produto { get; set; }
        public int Qtd { get; set; }
        
        public SolicitacaoCompra SolicitacaoCompra {get; set; }

        public Money Subtotal => ObterSubtotal();

        public Item(Produto produto, int qtd)
        {
            Produto = produto ?? throw new ArgumentNullException(nameof(produto));
            Qtd = qtd;
        }

        private Money ObterSubtotal()
        {
            return new Money(Produto.Preco.Value * Qtd);
        }

        private Item() { }

    }
}
