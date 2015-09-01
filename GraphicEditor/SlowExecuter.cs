using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GraphicEditor
{
    //Класс, отвечающий за выполнение операций в фоновом потоке
    public class SlowExecuter
    {
        DispatcherTimer disp;
        object obj;
        ProgressBar statusBar;
        EventHandler method;

        public SlowExecuter(object sender, ProgressBar statusBar, EventHandler myMethod)
        {
            this.obj = sender;
            this.statusBar = statusBar;
            this.method = myMethod;
        }

        //Инициализация DispatcherTimer и подписка на событие Tick
        public void BeginExecute()
        {
            disp = new DispatcherTimer();
            disp.Tick += EndExecute;
            disp.Interval = new TimeSpan(10000000L);
            disp.Start();
        }

        //Метод, который выполняет операцию в фоновом потоке
        void EndExecute(object sender, EventArgs e)
        {
            statusBar.Value++;
            if (statusBar.Value == statusBar.Maximum)
            {
                method(obj, null);
                disp.Stop();
                disp.Tick -= EndExecute;
                statusBar.Value = 0;
                statusBar = null;
                method = null;
                sender = null;
                disp = null;
            }
        }
    }
}
