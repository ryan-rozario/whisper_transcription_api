using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;


namespace AudioTranscriptionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioTranscriptionController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Transcribe(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
                return BadRequest("No audio file selected for upload.");

            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await audioFile.CopyToAsync(stream);
            }

            try
            {
                var transcription = await TranscribeAudio(filePath);
                return Ok(transcription);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during transcription: {ex.Message}");
            }
            finally
            {
                // Clean up the temporary file
                System.IO.File.Delete(filePath);
            }
        }

        private static async Task<string> TranscribeAudio(string audioFilePath)
        {
            return "";
        }
    }
}
