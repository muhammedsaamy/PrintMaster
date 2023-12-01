using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintMaster.DTOs;
using PrintMaster.Interfaces;

namespace PrintMaster.Controllers
{

    public class PrintFilesController : BaseApiController
    {
        private readonly ILogger<PrintFilesController> _logger;
        private readonly IPrintRepository _printingService;

        public PrintFilesController(ILogger<PrintFilesController> logger, IPrintRepository printingService)
        {
            _logger = logger;
            _printingService = printingService;
        }

        [HttpPost("PrintFiles")]
        public IActionResult PrintFiles([FromBody] PrintRequest request)
        {
            try
            {
                // Input validation
                if (string.IsNullOrEmpty(request.DirectoryPath) || string.IsNullOrEmpty(request.PrinterName))
                {
                    return BadRequest("Invalid input. DirectoryPath and PrinterName are required.");
                }
                else
                {
                    string returnedMessage = _printingService.PrintFilesInDirectory(request.DirectoryPath, request.PrinterName);

                    if (returnedMessage.Contains("printing process initiated"))
                    {
                    // Log success
                    _logger.LogInformation($"Printing process initiated for {request.DirectoryPath} to printer {request.PrinterName}.");

                    // Return success response
                    return Ok(returnedMessage);
                    }
                    else { return BadRequest(returnedMessage); }

                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while printing files .. " + ex.Message);
                return StatusCode(500, "Internal Server Error : " + ex.Message);
            }
        }
    }
}
