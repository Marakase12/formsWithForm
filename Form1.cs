using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // ������� ����� �����
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Purple; // ������������� ���������� ���� ����
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ������� ���� ��� ����� � ���� ������
            GraphicsPath starPath = CreateStarPath(new Rectangle(0, 0, 300, 300), 5, 150, 70);

            // ������������� ������ �����
            this.Region = new Region(starPath);
        }

        // ������� ��� �������� ������
        private GraphicsPath CreateStarPath(Rectangle bounds, int points, float outerRadius, float innerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            double angle = Math.PI / points;

            PointF[] pts = new PointF[points * 2];
            for (int i = 0; i < points * 2; i++)
            {
                float r = (i % 2 == 0) ? outerRadius : innerRadius;
                pts[i] = new PointF(
                    (float)(bounds.X + bounds.Width / 2 + r * Math.Cos(i * angle)),
                    (float)(bounds.Y + bounds.Height / 2 + r * Math.Sin(i * angle))
                );
            }

            path.AddPolygon(pts);
            return path;
        }

        // �������������� ����� ��������� �����
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // ����������� ����� ���������� ������
            e.Graphics.FillRegion(Brushes.Purple, this.Region);
        }

        // ��������� ����������� ������������� ����� �� ����� �������
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // ����������� ������ ��� ����������� ����� ������
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
    }
}
