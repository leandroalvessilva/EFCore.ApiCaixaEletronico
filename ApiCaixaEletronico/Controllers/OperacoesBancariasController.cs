using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiCaixaEletronico.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class OperacoesBancariasController : Controller
    {
        private readonly IOperacoesBancariasService _operacoesService;

        public OperacoesBancariasController(IOperacoesBancariasService operacoesService)
        {
            this._operacoesService = operacoesService;
        }

        [HttpGet]
        [Route("Saldo")]
        public ActionResult Saldo(int banco, int agencia, int numeroConta, long cpf)
        {
            try
            {
                var saldo = _operacoesService.Saldo(banco, agencia, numeroConta, cpf);

                return Ok(saldo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Data = null,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Sacar")]
        public ActionResult Sacar([FromBody]ContaDTO conta, decimal valorSacar)
            {
            try
            {
                var result = _operacoesService.Sacar(conta, valorSacar);

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
        public ActionResult Depositar([FromBody]ContaDTO conta, decimal valorDepositar, string notasDepositadas)
        {
            try
            {
                var result = _operacoesService.Depositar(conta, valorDepositar, notasDepositadas);

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
