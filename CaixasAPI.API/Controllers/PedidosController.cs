using CaixasAPI.Domain.Dtos;
using CaixasAPI.Domain.Models;
using CaixasAPI.Domain.Services;
using CaixasAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaixasAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEmpacotamentoService _empacotamentoService;
        private readonly IMapper _mapper;

        public PedidosController(IPedidoRepository pedidoRepository, IEmpacotamentoService empacotamentoService, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _empacotamentoService = empacotamentoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoSaidaDto>>> GetAllPedidos()
        {
            var pedidos = await _pedidoRepository.ObterTodosPedidosAsync();
            var pedidosDto = _mapper.Map<List<PedidoSaidaDto>>(pedidos);
            return Ok(pedidosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoSaidaDto>> GetPedidoById(int id)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorIdAsync(id);

            if (pedido == null)
            {
                return NotFound(new { message = $"Pedido com ID {id} não encontrado" });
            }

            var pedidoDto = _mapper.Map<PedidoSaidaDto>(pedido);

            return Ok(pedidoDto);
        }

        [HttpPost("processar")]
        public ActionResult<ProcessamentoPedidosSaidaDto> ProcessarPedidos(ProcessamentoPedidosEntradaDto request)
        {
            var pedidos = _mapper.Map<List<Pedido>>(request.Pedidos);

            var pedidosProcessados = _empacotamentoService.ProcessarPedidos(pedidos);

            var resultado = _empacotamentoService.MapearParaSaida(pedidosProcessados);

            return Ok(resultado);
        }

        [HttpPost("salvar")]
        public async Task<ActionResult<PedidoSaidaDto>> SalvarPedido(PedidoEntradaDto request)
        {
            var pedido = _mapper.Map<Pedido>(request);

            var pedidosProcessados = _empacotamentoService.ProcessarPedidos(new List<Pedido> { pedido });
            var pedidoProcessado = pedidosProcessados.First();

            var pedidoSalvo = await _pedidoRepository.SalvarPedidoAsync(pedidoProcessado);

            var resultado = _empacotamentoService.MapearParaSaida(new List<Pedido> { pedidoSalvo });
            return Ok(resultado.Pedidos[0]);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var resultado = await _pedidoRepository.DeletarPedidoAsync(id);

            if (!resultado)
            {
                return NotFound(new { message = $"Pedido com ID {id} não encontrado" });
            }

            return Ok(new { message = $"Pedido com ID {id} removido com sucesso" });
        }
    }
}
