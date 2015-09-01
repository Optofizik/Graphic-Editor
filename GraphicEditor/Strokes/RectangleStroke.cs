using GraphicEditor.Strokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditor
{
    /*
     Класс, который рисует прямоугольники с помощью штрихов
     */
    class RectangleStroke : CustomStroke
    {
        public RectangleStroke(StylusPointCollection col, Color strokeColor, int strokeThickness,Color fillColor)
            : base(col, strokeColor, strokeThickness, fillColor)
        { }

        //Переопредение базового метода, которое позволяет построить прямоугольник, имея левую нижнюю и правую верхнюю вершины
        protected override void DrawCore(DrawingContext context, DrawingAttributes overrides)
        {
            StylusPointCollection points = this.StylusPoints;

            StylusPoint p1 = points[0];
            StylusPoint p2 = points[1];

            context.DrawRectangle(new SolidColorBrush(this.fillColor), new Pen(new SolidColorBrush(this.strokeColor), thickness), new Rect((Point)p1, new Vector(p2.X - p1.X, p2.Y - p1.Y)));
        }
    }
}
