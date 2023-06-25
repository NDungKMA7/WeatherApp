using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using ProjectKMAIOT.Dto;
using ProjectKMAIOT.Models;
using ProjectKMAIOT.Rules;

namespace ProjectKMAIOT.Hubs
{
    public class SignalRServer : Hub
    {
        private Daily dl = new Daily();
        private Hourly hl = new Hourly();
        public async Task SaveData(string data)
        {
            ReceiveData rcd = JsonConvert.DeserializeObject<ReceiveData>(data);
            await hl.ControlData(rcd);
            await  dl.ControlData(rcd);
            await Clients.All.SendAsync("refreshData");
        }
    }
}
