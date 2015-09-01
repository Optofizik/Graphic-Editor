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
     Класс, наследник класса Draw, который отвечает за рисование прямых линий
     */
    public class LineDraw : Draw
    {
        private Line line;


        public LineDraw(InkCanvas surface) : base(surface)
        {
        }

        //Метода, который отвечает за установку первой точки новой линии
        public override void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            base.StartDrawing(sender, e);
            Point p = e.GetPosition(Surface);
            this.line = new Line { Stroke = new SolidColorBrush(this.strokeColor), StrokeThickness = this.strokeThickness };
            line.X1 = p.X;
            line.Y1 = p.Y;
            line.X2 = p.X;
            line.Y2 = p.Y;
            Canvas.SetLeft(line, p.X);
            Canvas.SetTop(line, p.Y);
            Surface.Children.Add(line);
            if (line == null)
            {
                isDrawing = false;
                return;
            }            
        }

        //Метод, который позволяет выбрать вторую точку
        public override void Drawing(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point p = e.GetPosition(Surface);
                line.X2 = p.X;
                line.Y2 = p.Y;
            }
        }

        //Метод, который по координатам линии, рисует штрих
        public override void EndDrawing(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing  == true)
            {
                LineStroke lineStroke = new LineStroke(new StylusPointCollection { new StylusPoint(line.X1, line.Y1), new StylusPoint(line.X2, line.Y2) }, strokeColor, strokeThickness);
                Surface.Strokes.Add(lineStroke);
                Surface.Children.Remove(line);
                line = null;
                isDrawing = false;
            }
        }
    }
}
