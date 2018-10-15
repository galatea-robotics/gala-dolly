using System.Windows.Forms;

namespace Gala.Dolly.UI
{
    internal static class EventHandlers
    {
        public static void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
