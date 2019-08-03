using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Models;
using FileStorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Controllers
{
    public class FilesController : Controller
    {
        private readonly FileService _fileServ;
        public FilesController(FileService fileServ)
        {
            _fileServ = fileServ;
        }

        [AllowAnonymous]
        [HttpPost("api/files")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveFiles([FromBody]IEnumerable<FileInfoDto> newDtoFiles)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _fileServ.SaveAsync(newDtoFiles);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("api/files")]
        [Produces(typeof(List<FileInfoDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _fileServ.GetAllFilesAsync());
        }
    }
}
