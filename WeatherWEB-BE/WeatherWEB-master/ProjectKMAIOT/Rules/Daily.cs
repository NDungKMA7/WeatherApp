using ProjectKMAIOT.Dto;
using ProjectKMAIOT.Models;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;

namespace ProjectKMAIOT.Rules
{
    public class Daily
    {
        private MyDbConnect db = new MyDbConnect();
        public string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

        public List<WeekData> GetWeeksData()
        {
             List<WeekData> ls =  db.DailyRecord.OrderByDescending(x => x.ID).Take(7).Select(x => new WeekData { Day = x.Day, TemperatureMax = x.TemperatureMax, RainSensor = x.RainSensor,ID=x.ID}).ToList();    
            return ls;  
        }
        public DailyDataRecord GetOnlyDayData()
        {
            DailyDataRecord ls = db.DailyRecord.OrderByDescending(x => x.ID).FirstOrDefault();
            return ls;
        }

        /*public List<DailyDataRecord> GetWeeksData()
        {
            List<DailyDataRecord> ls = db.DailyRecord.OrderByDescending(x => x.ID).Take(7).ToList();
            return ls;
        }
        public DailyDataRecord GetOnlyDayData()
        {
            DailyDataRecord ls = db.DailyRecord.OrderByDescending(x => x.ID).FirstOrDefault();
            return ls;
        }*/

        public void ControlData(ReceiveData rd)
        {
            DailyDataRecord RecordLast = db.DailyRecord.OrderByDescending(x => x.ID).FirstOrDefault();
            if (RecordLast == null || RecordLast.Day.Equals(currentDate) == false)
            {
                CreateRecord(rd);
            }
            else
            {
                UpdateRecord(rd, RecordLast);
            }

        }

        public void CreateRecord(ReceiveData rd)
        {
            DailyDataRecord record = new DailyDataRecord();
            record.TemperatureMin = rd.temperature;
            record.TemperatureMax = rd.temperature; 
            record.HumidityMin = rd.humidity;   
            record.HumidityMax = rd.humidity;
            record.Day = currentDate;
            record.AirSensor = rd.air;
            record.RainSensor = rd.rain;
            db.DailyRecord.Add(record);
            db.SaveChanges();   
        }

        public void UpdateRecord(ReceiveData rd, DailyDataRecord daily)
        {
            if(daily.TemperatureMin > rd.temperature) { 
                daily.TemperatureMin = rd.temperature;  
            }
            if (daily.TemperatureMax < rd.temperature)
            {
                daily.TemperatureMax = rd.temperature;
            }
            if (daily.HumidityMax < rd.humidity)
            {
                daily.HumidityMax = rd.humidity;
            }
            if (daily.HumidityMin > rd.humidity)
            {
                daily.HumidityMin = rd.humidity;
            }
            daily.AirSensor = rd.air;
            daily.RainSensor = rd.rain;
            db.SaveChanges();
        }

    }
}
