using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Searcher
{
    public partial class Form1 : Form
    {
        private string source;
        private string dest;

        public Form1()
        {
            InitializeComponent();
            labelProgress.Visible = false;
            this.Text = @"Log Searcher";
            GetSetting();
            source = tbSource.Text;
            dest = tbDest.Text;
            GetLogsFile();
        }



        private void GetSetting()
        {
            using (StreamReader r = new StreamReader("setting.txt"))
            {
                string oneLine = r.ReadToEnd();
                var lines = oneLine.RemoveEmpty().Split(',');

                var dict = lines.Select(line => line.Split('=')).ToDictionary(split => split[0], split => split[1]);

                tbSource.Text = dict["sourcePath"];
                tbDest.Text = dict["destinationPath"];
                tbLogExt.Text = dict["logExtension"];
                tbReportName.Text = dict["reportNameExtension"];
                tbSearchText.Text = dict["searchText"];
            }
        }

        public void GetLogsFile()
        {
            var files = GetLogs();

            foreach (var f in files)
            {
                listBox1.Items.Add(f);
            }
        }

        private FileInfo[] GetLogs()
        {
            var dic = new DirectoryInfo(source);
            var files = dic.GetFiles($"*.{tbLogExt.Text}");
            return files;
        }

        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var files = GetLogs();
            for (var i = 0; i < files.Length; i++)
            {
                var fileName = files[i];
                var lines = File.ReadAllLines(fileName.FullName);
                foreach (var line in lines)
                {
                    if (line.Contains(tbSearchText.Text))
                    {
                        var str = line + Environment.NewLine;
                        File.AppendAllText($"{dest}\\{tbReportName.Text}.txt", str);
                    }
                }
            }
            MessageBox.Show(@"Done");

        }
    }
}
