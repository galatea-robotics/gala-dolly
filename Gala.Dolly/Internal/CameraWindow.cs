using System.Drawing;
using System.Windows.Forms;

namespace Gala.Dolly.UI
{
    internal  partial class CameraWindow : motion.CameraWindow
    {
        public CameraWindow()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (pe == null)
            {
                throw new Galatea.TeaArgumentNullException("pe");
            }

            base.OnPaint(pe);

            if(_centerOverlay)
            {
				using (Pen overlayPen = new Pen(Color.FromArgb(48, 255, 255, 255)))
				{
					// Horizontal
					Point x1 = new Point(0, this.Height / 2);
					Point x2 = new Point(this.Width, this.Height / 2);
					pe.Graphics.DrawLine(overlayPen, x1, x2);

					// Vertical
					Point y1 = new Point(this.Width / 2, 0);
					Point y2 = new Point(this.Width / 2, this.Height);
					pe.Graphics.DrawLine(overlayPen, y1, y2);
				}
            }
        }

        public bool CenterOverlay { get { return _centerOverlay; } set { _centerOverlay = value; } }

        private bool _centerOverlay;
    }
}
