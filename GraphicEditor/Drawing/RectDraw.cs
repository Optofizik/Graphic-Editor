using System;
using System.Collections.Generic;
using System.Linq;
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
     Класс, наследник класса Draw, который отвечает за рисование прямоугольников
     */
    public class RectDraw : Draw
    {
        private Rectangle rect;
        Point startPoint;
        Point endPoint;

        public RectDraw(InkCanvas surface) : base(surface) { }

        //Метод, отвечающий за установку первой вершины прямоугольника
        public override void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            base.StartDrawing(sender, e);
            this.fillColor = (Application.Current.MainWindow as MainWindow).brushColor.SelectedColor.Value;
            startPoint = e.GetPosition(Surface);
            rect = new Rectangle { Stroke = new SolidColorBrush(this.strokeColor), StrokeThickness = this.strokeThickness, Fill = new SolidColorBrush(this.fillColor) };


            InkCanvas.SetLeft(rect, startPoint.X);
            InkCanvas.SetTop(rect, startPoint.Y);
            Surface.Children.Add(rect);
            if (rect == null)
            {
                isDrawing = false;
                return;
            }

        }

        //Метод, отвечающий за выбор размера и расположения прямоугольника
        public override void Drawing(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.GetPosition(Surface);

                double x = Math.Min(endPoint.X, startPoint.X);
                double y = Math.Min(endPoint.Y, startPoint.Y);

                double width = Math.Abs(endPoint.X - startPoint.X);
                double height = Math.Abs(endPoint.Y - startPoint.Y);

                rect.Width = width;
                rect.Height = height;

                InkCanvas.SetLeft(rect, x);
                InkCanvas.SetTop(rect, y);

            }
        }


        //Метод, отвечающий за рисование прямоугольника с помощью штрихов
        public override void EndDrawing(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing == true)
            {
                RectangleStroke lineStroke = new RectangleStroke(new StylusPointCollection { new StylusPoint(startPoint.X, startPoint.Y), new StylusPoint(endPoint.X, endPoint.Y) }, this.strokeColor, this.strokeThickness, this.fillColor);

                Surface.Strokes.Add(lineStroke);
                Surface.Children.Remove(rect);
                rect = null;
                isDrawing = false;
            }
        }
    }
}