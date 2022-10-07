using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.Produto.Command.AtualizarPreco;
using SistemaCompra.Application.Produto.Command.RegistrarProduto;
using SistemaCompra.Application.Produto.Query.ObterProduto;
using System;
using System.Threading.Tasks;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;

namespace SistemaCompra.API.Produto
{
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost, Route("compra/cadastro")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CadastrarCompra([FromBody] RegistrarCompraCommand registrarCompraCommand)
        {
            
            if (registrarCompraCommand == null)
            {
                return BadRequest();
            }
            else
            {
                _mediator.Send(registrarCompraCommand);
                return StatusCode(201);
            }
        }

       
    }
}
