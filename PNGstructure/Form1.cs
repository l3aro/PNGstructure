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
                var continue_check = true;

                // read PNG file signature
                if (file_type == "89-50-4E-47-0D-0A-1A-0A")
                {
                    info += "File type: PNG" + Environment.NewLine;
                }
                else
                {
                    info += "File type: not recognize";
                    continue_check = false;
                }

                if (continue_check)
                {
                    // read IHDR
                    info += "PNG image header: ";
                    string holder = data_as_hex.Substring(54, 5); // get 4 bytes that describe width 
                    holder = holder.Remove(2, 1); // remove "-" character
                    info += int.Parse(holder, System.Globalization.NumberStyles.HexNumber) + "x";

                    holder = data_as_hex.Substring(66, 5); // get 4 bytes that describe height 
                    holder = holder.Remove(2, 1); // remove "-" character
                    info += int.Parse(holder, System.Globalization.NumberStyles.HexNumber) + ", ";

                    holder = data_as_hex.Substring(72, 2); // get byte that describe bit depth
                    info += int.Parse(holder, System.Globalization.NumberStyles.HexNumber) + " bits/sample, ";

                    holder = data_as_hex.Substring(75, 2); // get byte that describe color type
                    switch (int.Parse(holder, System.Globalization.NumberStyles.HexNumber))
                    {
                        case 0:
                            info += "grayscale";
                            break;
                        case 2:
                            info += "truecolor";
                            break;
                        case 3:
                            info += "paletted";
                            break;
                        case 4:
                            info += "grayscale+alpha";
                            break;
                        case 6:
                            info += "truecolor+alpha";
                            break;
                        default:
                            info += "illegal color type";
                            break;
                    }

                    holder = data_as_hex.Substring(84, 2); // get byte that describe interlace method
                    if (int.Parse(holder, System.Globalization.NumberStyles.HexNumber) == 0)
                        info += ", noninterlaced" + Environment.NewLine;
                    else if (int.Parse(holder, System.Globalization.NumberStyles.HexNumber) == 1)
                        info += ", interlaced" + Environment.NewLine;
                    else
                        info += ", unrecognized interlace method" + Environment.NewLine;


                }

                textBox2.Text = info;

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
