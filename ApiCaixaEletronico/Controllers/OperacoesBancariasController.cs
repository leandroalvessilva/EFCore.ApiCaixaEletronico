using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiCaixaEletronico.Controllers
{
    [Route("api/[controller]")]
    public class OperacoesBancariasController : Controller
    {
        private readonly IOperacoesBancariasService _operacoesService;

        public OperacoesBancariasController(IOperacoesBancariasService operacoesService)
        {
            this._operacoesService = operacoesService;
        }

        [HttpGet]
        [Route("Saldo")]
        public ActionResult Saldo(ContaDTO conta)
        {
            try
            {
                var saldo = _operacoesService.Saldo(conta);

                return Ok(saldo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Mensagem = ex.Message
                });
            }
        }
              
        [HttpPost]
        [Route("Sacar")]
        public ActionResult Sacar(ContaDTO conta, bool isTransferencia, decimal ValorSacar)
            {
            try
            {
                var result = _operacoesService.Sacar(conta, isTransferencia, ValorSacar);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Depositar")]
        public ActionResult Depositar(ContaDTO conta, bool outraConta, decimal ValorDepositar)
        {
            try
            {
                var result = _operacoesService.Depositar(conta, outraConta, ValorDepositar);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Transferir")]
        public ActionResult Transferir(ContaDTO conta, ContaDTO contaDestino, decimal ValorTransferir)
        {
            try
            {
                var result = _operacoesService.Transferir(conta, contaDestino, ValorTransferir);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Mensagem = ex.Message
                });
            }
        }
    }
}
