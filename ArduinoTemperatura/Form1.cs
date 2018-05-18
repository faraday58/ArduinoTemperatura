using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
namespace ArduinoTemperatura
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            serialPort.BaudRate=9600;
            serialPort.PortName = "COM6";
            serialPort.Open();
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            serialPort.Write("a");
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            serialPort.Write("b");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort.Close();
        }
    }
}
