using ProjectKMAIOT.Dto;
using ProjectKMAIOT.Models;

namespace ProjectKMAIOT.Rules
{
    public class Hourly
    {
        private MyDbConnect db = new MyDbConnect();
        public string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string currentHour = DateTime.Now.Hour.ToString();

        public List<HourlyDataRecord> GetHoursData()
        {
            List<HourlyDataRecord> hl = db.HourRecord.ToList();
            return hl;
        }
       
        public void ControlData(ReceiveData rd)
        {
            HourlyDataRecord RecordLast = db.HourRecord.OrderByDescending(x => x.ID).FirstOrDefault();
            if (RecordLast == null || RecordLast.Day.Equals(currentDate) == false)
            {
                RemoveAll();
                CreateRecord(rd);
            }
            else
            {
                if (RecordLast.Hour.Equals(currentHour))
                {
                    UpdateRecord(rd, RecordLast);
                }
                else
                {
                    CreateRecord(rd);
                }
            }

        }

        public void RemoveAll()
        {
            List<HourlyDataRecord> hl = db.HourRecord.ToList();
            db.HourRecord.RemoveRange(hl);
            db.SaveChanges();   
        }

        public void CreateRecord(ReceiveData rd)
        {
            HourlyDataRecord record = new HourlyDataRecord();
            record.Temperature = rd.temperature;
            record.Humidity = rd.humidity;
            record.Day = currentDate;
            record.Hour = currentHour;
            record.Air = rd.air;
            record.Rain = rd.rain;
            db.HourRecord.Add(record);
            db.SaveChanges();
        }

        public void UpdateRecord(ReceiveData rd, HourlyDataRecord hourly)
        {

            hourly.Humidity = rd.humidity;
            hourly.Temperature = rd.temperature;
            hourly.Air = rd.air;
            hourly.Rain = rd.rain;
            db.SaveChanges();
        }

    }
}
