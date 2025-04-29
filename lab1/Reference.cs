using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace lab1
{
    internal class Reference
    {
        private readonly string _helpPath;
        private readonly string _aboutPath;

        public Reference(string helpPath, string aboutPath)
        {
            _helpPath = helpPath;
            _aboutPath = aboutPath;
        }

        public void ShowHelp()
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = _helpPath, UseShellExecute = true });
            }
            catch (Exception)
            {
                MessageBox.Show("Файл справки не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowAbout()
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = _aboutPath, UseShellExecute = true });
            }
            catch (Exception)
            {
                MessageBox.Show("Файл 'О программе' не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}