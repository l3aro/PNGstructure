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

                string info = "";

                // read hex data
                BinaryReader reader = new BinaryReader(new FileStream("C:\\Users\\dgbao\\Pictures\\what.png", FileMode.Open, FileAccess.Read, FileShare.None));
                reader.BaseStream.Position = 0x0;     // The offset we reading the data from
                byte[] fileData = new byte[reader.BaseStream.Length]; // bind reader to byte arry so we can extract data length
                byte[] data = reader.ReadBytes(fileData.Length); // Read data
                reader.Close();

                string data_as_str = Encoding.Default.GetString(data); 
                string data_as_hex = BitConverter.ToString(data);

                string file_type = data_as_hex.Substring(0, 23);

                // read PNG file signature
                if (file_type == "89-50-4E-47-0D-0A-1A-0A")
                {
                    info += "File type: PNG\n";
                }
                else
                {
                    info += "File type: not recognize";
                }
                
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
