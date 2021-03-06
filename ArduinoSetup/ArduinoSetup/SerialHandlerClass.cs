﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArduinoSetup
{
    class SerialHandlerClass
    {
        /// <summary>
        /// This is the port the arduino will be on and is used for the connections
        /// </summary>
        private SerialPort port;
        /// <summary>
        /// This is an instance of the normal form so that it can modify it from the thread
        /// </summary>
        private ArduinoControllerForm form;
        /// <summary>
        /// This controller sets up the class so that when we run it as a thread it can act autonomously
        /// </summary>
        public SerialHandlerClass(String port, ArduinoControllerForm form)
        {
           this.port = new SerialPort(port, 9600); 
           this.port.Open();
            this.form = form;
        }
        /// <summary>
        /// This is used to send chars to the arduino
        /// </summary>
        public void SendChar(char val)
        {
            port.Write(val+"");
        }
        /// <summary>
        /// This reads whatever the arduino writes back 
        /// </summary>
        public void ReadChar()
        {
            while (true)
            {
                Thread.Sleep(1000);
                var s = port.ReadExisting();
                if (s != "")
                {
                    form.SetText(s);
                }
            }
        }

    }
}
