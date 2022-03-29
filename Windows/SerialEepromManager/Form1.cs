using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialEepromManager
{
    public partial class Form1 : Form
    {

        #region Variabili (accesso privato)

        private SerialPort _serialPort;

        #endregion

        #region Costruttore

        public Form1()
        {
            InitializeComponent();

            // Inizializza porta seriale
            _serialPort = new SerialPort();
            _serialPort.BaudRate = 115200;
            _serialPort.DataBits = 8;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.One;
            _serialPort.PortName = "COM10";
            _serialPort.DtrEnable = false;

            // Enumerazione delle porte seriali disponibili
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cBSerialPorts.Items.Add(port);
            }
            if (cBSerialPorts.Items.Count > 0)
            {
                cBSerialPorts.SelectedIndex = 0;
                bConnetti.Enabled = true;
            }
        }

        #endregion

        #region Handler Eventi Form

        private void cBSerialPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            _serialPort.PortName = cBSerialPorts.SelectedItem.ToString();
            bConnetti.Enabled = true;
        }

        private void bConnetti_Click(object sender, EventArgs e)
        {
            if (Connetti())
            {
                string version = LeggiVersione();
                if (version != "")
                {
                    tBInfo.Text = "";
                    tBInfo.AppendText("Firmware version " + version + "\n");
                    tBInfo.AppendText("-------------------------------------------------\n");
                    tBInfo.AppendText("\n");

                    bConnetti.Enabled = false;
                    bDisconnetti.Enabled = true;
                    bReadAll.Enabled = true;
                    bWriteAll.Enabled = true;

                    toolStripStatusLabel1.Text = "Dispositivo connesso";
                }
                else
                {
                    Disconnetti();
                }
            }
        }

        private void bDisconnetti_Click(object sender, EventArgs e)
        {
            if (Disconnetti())
            {
                bWriteAll.Enabled = false;
                bReadAll.Enabled = false;
                bDisconnetti.Enabled = false;
                bConnetti.Enabled = true;

                toolStripStatusLabel1.Text = "Dispositivo non connesso";
            }
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            List<byte> datas = new List<byte>();
            bool b = ReadAll(datas);

            string s = "";
            int index = 0;
            for (int i = 0; i < datas.Count; i++)
            {
                byte data = datas[i];
                if (index < 15)
                {
                    s += data.ToString("X2") + " ";
                    index++;
                }
                else
                {
                    s += data.ToString("X2") + "\r\n";
                    tBInfo.AppendText(s);
                    s = "";
                    index = 0;
                }
            }

            saveFileDialog1.FileName = "ROM.bin";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog1.FileName, FileMode.Create)))
                {
                    byte[] buffer = datas.ToArray();
                    writer.Write(buffer, 0, buffer.Length);
                }
            }
        }

        private void bWriteAll_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.FileName = "ROM.bin";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(openFileDialog1.FileName);

                    _serialPort.DiscardInBuffer();
                    _serialPort.DiscardOutBuffer();

                    using (BinaryReader reader = new BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open)))
                    {
                        byte[] buffer = new byte[fi.Length];
                        reader.Read(buffer, 0, (int)fi.Length);
                        WriteAll(buffer);
                    }
                }
            }
            catch { }
        }

        #endregion

        #region Metodi (accesso privato)

        private bool Connetti()
        {
            try
            {
                _serialPort.Open();
                Thread.Sleep(1000);
                return _serialPort.IsOpen;
            }
            catch
            {
                return false;
            }
        }

        private bool Disconnetti()
        {
            _serialPort.Close();
            return !_serialPort.IsOpen;
        }

        private string LeggiVersione()
        {
            string s = "";
            try
            {
                _serialPort.Write("VERSION=?\r");

                // 5 secondi di timeout
                int expiredTick = Environment.TickCount + 5000;
                while (Environment.TickCount < expiredTick)
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        s = _serialPort.ReadLine();
                        string[] split = s.Trim('\r').Split(new char[] { '=' });
                        if (split[0] == "+VERSION")
                        {
                            s = split[1];
                            break;
                        }
                    }
                }

                return s;
            }
            catch
            {
                return s;
            }
        }

        private bool ReadAll(List<byte> datas)
        {
            try
            {
                _serialPort.Write("READALL=?\r");

                // 64 Kb = 8192 Byte
                byte[] buffer = new byte[0x2000];
                int offset = 0;

                while (true)
                {
                    int count = _serialPort.BytesToRead;
                    if (count > 0)
                    {
                        _serialPort.Read(buffer, offset, count);
                        offset += count;
                    }

                    if (offset == 0x2000)
                    {
                        datas.AddRange(buffer);
                        break;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool WriteAll(byte[] buffer)
        {
            try
            {
                // 32 byte per pagina massimo che sembrano comprensivi dei 2 byte dell'indirizzo
                // quindi scriviamo blocchi di 16 byte per sicurezza
                int npag = buffer.Length / 16;

                for (int i = 0; i < npag; i++)
                {
                    int addr = i * 16;
                    _serialPort.Write("WRITEBUFFER=" + addr.ToString() + ",16\r");
                    //Thread.Sleep(1);

                    for (int j = addr; j < addr + 16; j++)
                    {
                        //Debug.Print("Address: " + j.ToString() + " - Byte: " + buffer[j].ToString("X2"));
                        _serialPort.Write(buffer, j, 1);
                        Thread.Sleep(1);
                    }

                    //Thread.Sleep(1);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}
