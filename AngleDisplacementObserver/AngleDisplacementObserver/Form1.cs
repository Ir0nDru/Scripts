using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AngleDisplacementObserver
{
    public partial class MainForm : Form
    {
        private readonly string _defaultAngleValue = "00,00";
        public SerialPort SerialPort { get; set; }        

        public MainForm()
        {            
            InitializeComponent();
            InitializeFields();            
        }

        private void InitializeFields()
        {
            portComboBox.Items.AddRange(SerialPort.GetPortNames().OrderBy(s => s).ToArray());
            portComboBox.SelectedItem = portComboBox.Items[0];            
        }

        private void ConnButton_Click(object sender, EventArgs e)
        {
            if (connButton.Text == "Open")
            {
                connButton.Text = "Close";
                Connect();
            }
            else
            {
                connButton.Text = "Open";
                Disconnect();
            }
                
        }

        private void Connect()
        {
            var selectedPort = portComboBox.SelectedItem.ToString();            
            portComboBox.Enabled = false;
            SerialPort = new SerialPort(selectedPort, 115200, Parity.None, 8, StopBits.One);
            try
            {
                if (!SerialPort.IsOpen)
                {
                    SerialPort.Open();
                }                    
                SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceived);                
                Log($"{selectedPort} opened");
            }
            catch (Exception e)
            {
                Log($"Failed to open {selectedPort}: {e.Message}");
                connButton.Text = "Open";
                portComboBox.Enabled = true;
            }
        }

        private void Disconnect()
        {                        
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();                
                Log($"Closed {portComboBox.SelectedItem}");
            }
            portComboBox.Enabled = true;
        }

        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = SerialPort.ReadLine();
            string formated = data.Replace('.', ',');
            Log($"Received message from {SerialPort.PortName}: {data}");
            if (float.TryParse(formated, out _))
            {
                formated = data.Replace(',', '.');
                angleLabel.Invoke((MethodInvoker)(() => angleLabel.Text = formated));
            }
            else
            {
                angleLabel.Invoke((MethodInvoker)(() => angleLabel.Text = _defaultAngleValue));
            }
            
        }

        private void Log(string message)
        {
            DateTime dt = DateTime.Now;
            string time = dt.ToShortTimeString();
            logBox.Invoke((MethodInvoker)(() => logBox.AppendText($"[{time}] {message}\n")));
        }
    }
}
