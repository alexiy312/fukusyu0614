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
        //const = 定数 (ないと変数)
        const int num = 3;
        int k = num;

        enum SCENE
        {
            TITLE,
            GAME,
            GAMEOVER,
            CLEAR,
            NONE
        }
        /// <summary>
        /// 現在のシーン
        /// </summary>
        SCENE nowScene;

        /// <summary>
        /// 切り替えたいシーン
        /// </summary>
        SCENE nextScene;

        int[] vx = new int[num];
        int[] vy = new int[num];

        //label 全体の記憶領域確保
        Label[] labels = new Label[num];

        //ランダム値の生成
        private static Random rand = new Random();

        public Form1()
        {
            InitializeComponent();

            //次のシーンと今のシーンを初期化
            nextScene = SCENE.TITLE;
            nowScene = SCENE.NONE;

            for (int i = 0; i < num; i++)
            {
                //label 個々の設定用の記憶領域確保
                labels[i] = new Label();

                //labels の作成
                Controls.Add(labels[i]);

                //labels の設定
                labels[i].ForeColor = label1.ForeColor;
                labels[i].Text = "🐜";
                labels[i].AutoSize = true;

                //開始位置ランダム化
                labels[i].Left = rand.Next(ClientSize.Width - label1.Width);
                labels[i].Top = rand.Next(ClientSize.Height - label1.Height);

                //開始速度ランダム化
                vx[i] = rand.Next(-10, 11);
                vy[i] = rand.Next(-10, 11);
            }
        }

        void MyFunc()
        {
            MessageBox.Show("MyFunc");
        }

        void initProc()
        {
            //nextSceneがNONEだったら、何もしない
            if (nextScene == SCENE.NONE)
                return;

            nowScene = nextScene;
            nextScene = SCENE.NONE;

            switch (nowScene)
            {
                case SCENE.TITLE:
                    label2.Visible = true;
                    button1.Visible = true;
                    button1.Enabled = true;
                    label3.Visible = false;
                    button2.Visible = false;
                    button2.Enabled = false;
                    for (int i = 0; i < num; i++)
                        labels[i].Visible = false;
                        break;
                case SCENE.GAME:
                    label2.Visible = false;
                    button1.Visible = false;
                    button1.Enabled = false;
                    for (int i = 0; i < num; i++)
                        labels[i].Visible = true;
                    break;
                case SCENE.CLEAR:
                    label3.Visible = true;
                    button2.Visible = true;
                    button2.Enabled = true;
                    break;
            }
        }

        void updateProc()
        {
            if (nowScene == SCENE.GAME)
            {
                updateGame();
            }
            if (k == 0)
            {
                nextScene = SCENE.CLEAR;
                k = num;
            }
        }

        void updateGame()
        {
            label1.Text = "残り：" + k + "体";

            for (int i = 0; i < num; i++)
            {
                labels[i].Left += vx[i];
                labels[i].Top += vy[i];

                //跳ね返り処理
                if (labels[i].Left < 0)
                {
                    vx[i] = Math.Abs(vx[i]);
                }
                if (labels[i].Left > ClientSize.Width - labels[i].Width)
                {
                    vx[i] = -Math.Abs(vx[i]);
                }
                if (labels[i].Top < 0)
                {
                    vy[i] = Math.Abs(vy[i]);
                }
                if (labels[i].Top > ClientSize.Height - labels[i].Height)
                {
                    vy[i] = -Math.Abs(vy[i]);
                }

                Point p = PointToClient(MousePosition);

                if ((labels[i].Left < p.X) && (labels[i].Right > p.X) && (labels[i].Top < p.Y) && (labels[i].Bottom > p.Y) && (labels[i].Visible))
                {
                    k--;
                    labels[i].Visible = false;
                    //timer1.Enabled = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            initProc();
            updateProc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextScene = SCENE.GAME;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nextScene = SCENE.TITLE;
        }
    }
}
