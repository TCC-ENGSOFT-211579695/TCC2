using DataCompression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace DataCompressor
{
    internal class DataCompressor : ServiceBase
    {
        System.Timers.Timer triggerTimer;
        DataAccess dataAccess;

        public DataCompressor() 
        {
            dataAccess = new DataAccess();
            triggerTimer = new System.Timers.Timer(60000);
            triggerTimer.Elapsed += TriggerTimer_Elapsed;            
        }

        protected override void OnStart( string[] args )
        {
            triggerTimer.Start();
        }

        protected override void OnStop()
        {
            triggerTimer.Stop();
            dataAccess.CloseDatabase();
        }

        private void TriggerTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckCoilProduction();
        }

        private void CheckCoilProduction()
        {
            string lastCoil = dataAccess.GetLastCoilProduced();
            if (!string.IsNullOrEmpty(lastCoil))
            {
                SaveToDb(lastCoil, GenerateBlob(lastCoil));
            }
        }

        private byte[] GenerateBlob(string coilCode)
        {
            DataSet dsCoil = dataAccess.GetCoilData(coilCode);            

            return Blob.ConvertToByteArray(dsCoil);
        }

        private void SaveToDb(string coilCode, byte[] blob)
        {
            dataAccess.InsertBlob(coilCode, blob);
        }
    }
}
