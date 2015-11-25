using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoggerModule;
using InterceptedModule;

namespace Modul
{
    public partial class PasMessege : Form
    {
        public PasMessege()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logger.Info("Вызов парольного окна");
            //TODO Подумать как сделать лучше выход из приложения, ибо это костыль
            Application.Exit();
        }
    }
}
