using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKYPE4COMLib;
using System.Diagnostics;
using System.IO;
using CryptorEngine;

namespace HeresonFanClubBot
{
    public partial class Form1 : Form
    {
        private Skype skype;
        private const string nick = "[Shin's Imouto] ";
        public Form1()
        {
            InitializeComponent();
        }
        private void connect_Click(object sender, EventArgs e)
        {
            skype = new Skype();
            skype.Attach(7, false);
            
            skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
        }
        string username = "";
        private void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
            MessageBox.Show(status.ToString());
            if (msg.Body.StartsWith("!"))
            {
                string command = msg.Body.ToLower();
                username = msg.FromHandle;
                if (msg.Body.ToLower().StartsWith("!hello") && TChatMessageStatus.cmsReceived == status)
                {
                    if (username == "chuck.a.maverick")
                    {
                        msg.Chat.SendMessage("Hello, Onii-chan!");//Envoie dans le chat sélectionné
                    }
                    else
                    {
                        msg.Chat.SendMessage("Hello, " + msg.FromDisplayName + "!");//Envoie dans le chat sélectionné
                    }
                }
                else if (msg.Body.StartsWith("!encrypt ".ToLowerInvariant()) || msg.Body.StartsWith("!ENCRYPT ".ToUpperInvariant()))
                {
                    string msgtoencrypt = msg.Body.Replace("!ENCRYPT ", "").Replace("!encrypt", "");
                    string atbash = CryptorEngine.CryptorEngine.EncryptAB(msgtoencrypt);
                }
                else if (msg.Body.StartsWith("!decrypt ".ToLowerInvariant()) || msg.Body.StartsWith("!DECRYPT ".ToUpperInvariant()))
                {
                    string msgtoencrypt = msg.Body.Replace("!DECRYPT ", "").Replace("!decrypt", "");
                    string atbash = CryptorEngine.CryptorEngine.EncryptAB(msgtoencrypt);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory);
        }
    }
}
