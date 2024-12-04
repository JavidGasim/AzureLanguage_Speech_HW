using System.Diagnostics;
using AzureLanguage_Speech_HW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;

namespace AzureLanguage_Speech_HW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> TranslateSpeech()
        {
            var subKey = _configuration["AzureSpeech:SubscriptionKey"];
            var region = _configuration["AzureSpeech:Region"];

            var con = SpeechConfig.FromSubscription(subKey, region);

            using var recognizer = new SpeechRecognizer(con);
            var result = await recognizer.RecognizeOnceAsync();

            var speechResult = new SpeechResultViewModel
            {
                RecognizedText = result.Text,
            };
            Console.WriteLine(speechResult.RecognizedText);
            return Json(speechResult);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
