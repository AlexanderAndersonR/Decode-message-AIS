using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace decode_message
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string ASCII_in_6bit(string coded_number)
        {
            string decoding_number = "";
            byte[] ASCIIBytes = Encoding.ASCII.GetBytes(coded_number);
            foreach (var item in ASCIIBytes)
            {
                if (item <= 87 && item >= 48)
                {
                    decoding_number = String.Format("{0:d6}", Convert.ToInt32(Convert.ToString((item - 48), 2)));
                }
                else if (item <= 119 && item >= 96)
                {
                    decoding_number = String.Format("{0:d6}", Convert.ToInt32(Convert.ToString((item - 56), 2)));
                }
            }
            return decoding_number;
        }
        public static string _6bit_in_ASCII(string coded_number)
        {
            string decoding_number = "";
            int counter = 0;
            var result = 0u;

            for (var i = coded_number.Length - 1; i >= 0; i--)
            {
                if (coded_number[i] == '1')
                {
                    result += Convert.ToUInt32(Math.Pow(2, counter));
                }
                counter++;
            }
            if (result <= 39 && result >= 0)
            {
                result += 48;
            }
            else if (result <= 63 && result >= 40)
            {
                result += 56;
            }
            byte[] ASCIIBytes = new byte[] { Convert.ToByte(result) };
            decoding_number = Encoding.ASCII.GetString(ASCIIBytes);
            return decoding_number;
        }
        private void Clear_All_TextBox_Click(object sender, EventArgs e)
        {
            //textBox1.Clear();
            //textBox2.Clear();
            //textBox3.Clear();
            //textBox4.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string message_ASCII = textBox_message.Text;
            string message_bit = "";
            char[] charArray = textBox_message.Text.ToCharArray();
            foreach (var item in charArray)
            {
                message_bit += ASCII_in_6bit(Convert.ToString(item));
            }
            textBox_message.Text += " колличество бит - " + message_bit.Length;
            string message_number = message_bit.Substring(0, 6);
            textBox_decode_message.Text = "Идентификатор номера сообщения " + "\t"+ message_number + " / "+ Convert.ToInt32(message_number,2) +"\r\n";
            message_bit = message_bit.Remove(0, 6);

            string repeat = message_bit.Substring(0, 2);
            textBox_decode_message.Text += "Индикатор повтора " + "\t" + repeat + " / " + Convert.ToInt32(message_number, 2) + "\r\n";
            message_bit = message_bit.Remove(0, 2);

            string MMSI = message_bit.Substring(0, 30);
            textBox_decode_message.Text += "MMSI " + "\t" + MMSI + " / " + Convert.ToInt32(MMSI, 2) + "\r\n";
            message_bit = message_bit.Remove(0, 30);

            string reserve = message_bit.Substring(0, 2);
            textBox_decode_message.Text += "Резерв " + "\t" + reserve + " / " + Convert.ToInt32(reserve, 2) + "\r\n";
            message_bit = message_bit.Remove(0, 2);

            textBox_decode_message.Text += "Идентификаторы района и типа сообщений: \r\n";

            string DAC = message_bit.Substring(0, 10);
            textBox_decode_message.Text += "DAC " + "\t" + DAC + " / " + Convert.ToInt32(DAC, 2) + "\r\n";
            message_bit = message_bit.Remove(0, 10);

            string FI = message_bit.Substring(0, 6);
            textBox_decode_message.Text += "FI " + "\t" + FI + " / " + Convert.ToInt32(FI, 2) + "\r\n";
            message_bit = message_bit.Remove(0, 6);

            //string message_number_coint = message_bit.Substring(0, 4);
            //textBox_decode_message.Text += "Количество сообщений  " + "\t" + message_number_coint + " / " + Convert.ToInt32(message_number_coint, 2) + "\r\n";
            //message_bit = message_bit.Remove(0, 4);

            //string message_number_system = message_bit.Substring(0, 4);
            //textBox_decode_message.Text += "Количество сообщений  " + "\t" + message_number_system + " / " + Convert.ToInt32(message_number_system, 2) + "\r\n";
            //message_bit = message_bit.Remove(0, 4);
            string decod_mess_test = "";
            string decod_mess = "";
            while (message_bit.Length>0)
            {
                if (message_bit.Length>6)
                {
                    decod_mess_test += message_bit.Substring(0,6) + "//";
                    decod_mess += message_bit.Substring(0, 6);
                    message_bit = message_bit.Remove(0, 6);
                }
                else
                {
                    decod_mess_test += message_bit;
                    decod_mess += message_bit;
                    break;
                }
            }
            textBox_decode_message.Text += decod_mess_test + "\r\n";
            textBox_decode_message.Text += decod_mess + "\r\n";
            textBox_decode_message.Text += "finish" + "\r\n";

        }

        private void button_test_Click(object sender, EventArgs e)
        {
            textBox_message.Text = "8=ASuh14C000000005";
        }
    }
}
