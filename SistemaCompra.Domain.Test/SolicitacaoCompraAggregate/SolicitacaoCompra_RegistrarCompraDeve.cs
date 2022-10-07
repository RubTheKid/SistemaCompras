using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_RegistrarCompraDeve
    {
        [Fact]
        public void DefinirPrazo30DiasAoComprarMais50mil()
        {
            //dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("cedro", "transversal 3/3", 1001, Categoria.Madeira.ToString());
            itens.Add(new Item(produto, 50));

            //quando
            solicitacao.RegistrarCompra(itens);

            //então
            Assert.Equal(30, solicitacao.CondicaoPagamento.Valor);
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarItensCompra()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();

            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => solicitacao.RegistrarCompra(itens));

            //Então
            Assert.Equal("A solicitação de compra deve possuir itens!", ex.Message);
        }
    }
}
