using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetGameList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //http://steamcommunity.com/id/yourusername/games/?xml=1
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";

            this.Cursor = Cursors.WaitCursor;


            string url = textBox1.Text + "/games/?xml=1";
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            var FinalText = wc.DownloadString(url);
            var UnreadText = FinalText;
            var StartText = "<name>";
            var EndText = "</name>";
            while (UnreadText.IndexOf(StartText) >= 0)
            {
                var NextGameIndex = UnreadText.IndexOf(StartText) + StartText.Length;
                UnreadText = UnreadText.Substring(NextGameIndex);

                var StartIndex = 0;
                var EndIndex = UnreadText.IndexOf(EndText);
                var strLength = EndIndex - StartIndex;

                if (StartIndex > EndIndex)
                {
                    //window.alert(UnreadText);
                    //window.alert(StartIndex + " - " + EndIndex + " - " + strLength);
                    MessageBox.Show("Failed to parse text");
                    break;
                }

                var GameName = UnreadText.Substring(StartIndex, strLength);
                GameName = GameName.Replace("<![CDATA[", "").Replace("]]>", "");
                if (textBox2.Text != "") textBox2.Text += Environment.NewLine;
                textBox2.Text += GameName;
            }

            wc.Dispose();
            this.Cursor = Cursors.Default;

        }
    }
}
