using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiCaixaEletronico.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class CaixaEletronicoController : Controller
    {
        private readonly ICaixaEletronicoService _caixaEletronicoService;

        public CaixaEletronicoController(ICaixaEletronicoService caixaEletronicoService)
        {
            this._caixaEletronicoService = caixaEletronicoService;
        }

        [HttpGet]
        [Route("Login")]
        public ActionResult Login(long cpf, int senha)
        {
            try
            {
                var result = _caixaEletronicoService.Login(cpf, senha);

                return Ok(result);
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

        [HttpGet]
        [Route("ListarUsuario")]
        public ActionResult ListarUsuario(long cpf)
        {
            try
            {
                var result = _caixaEletronicoService.ListarUsuario(cpf);

                return Ok(result);
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
    }
}
