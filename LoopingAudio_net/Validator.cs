using System;
using System.Windows.Forms;

namespace LoopingAudio_net
{
    public static class Validator
    {
        public static bool IsWithinRange(TextBox textBox, int min = 0, int max = 100)
        {
            int number = Convert.ToInt32(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show($"The value in {textBox.Tag} must be between {min} and {max}."
                   , "Out of Range Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public static bool IsWithinRange(TextBox textBox, uint min = 0, uint max = 100)
        {
            uint number = Convert.ToUInt32(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show($"The value in {textBox.Tag} must be between {min} and {max}."
                   , "Out of Range Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public static bool IsInteger(TextBox textBox)
        {
            if (!int.TryParse(textBox.Text, out _))
            {
                MessageBox.Show($"The value, '{textBox.Text}', in {textBox.Tag} is not a valid integer.", "Invalid Entry");
                textBox.Focus();
                return false;
            }
            else
                return true;
        }

        public static bool IsUnsignedInteger(TextBox textBox)
        {
            if (!uint.TryParse(textBox.Text, out _))
            {
                MessageBox.Show($"The value, '{textBox.Text}', in {textBox.Tag} is not a valid (positive) integer.", "Invalid Entry");
                textBox.Focus();
                return false;
            }
            else
                return true;
        }

        public static bool IsNotEmpty(TextBox textBox)
        {
            if (textBox.Text == String.Empty)
            {
                MessageBox.Show($"{textBox.Tag} cannot be empty.", "Empty Textbox");
                textBox.Focus();
                return false;
            }
            else
                return true;
        }

        public static bool IsSelected(ListBox listBox)
        {
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show($"You have not selected anything from the {listBox.Tag}.", "No Selection");
                return false;
            }
            else
                return true;
        }
    }
}
