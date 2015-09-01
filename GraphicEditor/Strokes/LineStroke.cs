using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditor.Strokes
{
    /*
     Класс, который рисует прямые линии с помощью штрихов
     */

    public class LineStroke : CustomStroke
    {
      
        public LineStroke(StylusPointCollection col, Color strokeColor, int strokeThickness)
            : base(col, strokeColor,strokeThickness,new Color())
        {  }

        //Переопределение базового метода DrawCore, который позволяет отрисовать линию штрихом, соединяющим заданные точки
        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {

            StylusPointCollection points = this.StylusPoints;

            StylusPoint p1 = points[0];
            StylusPoint p2 = points[1];

            drawingContext.DrawLine(new Pen(new SolidColorBrush(this.strokeColor), this.thickness), (Point)p1, (Point)p2);

        }
    }
}
