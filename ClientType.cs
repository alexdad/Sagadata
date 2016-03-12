using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class ClientType : RecordType
    {
        public ClientType(FormGlob glob) : base(glob)
        {
        }

        public override Modes Mode { get { return Modes.Clients;} }  

        public override bool ReadFile()
        {
            return ReadRecordsFile<Client>();
        }
        public override bool DownloadFile()
        {
            return Download<Client>();
        }
        public override bool UploadFile()
        {
            return Upload<Client>();
        }

        public override void ShowCount()
        {
        }

        public override void DoSelection()
        {
                return;
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortClients(hdr, temp as Client[]);
        }
        public void SortClients(string hdr, Client[] temp)
        {
            switch (hdr)
            {
                case "Code":
                    Array.Sort(temp, new Client.ComparerByCode());
                    break;
                case "MachineName":
                    Array.Sort(temp, new Client.ComparerByMachineName());
                    break;
                case "LastTouch":
                    Array.Sort(temp, new Client.ComparerByLastTouch());
                    break;
                default:
                    Record.NeedToReverse = false;
                    break;
            }
            if (Record.NeedToReverse)
                Array.Reverse(temp);
        }

    }
}