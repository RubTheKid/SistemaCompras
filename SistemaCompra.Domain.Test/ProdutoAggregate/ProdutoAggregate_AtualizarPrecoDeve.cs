using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SistemaCompra.Domain.Test.ProdutoAggregate
{
    public class ProdutoAggregate_AtualizarPrecoDeve
    {
        [Fact]
        public void AtualizarPrecoDe400Para500()
        {
            //Dado
            var produto = new Produto("produto001", "desc", 400, Categoria.Madeira.ToString());

            //Quando
            produto.AtualizarPreco(500);

            //Então
            Assert.Equal(500, produto.Preco.Value);
        }

        [Fact]
        public void NotificarErroQuandoProdutoEstaCancelado()
        {
            //Dado
            var produto = new Produto("produto001", "desc", 400, Categoria.Madeira.ToString());
            produto.Suspender();

            //Quando
            var ex = Assert.Throws<BusinessRuleException>(() => produto.AtualizarPreco(500));

            //Então
            Assert.Equal("Produto deve estar ativo!", ex.Message);
        }
    }
}
