using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controll_Led_Arduino
{
    public partial class Form1 : Form
    {

        // Configuration for Arduino
        SerialPort currentPort;
        bool portFound;
        int baudRate = 9600;
        string returnMessage = "";
        string arduinoMessage = "HELLO FROM ARDUINO";


        public Form1()
        {
            InitializeComponent();

            SetComPort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(portFound)
            {
                currentPort.Open();
                if (currentPort.IsOpen)
                {
                    currentPort.WriteLine("A");
                }
                currentPort.Close();
                panel1.BackColor = Color.Lime;
                panel2.BackColor = Color.Transparent;
            }
            else
            {
                arduinoPortText.Text = "No Arduino found";
                arduinoPortText.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (portFound)
            {
                currentPort.Open();
                if (currentPort.IsOpen)
                {
                    currentPort.WriteLine("a");
                }
                currentPort.Close();
                panel2.BackColor = Color.Red;
                panel1.BackColor = Color.Transparent;
            }
            else
            {
                arduinoPortText.Text = "No Arduino found";
                arduinoPortText.ForeColor = Color.Red;
            }
        }


        /// Searching Arduino ///
        
        private void SetComPort()
	    {
	        try
	        {
		        string[] ports = SerialPort.GetPortNames();
		        foreach (string port in ports)
		        {
		            currentPort = new SerialPort(port, baudRate);
		            
                    if (DetectArduino())
		            {
			            portFound = true;
                        arduinoPortText.Text = "Connected to : " + currentPort.PortName;
                        arduinoPortText.ForeColor = Color.Green;
			            break;
		            }
		            else
		            {
			            portFound = false;
		            }
		        }
	        }
	        catch (Exception e)
	        {
                Console.WriteLine(e.ToString());
	        }
    	}

	    private bool DetectArduino()
	    {
	        try
	        {
                currentPort.Open();
                currentPort.WriteLine("z");

                returnMessage = "";
                
                while (!returnMessage.Contains(arduinoMessage))
                {
                    returnMessage += Convert.ToString(Convert.ToChar(currentPort.ReadChar()));
                }

                currentPort.Close();

                if (returnMessage.Contains(arduinoMessage))
		        {
		            return true;
		        }
		        else
                {
		            return false;
		        }

	        }
	        catch (Exception e)
	        {
                Console.WriteLine(e.ToString());
		        return false;
	        }       
        }

    } 

}
