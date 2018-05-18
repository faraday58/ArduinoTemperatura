using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoTemperatura
{
    public partial class Prueba : Form
    {
        private SerialPort miserialPort;
        public Prueba( SerialPort serialPort )
        {
            InitializeComponent();
            this.miserialPort = serialPort;
        }

        private void btn_encender_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Yellow;
            miserialPort.Write("a");
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            miserialPort.Write("b");
        }
    }
}
