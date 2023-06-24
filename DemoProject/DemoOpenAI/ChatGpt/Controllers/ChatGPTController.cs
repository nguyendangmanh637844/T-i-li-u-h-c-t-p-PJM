using ChatGpt.Extension;
using ChatGpt.Models;
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using OpenAI.Audio;
using OpenAI.Chat;
using OpenAI.Images;
using OpenAI.Models;

namespace ChatGpt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public ChatGPTController(IConfiguration config, HttpClient client)
        {
            _config = config;
            _client = client;
        }

        /// <summary>
        /// Trả về kết quả câu hỏi
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAsync(string question)
        {
            if (String.IsNullOrEmpty(question))
            {
                return BadRequest();
            }
            var apiKey = _config.GetValue(ConstString.API_KEY, String.Empty);
            if (String.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Sai api-key rồi bạn eii");
            }

            OpenAIClient api;
            try
            {
                api = new OpenAIClient(apiKey);

                var chatPrompts = new List<ChatPrompt>
                {
                    new ChatPrompt("assistant", question),
                };

                var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);
                var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
                return Ok(result.FirstChoice.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Trả về link ảnh được tạo ra
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Generate-image")]
        public async Task<IActionResult> GenerateImageAsync(GenerateImageRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            if (request.NumberOfResult < 1)
            {
                return BadRequest("Số ảnh trả về phải lớn hơn 0");
            }
            if ((int)request.ImageSize < 0 || (int)request.ImageSize > 2)
            {
                return BadRequest("Kích cỡ ảnh không hợp lệ: 0-Nhỏ, 1-Trung bình, 2-Lớn");
            }
            var apiKey = _config.GetValue(ConstString.API_KEY, String.Empty);

            if (String.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Sai api-key rồi bạn eii");
            }
            OpenAIClient api;
            try
            {
                api = new OpenAIClient(apiKey);

                var results = await api.ImagesEndPoint.GenerateImageAsync(request.Description, request.NumberOfResult, request.ImageSize);
                if (results.Count > 0)
                {
                    await DownloadAndSaveImagesAsync(results.ToList());
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// chuyển đổi file âm thanh thành văn bản
        /// </summary>
        /// <param name="audioFile"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Translate-audio")]
        public async Task<IActionResult> TranslateAudioAsync(IFormFile audioFile, string language)
        {
            if (string.IsNullOrEmpty(language))
            {
                return BadRequest("Ngôn ngữ không được trống");
            }
            if (audioFile == null || audioFile.Length == 0)
            {
                return BadRequest("Invalid audio file.");
            }
            var apiKey = _config.GetValue(ConstString.API_KEY, String.Empty);
            if (String.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Sai api-key rồi bạn eii");
            }

            var uploadsFolder = Path.Combine(
                Path.Combine(_config.GetValue(ConstString.ROOT_FOLDER, ConstString.DEFAULT_ROOT_FOLDER), _config.GetValue(ConstString.UPLOAD_FOLDER, ConstString.DEFAULT_UPLOAD_FOLDER)),
                _config.GetValue(ConstString.AUDIO_FOLDER, ConstString.DEFAULT_AUDIO_FOLDER));
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, $"{Guid.NewGuid()}_{audioFile.FileName}");
            if (_config.GetValue(ConstString.IS_SAVE_FILE_UPLOAD, true))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(fileStream);
                }
            }

            OpenAIClient api;
            try
            {
                api = new OpenAIClient(apiKey);

                var request = new AudioTranscriptionRequest(filePath, language: language);
                var result = await api.AudioEndpoint.CreateTranscriptionAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// chuyển đổi âm thanh thành văn bản tiếng anh
        /// </summary>
        /// <param name="audioFile"></param>
        /// <param name="langudge"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Translate-audio-to-english")]
        public async Task<IActionResult> TranslateAudioToEnglistAsync(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                return BadRequest("Invalid audio file.");
            }
            var apiKey = _config.GetValue(ConstString.API_KEY, String.Empty);
            if (String.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Sai api-key rồi bạn eii");
            }

            var uploadsFolder = Path.Combine(
                Path.Combine(_config.GetValue(ConstString.ROOT_FOLDER, ConstString.DEFAULT_ROOT_FOLDER), _config.GetValue(ConstString.UPLOAD_FOLDER, ConstString.DEFAULT_UPLOAD_FOLDER)),
                _config.GetValue(ConstString.AUDIO_FOLDER, ConstString.DEFAULT_AUDIO_FOLDER));
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, $"{Guid.NewGuid()}_{audioFile.FileName}");
            if (_config.GetValue(ConstString.IS_SAVE_FILE_UPLOAD, true))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(fileStream);
                }
            }

            OpenAIClient api;
            try
            {
                api = new OpenAIClient(apiKey);

                var request = new AudioTranslationRequest(filePath);
                var result = await api.AudioEndpoint.CreateTranslationAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Tạo các biến thể khác của ảnh
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="langudge"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-another-image")]
        public async Task<IActionResult> CreateAnotherImageAsync(IFormFile imageFile, int numberOfResult, ImageSize imageSize)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Invalid image file.");
            }
            if (numberOfResult == 0)
            {
                return BadRequest("Số ảnh trả về phải lớn hơn 0");
            }
            if ((int)imageSize < 0 || (int)imageSize > 2)
            {
                return BadRequest("Kích cỡ ảnh không hợp lệ: 0-Nhỏ, 1-Trung bình, 2-Lớn");
            }
            var apiKey = _config.GetValue(ConstString.API_KEY, String.Empty);
            if (String.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Sai api-key rồi bạn eii");
            }

            var uploadsFolder = Path.Combine(
                Path.Combine(_config.GetValue(ConstString.ROOT_FOLDER, ConstString.DEFAULT_ROOT_FOLDER), _config.GetValue(ConstString.UPLOAD_FOLDER, ConstString.DEFAULT_UPLOAD_FOLDER)),
                _config.GetValue(ConstString.IMAGES_FOLDER, ConstString.DEFAULT_IMAGES_FOLDER));
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, $"{Guid.NewGuid()}_{imageFile.FileName}");
            if (_config.GetValue(ConstString.IS_SAVE_FILE_UPLOAD, true))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }

            OpenAIClient api;
            try
            {
                api = new OpenAIClient(apiKey);

                var results = await api.ImagesEndPoint.CreateImageVariationAsync(filePath, numberOfResult, imageSize);
                if (results.Count > 0)
                {
                    await DownloadAndSaveImagesAsync(results.ToList());
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task DownloadAndSaveImagesAsync(List<string> imageUrls)
        {
            foreach (string url in imageUrls)
            {
                var downloadFolder = Path.Combine(_config.GetValue(ConstString.ROOT_FOLDER, ConstString.DEFAULT_ROOT_FOLDER), _config.GetValue(ConstString.DOWNLOAD_FOLDER, ConstString.DEFAULT_DOWNLOAD_FOLDER));
                if (!Directory.Exists(downloadFolder))
                {
                    Directory.CreateDirectory(downloadFolder);
                }
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        response.EnsureSuccessStatusCode();

                        string filename = Path.Combine(downloadFolder, $"{Path.GetFileName(url)}{ConstString.PNG}");

                        using (Stream stream = System.IO.File.Create(filename))
                        {
                            await response.Content.CopyToAsync(stream);
                        }
                    }
                }
            }
        }
    }
}