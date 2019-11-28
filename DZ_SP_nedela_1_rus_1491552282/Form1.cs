using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DZ_SP_nedela_1_rus_1491552282
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            listViewProcess.View = View.Details;
            listViewProcess.Columns.Add("Name", "Имя", 150);
            listViewProcess.Columns.Add("Id", "ИД процесса", 100);
            listViewProcess.Columns.Add("Streams", "Потоки", 100);
            listViewProcess.Columns.Add("Descriptor", "Дескриптор", 100);
            listViewProcess.Width = listViewProcess.Columns[0].Width + listViewProcess.Columns[1].Width+ 
            listViewProcess.Columns[2].Width+listViewProcess.Columns[3].Width;
            this.Width = listViewProcess.Width + 40;
            listViewProcess.GridLines = true;
            listViewProcess.FullRowSelect = true;
            listViewProcess.Visible = false;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            btnGetProcess_Click(sender,e);
        }

        private void btnGetProcess_Click(object sender, EventArgs e)
        {
            listViewProcess.Visible = true;
            timer.Stop();
            Task newtask = new Task(new Action(GetProcessecs));
            newtask.Start();
        }

        public void GetProcessecs()
        {
            Process[] proc = Process.GetProcesses();
            Action a = () =>
            {
                foreach (Process item in proc)
                {
                    var list = listViewProcess.Items.Add(item.ProcessName.ToString());
                    list.SubItems.Add(item.Id.ToString());
                    list.SubItems.Add(item.Threads.Count.ToString());
                    list.SubItems.Add(item.HandleCount.ToString());
                }
                timer.Start();
            };
            Invoke(a);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
