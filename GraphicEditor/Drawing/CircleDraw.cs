using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using GraphicEditor.Strokes;
using System.Windows.Input;

namespace GraphicEditor.Drawing
{
    /*
     Класс, наследник класса Draw, который отвечает за рисование кругов
     */
    public class CircleDraw : Draw
    {
        private Ellipse circle;
        private Point center;
        private double width, height;

        public CircleDraw(InkCanvas surface) : base(surface) { }

        //Метод, отвечающий за установку центра круга
        public override void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            base.StartDrawing(sender, e);
            this.fillColor = (Application.Current.MainWindow as MainWindow).brushColor.SelectedColor.Value;
            center = e.GetPosition(Surface);
            circle = new Ellipse { Stroke = new SolidColorBrush(this.strokeColor), StrokeThickness = this.strokeThickness, Fill = new SolidColorBrush(this.fillColor) };
            InkCanvas.SetLeft(circle, center.X);
            InkCanvas.SetTop(circle, center.Y);
            Surface.Children.Add(circle);

            if (circle == null)
            {
                isDrawing = false;
                return;
            }
        }

        //Метод, отвечающий за выбор размера круга
        public override void Drawing(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point endPoint = e.GetPosition(Surface);
                circle.Width = width = Math.Abs(endPoint.X - center.X);
                circle.Height = height = Math.Abs(endPoint.Y - center.Y);

                InkCanvas.SetLeft(circle, Math.Min(endPoint.X, center.X));
                InkCanvas.SetTop(circle, Math.Min(endPoint.Y, center.Y));
            }
        }

        //Метод, отвечающий за рисование круга с помощью штрихов
        public override void EndDrawing(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing == true)
            {
                CircleStroke cr = new CircleStroke(new StylusPointCollection { new StylusPoint(InkCanvas.GetLeft(circle) + width / 2, InkCanvas.GetTop(circle) + height / 2) }, this.strokeColor, this.strokeThickness, this.fillColor, width / 2, height / 2);
                Surface.Strokes.Add(cr);
                Surface.Children.Remove(circle);
                circle = null;
                isDrawing = false;
            }
        }
    }
}
