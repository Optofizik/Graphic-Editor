using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.Drawing
{
    /* Абстрактный класс, который отвечает за рисование элементов на поверхности InkCanvas
       Принцип рисования: Сначала на InkСanvas с помощью объектов Shape определяется размер и положение фигуры. 
       По конечным точкам рисуются штрихи, а объект Shape удаляется из InkCanvas     */
    public abstract class Draw
    {
        //Поверхность для рисования
        protected InkCanvas Surface;
        //Текущий статус
        protected bool isDrawing;
        //Цвет штриха
        protected Color strokeColor;
        //Цвет заливки
        protected Color fillColor;
        //Толщина штрихов
        protected int strokeThickness;

        //В конструкторе осуществляется настройка параметров InkCanvas
        public Draw(InkCanvas surface)
        {
            this.Surface = surface;
            Surface.EditingMode = InkCanvasEditingMode.None;
            Surface.UseCustomCursor = true;
            Surface.Cursor = Cursors.Arrow;
        }

        //Виртуальный метод, базовая реализация которого отвечает за инициализацию переменных (цвет штриха, толщина штриха)
        public virtual void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            this.strokeColor = (Application.Current.MainWindow as MainWindow).penColor.SelectedColor.Value;
            this.strokeThickness = (int)(Application.Current.MainWindow as MainWindow).thicknessSlider.Value;
            e.Handled = true;
            isDrawing = true;
        }


        //Абстрактные методы, предназначенные для рисования и отрисовки штрихов
        public abstract void Drawing(object sender, MouseEventArgs e);
        public abstract void EndDrawing(object sender, MouseButtonEventArgs e);

        //Виртуальный метод, базовая реализация которого отвечает за удаление ссылки на InkCanvas
        public virtual void DropSurface()
        {
            this.Surface = null;
        }
    }
}
