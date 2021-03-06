﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO.Ports;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace ArduinoTemperatura
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        FormTemperatura formTemperatura;
        FileStream fs;
        StreamWriter sw;
        StreamReader sr;
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
            toolStripPuertosComboBox.Items.AddRange(v);

            string[] velocidad = { "2400", "4800", "9600","19200"};

            toolStripVelocidadComboBox.Items.AddRange(velocidad);

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
                serialPort.BaudRate = int.Parse(toolStripVelocidadComboBox.SelectedItem.ToString());
                serialPort.PortName = toolStripPuertosComboBox.SelectedItem.ToString();


            }
            catch(IOException error)
            {
                MessageBox.Show(" Problema de conexión: "+ error );
            }
        }

        private void conectarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort.Open();
        }

        private void pruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prueba formprueba = new Prueba(serialPort);
            formprueba.Show();
        }

        private void temperaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formTemperatura = new FormTemperatura(serialPort,this);
            formTemperatura.Show();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fs = new FileStream(saveFile.FileName, FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine("Tiempo Temperatura ");
                    int N = formTemperatura.temperaturas.Count;
                    for (int i = 0; i < N; i++)
                    {
                        sw.WriteLine(" " + formTemperatura.tiempos[i] + " " + formTemperatura.temperaturas[i]);
                    }
                }
                catch(IOException error)
                {
                    MessageBox.Show("Error " + error.Message);
                }
                finally
                {
                    sw.Close();
                    fs.Close();

                }



            }


        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if(openFile.ShowDialog() == DialogResult.OK  )
            {
                fs = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                string datos=sr.ReadLine();
                int tiempo;
                int temperatura;
                while (datos != null)
                {
                    
                }


            }

        }

        private void exToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document docpdf = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if( saveFileDialog.ShowDialog() ==  DialogResult.OK)
            {
                PdfWriter pdfFile = PdfWriter.GetInstance(docpdf, new FileStream(saveFileDialog.FileName, FileMode.Append));
                docpdf.Open();
                MemoryStream imagenGraficar = new MemoryStream();
                formTemperatura.chTemperatura.SaveImage(imagenGraficar, ChartImageFormat.Png);
                iTextSharp.text.Image GrafImagenPDF = iTextSharp.text.Image.GetInstance(imagenGraficar.GetBuffer());
                docpdf.Add(GrafImagenPDF);
                docpdf.Close();

            }

        }
    }
}
