using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace ArduinoTemperatura
{
    public partial class FormTemperatura : Form
    {
        SerialPort mipuerto;
        int tiempo_temperatura;
        public FormTemperatura(SerialPort mipuerto,Form1 form1)
        {
            InitializeComponent();
            this.mipuerto = mipuerto;
            MdiParent = form1;
        }

        private void tiempo_adquisición_Tick(object sender, EventArgs e)
        {
            int temperatura = mipuerto.ReadByte();
            chTemperatura.Series[0].Points.AddXY(tiempo_temperatura, temperatura);


        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tiempo_adquisición.Start();
        }

        private void pararToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tiempo_adquisición.Stop();
            mipuerto.Close();
        }
    }
}
