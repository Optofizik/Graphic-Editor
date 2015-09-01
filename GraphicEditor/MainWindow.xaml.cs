using GraphicEditor.Drawing;
using GraphicEditor.Strokes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Draw artist;
        FileWorker fileWorker;
        ImageEditor editor;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            artist = new FreeDraw(drawCanvas);
            drawCanvas.MouseLeftButtonDown += artist.StartDrawing;
            drawCanvas.MouseMove += artist.Drawing;
            drawCanvas.MouseLeftButtonUp += artist.EndDrawing;

            fileWorker = new FileWorker(drawCanvas);
            openFile.Click += fileWorker.OpenFile;
            saveFile.Click += fileWorker.SaveFile;
            editor = new ImageEditor(drawCanvas, statusBar);
            colorInvert.Click += SlowImageOperation;
            vertical.Click += SlowImageOperation;
            horizontal.Click += SlowImageOperation;
            rotation90.Click += SlowImageOperation;
            rotation180.Click += SlowImageOperation;
            this.Closing += MainWindow_Closing;
            exit.Click += (n, m) =>
            {
                this.Close();
            };

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            artist.DropSurface();
            Unsubscriber(sender as Button);
            switch (b.Name)
            {
                case "free":
                    artist = new FreeDraw(drawCanvas);
                    break;
                case "line":
                    artist = new LineDraw(drawCanvas);
                    break;
                case "rect":
                    artist = new RectDraw(drawCanvas);
                    break;
                case "circle":
                    artist = new CircleDraw(drawCanvas);
                    break;
            }
            drawCanvas.MouseLeftButtonDown += artist.StartDrawing;
            drawCanvas.MouseMove += artist.Drawing;
            drawCanvas.MouseLeftButtonUp += artist.EndDrawing;
        }

        private void SlowImageOperation(object sender, RoutedEventArgs e)
        {
            EventHandler hand = null;

            switch ((sender as MenuItem).Name)
            {
                case "colorInvert":
                    hand = new EventHandler(editor.InvertColors);
                    break;
                case "horizontal":
                case "vertical":
                    hand = new EventHandler(editor.MirrorReflection);
                    break;
                case "rotation90":
                case "rotation180":
                    hand = new EventHandler(editor.RotationTransform);
                    break;
            }

            SlowExecuter slow = new SlowExecuter(sender, statusBar, hand);
            slow.BeginExecute();
        }

        private void selection_Click(object sender, RoutedEventArgs e)
        {
            Unsubscriber(sender as Button);
            drawCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

        void Unsubscriber(Button but)
        {
            Button b = but;
            Button prev = buttonGrid.Children.Cast<Button>().Where(c => c.IsEnabled == false).First();
            prev.IsEnabled = true;
            b.IsEnabled = false;

            drawCanvas.MouseLeftButtonDown -= artist.StartDrawing;
            drawCanvas.MouseMove -= artist.Drawing;
            drawCanvas.MouseLeftButtonUp -= artist.EndDrawing;
        }


        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            ClosingWindow close = new ClosingWindow(this);
            close.ShowDialog();
        }

        private void cleaner_Click(object sender, RoutedEventArgs e)
        {            
            drawCanvas.Strokes.Clear();
        }

        private void eraser_Click(object sender, RoutedEventArgs e)
        {
            drawCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            Unsubscriber(sender as Button);
        }

        private void backgroundColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (drawCanvas.Background is ImageBrush)
            {
                drawCanvas.Background = null;
                drawCanvas.Background = new SolidColorBrush(backgroundColor.SelectedColor.Value);
            }
        }



    }
}
