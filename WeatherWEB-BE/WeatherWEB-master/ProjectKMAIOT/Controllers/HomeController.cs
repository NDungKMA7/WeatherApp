using Microsoft.AspNetCore.Mvc;
using ProjectKMAIOT.Models;
using System.Text;
using System;
using Newtonsoft.Json;
using ProjectKMAIOT.Dto;
using Microsoft.AspNetCore.SignalR;
using ProjectKMAIOT.Hubs;
using ProjectKMAIOT.Rules;
using OpenAI_API;
using OpenAI_API.Completions;
using Microsoft.Identity.Client;

namespace ProjectKMAIOT.Controllers
{
    public class HomeController : Controller
    {
        private IHubContext<SignalRServer> _signalRHub;
        private Daily dl = new Daily();
        private Hourly hl = new Hourly();
        private OpenAI AI = new OpenAI();   
        public HomeController(IHubContext<SignalRServer> signalRHub)
        {
            _signalRHub = signalRHub;
        }
        public  IActionResult Index()
        {
           
        _signalRHub.Clients.All.SendAsync("refreshData");
            return View();
        }
        [HttpPost]
        public IActionResult GetResultOpenAI([FromBody] JsonFormat js)
        {
            string apiKey = "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";
            string answer = string.Empty;
            var openai = new OpenAIAPI(apiKey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = js.body;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 1024;
            completion.Temperature = 0.5f;
            var result = openai.Completions.CreateCompletionsAsync(completion); 
            if(result!= null)
            {
                foreach(var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
                return Ok(answer);

            }
            else
            {
                return BadRequest("not found");
            }


        }
        [HttpPost]
        public IActionResult SaveData([FromBody]  JsonFormat js)
        {
            if (js == null)
            {
                return BadRequest("Missing parameter value");
            }

            if (String.IsNullOrEmpty(js.body))
            {
                return Content("Data of parameter null");
            }
            ReceiveData rcd = JsonConvert.DeserializeObject<ReceiveData>(js.body);
            hl.ControlData(rcd);
            dl.ControlData(rcd);
            _signalRHub.Clients.All.SendAsync("refreshData");
            return Ok();  
        }



        public IActionResult GetOnlyDay()
        {
            DailyDataRecord Onlywd = dl.GetOnlyDayData();
            return Ok(Onlywd);
        }
        public IActionResult GetDaily()
        {
            List<WeekData> lwd = dl.GetWeeksData();
            return Ok(lwd);
        }
        public IActionResult GetHourly()
        {
            List<HourlyDataRecord> lhd = hl.GetHoursData();
            return Ok(lhd);
        }
        



    }
}
