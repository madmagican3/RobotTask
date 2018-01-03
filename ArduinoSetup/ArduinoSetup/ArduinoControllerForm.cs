using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// This is used in order to access the form from the serialHandlerClass
        /// </summary>
        /// <param name="text"></param>
        public delegate void SetTextCallback(string text);
        /// <summary>
        /// This is used to indiciate if we should look for a number 
        /// </summary>
        private bool gettingWallNumber;

        /// <summary>
        /// This is a cheap and easy way to work out if we've got to hide the override UI
        /// </summary>
        private int numberofTimesVCalled;
        /// <summary>
        /// This is used to keep track on if we hit a wall as we want different options available if we hit a wall
        /// </summary>
        private bool hitWall = false;

        /// <summary>
        /// This will initalize the program and get the correct com port
        /// </summary>
        public ArduinoControllerForm()
        {
            InitializeComponent();
            DetectArduinoPort();
            OverrideController(true);
            ChooseDirection(false);
            this.Closing += ArduinoControllerFormOnClosing;
        }

        /// <summary>
        /// This is to shut down the listener thread upon exiting the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ArduinoControllerFormOnClosing(object sender, CancelEventArgs e)
        {
            Environment.Exit(0);
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
        /// <summary>
        /// This should attempt to write to the arduino, if it responds we know it's the correct port
        /// </summary>
        /// <param name="currentPort"></param>
        /// <returns></returns>
        private bool DetectArduino(SerialPort currentPort)
        {
            try
            {
                currentPort.Open();
                currentPort.Write("c");
                Thread.Sleep(1000);

                var returnStr = currentPort.ReadExisting();
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
        /// <summary>
        /// /This is used so we dont start the thread earlier than the rest of the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArduinoControllerForm_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(_localSerialInstance.ReadChar));
            thread.Start();
            ChooseDirection(false);
            RoomOrCorridor.SelectedIndex = 0;
        }
        /// <summary>
        /// This is used to update the list box from the serial handler class
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            if (this.SerialReturnsList.InvokeRequired)
            {
                var d = new SetTextCallback(SetText);
                this.Invoke(d, text);
            }
            else
            {
                if (!IdentifyChar(text))//if we cant identify the character
                {
                    if (text.Contains("|"))// if it's got a pipe we've got to split the string
                    {
                        DelimitString(text);
                        return;
                    }
                    this.SerialReturnsList.Items.Add(text);
                    this.SerialReturnsList.TopIndex = SerialReturnsList.Items.Count - 1;
                }
            }
        }

        /// <summary>
        /// This will check to see if the chars are the ones we need to interact with
        /// </summary>
        public bool IdentifyChar(String localString)
        {
            int tempNo;
            if (localString.Length <= 0)
            {
                return false;
            }
            var charArray = localString.ToCharArray();
            // if we've encountered a wall
            if (charArray[0] == 'w' && charArray.Length == 1) 
            {
                this.SerialReturnsList.Items.Add("We've encountered a wall");
                hitWall = true;
                gettingWallNumber = true;
                return true;
            }
            else if (gettingWallNumber && int.TryParse(localString, out tempNo))
            {
                this.SerialReturnsList.Items.Add("At corridor no " + tempNo);
                gettingWallNumber = false;
                return true;
            }else if (charArray[0] == 'h' && charArray.Length == 1)
            { 
                this.SerialReturnsList.Items.Add("Paused, please select a location");
                ChooseDirection(true);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This enabled or disables the buttons for choosing direction depending on the bool passed
        /// false is hiding everything but pause and true is making them visible
        /// </summary>
        /// <param name="activate"></param>
        public void ChooseDirection(bool activate)
        {
            RightBtn.Visible = activate;
            LeftBtn.Visible = activate;
            BackBtn.Visible = activate;
            RoomOrCorridor.Visible = activate;
            PauseBtn.Visible = !activate;
            Resume.Visible = activate;
            label1.Visible = activate;
        }
        /// <summary>
        /// This is used to indicate our direction 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar(RoomOrCorridor.SelectedIndex == 0 ? 'd' : 'm');
            ChooseDirection(false);
            OverrideController(false);
        }
        /// <summary>
        /// This is used to indicate our direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar(RoomOrCorridor.SelectedIndex == 0 ? 'a' : 'b');
            ChooseDirection(false);
            OverrideController(false);
        }
        /// <summary>
        /// This is used to force a pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('p');
            ChooseDirection(true);
        }
        /// <summary>
        /// This is used to split the string via the pipe symbol and then write it to the list
        /// </summary>
        /// <param name="text"></param>
        private void DelimitString(string text)
        {
            var tempList = text.Split('|');
            foreach (var localString in tempList)
            {
                if (!IdentifyChar(localString))
                {
                    SerialReturnsList.Items.Add(localString);
                }
            }
        }
        /// <summary>
        /// This is used to force an override
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverrideBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('o');
            OverrideController(false);
        }
        /// <summary>
        /// This Cancels the override
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelOverrideBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('n');
            OverrideController(true);
        }
        /// <summary>
        /// This will resume normal running if we accidently hit pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resume_Click(object sender, EventArgs e)
        {
            ChooseDirection(false);
            OverrideController(false);
        }
        /// <summary>
        /// This controls the buttons for overriding, true if you want to disable everything, false if you want to enable everything
        /// </summary>
        /// <param name="activate"></param>
        private void OverrideController(bool activate)
        {
            FinishOverride.Visible = !activate;
            OBackBtn.Visible = !activate;
            OFowardBtn.Visible = !activate;
            OLeftBtn.Visible = !activate;
            ORiightBtn.Visible = !activate;
            OStopBtn.Visible = !activate;
        }
        /// <summary>
        /// This is for the finish override button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (RoomOrCorridor.SelectedIndex == 0) //if we're looking into a corridor
            {
                _localSerialInstance.SendChar('c');
                OverrideController(true);
            }
            else
            {
                _localSerialInstance.SendChar('v');
                if (numberofTimesVCalled % 2 != 0)
                {
                    OverrideController(true);
                    numberofTimesVCalled = 0;
                    return;
                }
                numberofTimesVCalled += 1;
                SerialReturnsList.Items.Add(
                    "The robot is checking the room, once that is done please reverse it back into the corridor");
            }
        }
        /// <summary>
        /// override foward button sends the character to go foward
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OFowardBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('w');
        }
        /// <summary>
        /// override left button sends the character to turn left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OLeftBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('a');
        }
        /// <summary>
        /// Override back button sends the character to reverse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OBackBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('s');
        }
        /// <summary>
        /// override right button sends the char to turn right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ORiightBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('d');
        }
        /// <summary>
        /// Override stop button sends the character to stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OStopBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('z');
        }

        /// <summary>
        /// this should send the return character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('r');
            OverrideController(false);
            ChooseDirection(false);
        }
        /// <summary>
        /// This should send the foward character to mark a room or corridor as being in front
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FowardBtn_Click(object sender, EventArgs e)
        {
            _localSerialInstance.SendChar('f');
        }
    }
}
