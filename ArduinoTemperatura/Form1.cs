using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
namespace ArduinoTemperatura
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();

            selecciona();
        }

        private void selecciona()
        {
            string[] v = SerialPort.GetPortNames();
            Array.Sort(v);
            toolStripPuertosComboBox.Items.Add(v);

            string[] velocidad = { "2400", "4800", "9600","19200"};

            toolStripVelocidadComboBox.Items.Add(velocidad);

        }



/*
        private void btnEncender_Click(object sender, EventArgs e)
        {
            serialPort.Write("a");
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            serialPort.Write("b");
        }

    */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort.Close();
        }

        private void verificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.BaudRate = int.Parse(toolStripVelocidadComboBox.SelectedText);
                serialPort.PortName = toolStripPuertosComboBox.SelectedText;


            }
            catch(IOException error)
            {
                MessageBox.Show(" Problema de conexión: "+ error );
            }
        }
    }
}
