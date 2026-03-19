using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.Services.Interfaces;

namespace NF.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemServicoController : ControllerBase
    {
        private readonly IOrdemServicoService _service;
        private readonly IOrdemServicoPecaService _OsPecaService;

        public OrdemServicoController(IOrdemServicoService service, IOrdemServicoPecaService OsPecaService )
        {
            _service = service;
            _OsPecaService = OsPecaService;
        }



    }
}
