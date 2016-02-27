using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    public partial class FormGlob : Form
    {
        delegate bool EvaluateClient(Client t);

        private void ClientToFormConst1()
        {
            m_dataTypes.Add(Modes.Clients, typeof(Client));
            m_recordTypes.Add(Modes.Clients, new ClientType(this));
        }
        private void ClientToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Clients.ToString() );
        }

        List<Client> AllClients()
        {
            return FindClients(t => true);
        }

        List<Client> FindClients(EvaluateClient comp)
        {
            List<Client> clients = new List<Client>();
            foreach (var tt in this.clientList)
            {
                Client t = tt as Client;
                if (comp(t))
                    clients.Add(t);
            }
            return clients;
        }
    }
}