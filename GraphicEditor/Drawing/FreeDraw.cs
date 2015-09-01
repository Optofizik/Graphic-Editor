using GraphicEditor.Strokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.Drawing
{
    /*
       Класс, наследник класса Draw, используется для свободного рисования, используя стандартный инструмент InkCanvas
     */
    public class FreeDraw : Draw
    {
        public FreeDraw(InkCanvas surface)
            : base(surface)
        {
            this.Surface.EditingMode = InkCanvasEditingMode.Ink;
            this.Surface.PreviewMouseLeftButtonDown += StartDrawing;
        }

        //Метод, отвечающий за свободное рисование
        public override void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            this.strokeColor = (Application.Current.MainWindow as MainWindow).penColor.SelectedColor.Value;
            this.strokeThickness = (int)(Application.Current.MainWindow as MainWindow).thicknessSlider.Value;
            this.Surface.DefaultDrawingAttributes = new DrawingAttributes { Color = this.strokeColor, Height = this.strokeThickness, Width = this.strokeThickness };
        }

        public override void Drawing(object sender, MouseEventArgs e)
        {
        }

        public override void EndDrawing(object sender, MouseButtonEventArgs e)
        {
            
        }


        //К функционалу базового метода добавлена отписка от события.
        public override void DropSurface()
        {
            this.Surface.PreviewMouseLeftButtonDown -= StartDrawing;
            base.DropSurface();
        }

    }
}
