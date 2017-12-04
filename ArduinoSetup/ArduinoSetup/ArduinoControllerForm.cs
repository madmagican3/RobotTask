using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace ArduinoSetup
{
    public partial class ArduinoControllerForm : Form
    {
        /// <summary>
        /// This is a local instance of the serialHandlerClass so as to allow writing chars when required
        /// </summary>
        private SerialHandlerClass _localSerialInstance;
        /// <summary>
        /// This will initalize the program and get the correct com port
        /// </summary>
        public ArduinoControllerForm()
        {
            InitializeComponent();
            DetectArduinoPort();
        }
        /// <summary>
        /// This detects the com port
        /// </summary>
        public void DetectArduinoPort()
        {
            try
            {
                //repeat until the arduinos started up
                while (_localSerialInstance == null) { 
                    //Get all the ports
                    var ports = SerialPort.GetPortNames();
                    foreach (var port in ports)
                    {
                        //for each port
                        SerialPort currentPort = new SerialPort(port, 9600);
                        //attempt to write to it
                        if (DetectArduino(currentPort))
                        {
                            //and if it's correct create a new instace of the serialhandlerclass
                            _localSerialInstance = new SerialHandlerClass(port, this);
                            Thread thread = new Thread(new ThreadStart(_localSerialInstance.ReadChar));
                            thread.Start();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        //This should attempt to write to the arduino and if it's there it will respond and we know the correct port
        
        private bool DetectArduino(SerialPort currentPort)
        {
            try
            {
                currentPort.Open();
                currentPort.Write("c");
                Thread.Sleep(1000);

                String returnStr = currentPort.ReadLine();
                currentPort.Close();
                if (returnStr != "")
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
