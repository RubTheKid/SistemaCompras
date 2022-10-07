using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public List<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }    


        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            Itens = new List<Item>();

        }

        public void AdicionarItem(Produto produto, int qtd)
        {
            Itens.Add(new Item(produto, qtd));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
           Itens.AddRange(itens);

           TotalGeral = new Money(Itens.Sum(a => a.Subtotal.Value));

           if (IsValid())
           {
                DefinirCondicaoPagamento();
           } 
           else
           {
                throw new BusinessRuleException("Não há itens na solicitação de compra");
           }
        }

        private void DefinirCondicaoPagamento()
        {
            if (TotalGeral.Value > 50000)
            {
                CondicaoPagamento = new CondicaoPagamento(30);
            } 
        }
        private bool IsValid()
        {
            if (Itens.Count() == 0)
            {
                return false;
            }
            return true;
        }
    }
}
