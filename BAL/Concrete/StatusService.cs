using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DAL.DAO;
using System.IO;

namespace BAL
{
    public class StatusService : Abstract.IStatusService
    {
        private readonly string fileName;
        public StatusService()
        {
            fileName = ConfigurationManager.AppSettings["FileName"];
        }
        public async Task SaveStatus(DAL.DAO.Status status)
        {
            string json = JsonConvert.SerializeObject(status);

            //write string to file
             await System.IO.File.WriteAllTextAsync(fileName, json);
            
        }

        public DAL.DAO.Status GetStatus()
        {
            Status status = JsonConvert.DeserializeObject<Status>(File.ReadAllText(fileName));
            return status;
        }
    }
}
