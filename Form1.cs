using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week3_Day4_2
{
    public partial class Form1 : Form
    {
        bool isSelected = false;
        bool isMouseDown = false;
        bool isResize = false;
        int x = 300 ;
        int y =150;
        int dx = 300 ;
        int dy =150;
        int width = 200;
        int higth = 200;
        int FontSize = 3;
        Color FontStyle =Color.Black;


        Rectangle rec;
        public Form1()
        {
            InitializeComponent();
           // this.comboBox1.SelectedIndex = 0;
            this.numericUpDown1.Value = 3;
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            rec = new Rectangle(this.x, this.y, this.width, this.higth);
            
            g.DrawEllipse(new Pen(FontStyle, FontSize), x, y, width, higth);
            if (isSelected)
            {
                Pen p = new Pen(Brushes.Blue, 2);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(p, x, y, this.width, this.higth);
                g.FillEllipse(new SolidBrush(Color.Black), (x + width / 2 - 5), (y + higth / 2 - 5), 10, 10); // middle
                g.FillEllipse(new SolidBrush(Color.Blue), (x + width / 2 - 5), (y - 5), 10, 10); // top 
                g.FillEllipse(new SolidBrush(Color.Blue), (x - 5), (y + higth / 2 - 5), 10, 10);//left
                g.FillEllipse(new SolidBrush(Color.Blue), (x + width - 5), (y + higth / 2 - 5), 10, 10); // right
                g.FillEllipse(new SolidBrush(Color.Blue),( x + width / 2 - 5),( y - 5 + higth), 10, 10); // bottom
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            
        }
        public bool checkIntract(MouseEventArgs mouse)
        {
            var rect1 = new System.Drawing.Rectangle(this.x, this.y, this.width, this.higth);
            var rect2 = new System.Drawing.Rectangle(mouse.X, mouse.Y, 10, 10);
            return rect1.IntersectsWith(rect2);

        }
        public bool checkUp(MouseEventArgs mouse)
        {
            var rect1 = new System.Drawing.Rectangle((x + width / 2 - 10), (y - 10), 50, 50);
            var rect2 = new System.Drawing.Rectangle(mouse.X, mouse.Y, 10, 10);
            return rect1.IntersectsWith(rect2);
        }
        public bool checkdown(MouseEventArgs mouse)
        {
            var rect1 = new System.Drawing.Rectangle((x + width / 2 - 10), (y - 10 + higth), 50, 50);
            var rect2 = new System.Drawing.Rectangle(mouse.X, mouse.Y, 10, 10);
            return rect1.IntersectsWith(rect2);
        }
        public bool checkRight(MouseEventArgs mouse)
        {
            var rect1 = new System.Drawing.Rectangle((x + width - 10), (y + higth / 2 - 10), 50, 50);
            var rect2 = new System.Drawing.Rectangle(mouse.X, mouse.Y, 10, 10);
            return rect1.IntersectsWith(rect2);
        }
        public bool checkLeft(MouseEventArgs mouse)
        {
            var rect1 = new System.Drawing.Rectangle((x - 10), (y + higth / 2 - 10), 50, 50);
            var rect2 = new System.Drawing.Rectangle(mouse.X, mouse.Y, 10, 10);
            return rect1.IntersectsWith(rect2);
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkdown(e))
            {
                this.isSelected = true;
                this.Invalidate();
            }else
            if (checkIntract(e))
            {
                this.isSelected = true;
                this.Invalidate();
            }
            else
            {
                this.isSelected = false;
                this.Invalidate();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dx = e.Location.X - x;
            dy = e.Location.Y - y;
            if (isSelected && (checkdown(e) || checkUp(e) || checkRight(e) || checkLeft(e) ))
            {
                this.isMouseDown = true;
                isResize = true;
            }
            else if (isSelected && checkIntract(e))
            {
                this.isMouseDown = true;
                isResize = false;
            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int distX = e.X - x;
            int distY = e.Y - y;
            if (this.isMouseDown && isResize)
            {
                if (checkdown(e))
                {
                    this.higth = distY;
                    this.Invalidate();
                } else if (checkUp(e))
                {

                    this.y = e.Y;
                    this.higth -= distY;
                    
                    this.Invalidate();
                } else if (checkRight(e))
                {
                    this.width = distX ;
                    //this.x -= dx;

                    this.Invalidate();
                }else if (checkLeft(e))
                {
                    
                    this.x = e.Location.X ;
                    this.width -= distX;
                    this.Invalidate();
                }

            }
            else if(this.isMouseDown)
            
                
                {
                    this.x = e.Location.X - dx;
                    this.y = e.Location.Y - dy;
                    this.Invalidate();
                }
            
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.isMouseDown = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // this.comboBox1.SelectedIndex = 1;
            
            {
                FontSize = (int)this.numericUpDown1.Value;
                
                this.Invalidate();
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isSelected)
            {
                ColorDialog MyDialog = new ColorDialog();
                MyDialog.AllowFullOpen = true;
                if (MyDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(MyDialog.Color.ToString());
                    FontStyle = MyDialog.Color;
                    this.Invalidate();
                }

            }
        }
    }
}
