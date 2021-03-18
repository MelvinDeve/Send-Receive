using System.Windows.Controls;

namespace ArschlochClient
{
    internal class PictureBox
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public Image Image { get; internal set; }
        public object SizeMode { get; internal set; }
    }
}