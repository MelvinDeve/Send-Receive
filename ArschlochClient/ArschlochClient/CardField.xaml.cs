using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace ArschlochClient
{
    /// <summary>
    /// Interaction logic for CardField.xaml
    /// </summary>
    public partial class CardField : Window
    {
        private PictureBox[] pictures;
        public const string imagePath = @"Assets/";

        public object PictureBoxSizeMode { get; private set; }

        public CardField()
        {
            InitializeComponent();
            pictures = new PictureBox[36];

        }

        private void BtnNewHand_Click(object sender, RoutedEventArgs e)
        {
            CreateContols();
            DisplayControls();
        }

        private void CreateContols()
        {
            for (int i = 0; i < 36; i++)
            {
                var newPictureBox = new PictureBox();
                newPictureBox.Width = 75;
                newPictureBox.Height = 100;

                pictures[i] = SizeImage(newPictureBox, i + 1);
            }
        }

        private PictureBox SizeImage(PictureBox pb, int i)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(imagePath + i.ToString() + ".png");
            bm.EndInit();

            return pb;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a new BitmapImage.
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri("Assets/");
            bm.EndInit();

            // ... Get Image reference from sender.
            var image = sender as Image;
            // ... Assign Source.
            image.Source = bm;
        }

        private void DisplayControls()
        {
            for (int i = 0; i < 8; i++)
            {
                pictures[i].Left = (i * 18) + 100;
                pictures[i].Top = 50;
                this.Controls.Add(pictures[i]);
            }
        }
    }
}
