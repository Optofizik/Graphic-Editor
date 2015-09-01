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
     Класс, который позволяет рисовать круги с помощью штрихов
     */
    public class CircleStroke : CustomStroke
    {
        double xRadius, yRaduis;

        public CircleStroke(StylusPointCollection col, Color strokeColor, int strokeThickness, Color fillColor, double width, double height)
            : base(col, strokeColor, strokeThickness, fillColor)
        {
            this.xRadius = width;
            this.yRaduis = height;
        }

        //Переопределение базового метода, который позволяет рисовать круг, имея координаты центра и радиус
        protected override void DrawCore(DrawingContext context, DrawingAttributes overrides)
        {
            StylusPointCollection points = this.GetBezierStylusPoints(); 

            StylusPoint p1 = points[0];
            context.DrawEllipse(new SolidColorBrush(fillColor), new Pen(new SolidColorBrush(strokeColor), thickness), new Point(p1.X, p1.Y), xRadius, yRaduis);

        }
    }
}
