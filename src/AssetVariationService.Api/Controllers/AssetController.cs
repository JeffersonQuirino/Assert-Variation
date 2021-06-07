using AssetVariation.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AssetVariation.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IProcessService processService;
      
        public AssetController(IProcessService processService)
        {
            this.processService = processService;          
        }

        [HttpPost]
        [Route("processAsset")]
        public async Task<IActionResult> ProcessAsset()
        {
            try
            {
                return Ok(await processService.ProcessData());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("variation")]
        public async Task<IActionResult> Variation()
        {
            try
            {
                return Ok(await processService.GetAssetVariation());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}