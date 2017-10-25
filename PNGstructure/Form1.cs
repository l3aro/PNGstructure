using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGstructure
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = "";

                // read hex data
                BinaryReader reader = new BinaryReader(new FileStream("C:\\Users\\dgbao\\Pictures\\what.png", FileMode.Open, FileAccess.Read, FileShare.None));
                reader.BaseStream.Position = 0x0;     // The offset we reading the data from
                byte[] fileData = new byte[reader.BaseStream.Length]; // bind reader to byte arry so we can extract data length
                byte[] data = reader.ReadBytes(fileData.Length); // Read data
                reader.Close();

                string data_as_str = Encoding.Default.GetString(data); 
                string data_as_hex = BitConverter.ToString(data);
                
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
