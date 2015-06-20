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

namespace HeresonFanClubBot
{
    public partial class Form1 : Form
    {
        private Skype skype;
        private const string trigger = "!"; // Say !help
        private const string nick = "[The Yandere Whale-son Senpai Fan Club Class of 2k15 Stimulator] ";
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
            if (msg.Body.IndexOf(trigger) == 0)
            {
                string command = msg.Body.Remove(0, trigger.Length).ToLower();
                username = msg.FromHandle;
                Chat chat = skype.ActiveChats[0];
                chat.SendMessage(nick + ProcessCommand(command));
            }
        }

        private string ProcessCommand(string str)
        {
            string result;
            switch (str)
            {
                case "hello":
                    if (username == "chuck.a.maverick")
                    {
                        result = "Hello! " + "Onii-chan!";
                    }
                    else
                    {
                        result = "Hello!";
                    }
                    break;
                default:
                    result = "Sorry, command doesn't exist.";
                    break;
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
