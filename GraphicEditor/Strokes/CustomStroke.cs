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
     Класс, наследник класса Stroke, который добавляет поля толщина штриха, цвет штриха, цвет заливки.
     */
    public abstract class CustomStroke : Stroke
    {
        protected Color strokeColor;
        protected int thickness;
        protected Color fillColor;

        public CustomStroke(StylusPointCollection col, Color strokeColor, int thickness, Color fill)
            : base(col)
        {
            this.strokeColor = strokeColor;
            this.thickness = thickness;
            this.fillColor = fill;
        }
    }
}
