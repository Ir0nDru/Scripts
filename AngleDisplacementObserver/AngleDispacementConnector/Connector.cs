using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;

namespace AngleDispacementConnector
{
    class Connector
    {
        private static Connector instance;

        private SerialPort Port;

        private TestingData TesingData;

        private Connector()
        {
            RefreshPorts();
        }

        public List<string> AvailablePorts { get; set; }

        public float CurrentAngle { get; set; }

        public static Connector GetInstance()
        {
            if (instance == null)
            {
                instance = new Connector();
            }

            return instance;
        }

        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = Port.ReadLine();                        
            if (float.TryParse(data, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out float num))
            {
                CurrentAngle = num;
            }
            else if (data.StartsWith("T"))
            {
                if (int.TryParse(data.Remove(0, 1), out var testData))
                {

                }
                //TODO testing
            }
            else if (data.StartsWith("K"))
            {
                //TODO kalibrating
            }
        }

        public void RefreshPorts()
        {
            AvailablePorts = new List<string>(SerialPort.GetPortNames());
        }

        public bool Connect(string com)
        {
            if (AvailablePorts.Contains(com))
            {
                return false;
            }
            Port = new SerialPort(com, 115200, Parity.None, 8, StopBits.One);
            try
            {
                if (!Port.IsOpen)
                {
                    Port.Open();
                }
                Port.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceived);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Disconnect()
        {
            if (Port.IsOpen)
            {
                Port.Close();
            }
        }

        public struct TestingData
        {
            public bool CpuOk;
            public bool Adc1Ok;
            public bool Adc2Ok;
            public bool Photo1ok;
            public bool Photo2ok;
            public bool LedOk;
        }
    }
}
