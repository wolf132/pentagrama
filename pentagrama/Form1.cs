using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace pentagrama
{
    public partial class Form1 : Form
    {
        private bool dragging;
        private Point dragCursorPoint; private Point dragFormPoint;
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800); this.Text = "Абоба";
            this.FormBorderStyle = FormBorderStyle.None; this.Paint += new PaintEventHandler(MainForm_Paint);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown); this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position; dragFormPoint = this.Location;
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

            BackColor = Color.DarkRed; int side = Math.Min(ClientSize.Width, ClientSize.Height);
            int centerX = side / 2; int centerY = side / 2;
            int outerRadius = side / 2 - 10; double innerRadius = outerRadius / 3;
            double angle = 5;
            double angleStep = Math.PI * 2 / 5;
            Point[] starPoints = new Point[10]; for (int i = 0; i < 5; i++)
            {
                int xOuter = (int)(centerX + outerRadius * Math.Cos(angle));
                int yOuter = (int)(centerY + outerRadius * Math.Sin(angle)); starPoints[i * 2] = new Point(xOuter, yOuter);
                int xInner = (int)(centerX + innerRadius * Math.Cos(angle + angleStep / 2));
                int yInner = (int)(centerY + innerRadius * Math.Sin(angle + angleStep / 2)); starPoints[i * 2 + 1] = new Point(xInner, yInner);
                angle += angleStep;
            }
            GraphicsPath starPath = new GraphicsPath(); starPath.AddPolygon(starPoints);
            this.Region = new Region(starPath);
        }
        private void Form1_Load(object sender, EventArgs e)
        { }
    }
}