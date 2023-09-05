using System;
using System.IO;
using System.Windows.Forms;

namespace charSplitTools
{
    public partial class Form1 : Form
    {
        string content=null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    content = "";
                    content = File.ReadAllText(filePath);
                    MessageBox.Show("讀取完成");
                    button2.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
        private string FormatString(string input)
        {
            string formatted = string.Empty;
            int interval=Convert.ToInt16(numericUpDown1.Value);
            string insert = null;
            if (comboBox1.Text == "請輸入增添符號"|| interval==0)
                return null;
            else if (comboBox1.Text == "空格")
                insert = " ";
            else
                insert = comboBox1.Text;
            for (int i = 0; i < input.Length; i += interval)
                if (i + 1 < input.Length)
                    formatted += input.Substring(i, interval) + insert; // Insert a space between every two characters
                else
                    formatted += input.Substring(i);
            return formatted;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string modifyContent = FormatString(content);
            if (modifyContent == null)
            {
                MessageBox.Show("請輸入符號及間距");
                return;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        File.WriteAllText(filePath, modifyContent);
                        MessageBox.Show("轉換成功", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { MessageBox.Show("轉換失敗" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}
