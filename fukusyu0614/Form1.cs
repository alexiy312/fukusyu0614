using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fukusyu0614
{
    public partial class Form1 : Form
    {
        int vx = -10;
        int vy = -10;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left += vx;
            label1.Top += vy;

            //跳ね返り処理
            if (label1.Left < 0)
            {
                vx = Math.Abs(vx);
            }
            if (label1.Left > ClientSize.Width - label1.Width) 
            {
                vx = -Math.Abs(vx);
            }
            if (label1.Top < 0)
            {
                vy = Math.Abs(vy);
            }
            if (label1.Top > ClientSize.Height - label1.Height) 
            {
                vy = -Math.Abs(vy);
            }

            //マウスが重なったら停止
            Point p = PointToClient(MousePosition);

            if ((label1.Left < p.X) && (label1.Right > p.X) && (label1.Top < p.Y) && (label1.Bottom > p.Y)) 
            {
                timer1.Enabled = false;
            }
        }
    }
}
