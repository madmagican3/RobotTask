using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
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
        /// This is used to indiciate if we should look for a number 
        /// </summary>
        private bool gettingWallNumber;
        
        /// <summary>
        /// This controller sets up the class so that when we run it as a thread it can act autonomously
        /// </summary>
        public SerialHandlerClass(String port, ArduinoControllerForm form)
        {
           this.port = new SerialPort(port, 9600); 
           this.port.Open();
        }
        /// <summary>
        /// This is used to send chars to the arduion
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
                var s = Console.ReadLine();
                if (s != null)
                {
                    form.SerialReturnsList.Items.Add(s);
                    IdentifyChar(s);
                }
            }
        }
        /// <summary>
        /// This will check to see if the chars are the ones we need to interact with
        /// </summary>
        public void IdentifyChar(String localString)
        {
            int tempNo;
            if (localString.Length <= 0)
            {
                return;
            }
            var charArray = localString.ToCharArray();
            // if we've encountered a wall
            if (charArray[0] == 'w')
            {
                
            }else if (gettingWallNumber && int.TryParse(localString,out tempNo))
            {
                
            }
        }
    }
}
