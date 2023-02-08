using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstGame
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;
        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score + Environment.NewLine + "Collect All Coins";
            player.Top += jumpSpeed;
            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            } 
            else
            {
                jumpSpeed = 10;
            }
            foreach (Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;
                        }
                        x.BringToFront();
                    }
                    if((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            ++score;
                        }
                    }
                    if((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine + "Pff! You were burnt alive!";
                        }
                    }
                }
            }
            verticalPlatform.Top += verticalSpeed;

            if (verticalPlatform.Top < 200 || verticalPlatform.Top > 581)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;

            if (enemyOne.Left < pictureBox6.Left || enemyOne.Left + enemyOne.Width > pictureBox6.Left + pictureBox6.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;

            if (enemyTwo.Left < pictureBox8.Left || enemyTwo.Left + enemyTwo.Width > pictureBox8.Left + pictureBox8.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death!";
            }

            if (player.Bounds.IntersectsWith(exitDoor.Bounds) && score == 28)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You Won! Well Done!!";
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }


        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score: " + score + Environment.NewLine + "Collect All Coins";

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            // reset position of player platform and enemies

            player.Left = 37;
            player.Top = 634;

            enemyOne.Left = 456;
            enemyTwo.Left = 394;

            verticalPlatform.Top = 262;

            gameTimer.Start();

        }
    }
}
