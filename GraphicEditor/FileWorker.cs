using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace GraphicEditor
{
    //Класс, отвечающий за работу с файлами: открытие и сохранение
    public class FileWorker
    {
        private InkCanvas surface;

        public FileWorker(InkCanvas surface)
        {
            this.surface = surface;
        }

        //Метод, который отвечает за открытие изображения и установку его в качестве фона InkCanvas
        public void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "JPEG image|*.jpg;*.jpeg|PNG image|*.png";
            dialog.ShowDialog();

            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                ImageBrush image = new ImageBrush();
                Uri ur = new Uri(dialog.FileName, UriKind.RelativeOrAbsolute);
                BitmapImage bi = new BitmapImage(ur);
                surface.Strokes.Clear();
                surface.Background = new ImageBrush(bi);
            }
        }

        //Метод, который отвечает сохранение изображения
        public void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfdialog = new SaveFileDialog();
            sfdialog.Filter = "JPEG image|*.jpg;*.jpeg";
            sfdialog.FileName = "default";
            sfdialog.DefaultExt = ".jpg";
            bool? result = sfdialog.ShowDialog();

            if (result == true)
            {
                string fileName = sfdialog.FileName;           

                int width = (int)surface.ActualWidth;
                int height = (int)surface.ActualHeight;
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                renderBitmap.Render(surface);

                using(FileStream stream = new FileStream(fileName,FileMode.Create))
                {
                    JpegBitmapEncoder jpeg = new JpegBitmapEncoder();
                    jpeg.Frames.Add(BitmapFrame.Create(renderBitmap));
                    jpeg.Save(stream);
                }
            }
        }
    }
}
