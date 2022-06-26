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
using EasyModbus;
using Modbus.Device;
using Modbus.Data;
using Modbus.Utility;






namespace modbus_test
{
    public partial class ReadHoldingRegistr : Form
    {
      

        public ReadHoldingRegistr()
        {
            InitializeComponent();
        }
       
       

     
       
        private void Form1_Load(object sender, EventArgs e)
        {
      
           


        }

        private void textResult_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void btnRHR_Click(object sender, EventArgs e)
               
        {
            SerialPort serialPort = null;
            string portName = comboBox1.SelectedItem.ToString();
            //подлкючение портов 
            try
            {
                serialPort = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
                serialPort.Open();
            }

            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                byte slaveAddress = 1;
                ushort startAddress = 40001;
                ushort numberOfPoints = 25;
                IModbusMaster masterRTU = ModbusSerialMaster.CreateRtu(serialPort);
                ushort[] result = masterRTU.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);

                //display result 

                textResult.Text = string.Empty;
                foreach (ushort item in result)
                {
                    textResult.Text += string.Format("{0}/", item);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                serialPort.Close();
            }
        }

        //com0com
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            var portList = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(portList);
        }
    }
}
