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

namespace HeresonFanClubBot
{
    public partial class Form1 : Form
    {
        private Skype skype;
        private const string nick = "[ImoutoBot] ";
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
            if (msg.Body.StartsWith("!"))
            {
                string command = msg.Body.ToLower();
                username = msg.FromHandle;
                Chat chat = msg.Chat;
                if (msg.Body.ToLower().StartsWith("!hello"))
                {
                    if (username == "chuck.a.maverick")
                    {
                        chat.SendMessage("Hello, Onii-chan!");
                    }
                    else
                    {
                        chat.SendMessage("Hello, " + msg.FromDisplayName + "!");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory);
        }
    }
}
