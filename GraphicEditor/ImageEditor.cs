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
using System.Windows.Threading;

namespace GraphicEditor
{


    public class ImageEditor
    {
        private enum RotateIteration { None, First, Second, Third }

        private InkCanvas surface;
        private RotateIteration rotationState;
        TransformGroup transGroup;

        public ImageEditor(InkCanvas surface, ProgressBar progressBar)
        {
            this.surface = surface;
            this.rotationState = RotateIteration.None;
        }

        public void InvertColors(object sender, EventArgs e)
        {
            Color scb = ((SolidColorBrush)surface.Background).Color;
            surface.Background = new SolidColorBrush(Color.FromArgb(scb.A, (byte)~scb.R, (byte)~scb.G, (byte)~scb.B));
            for (int i = 0; i < surface.Strokes.Count; i++)
            {
                Color c = surface.Strokes[i].DrawingAttributes.Color;
                surface.Strokes[i].DrawingAttributes.Color = Color.FromArgb(c.A, (byte)~c.R, (byte)~c.G, (byte)~c.B);
            }
        }

        public void MirrorReflection(object sender, EventArgs e)
        {
            double val;
            bool horizontally = false;


            if ((sender as MenuItem).Name == "horizontal")
            {
                val = surface.ActualHeight;
                horizontally = true;
            }
            else
            {
                val = surface.ActualWidth;
            }

            for (int i = 0; i < surface.Strokes.Count; i++)
            {
                DrawingAttributes dr = surface.Strokes[i].DrawingAttributes;
                StylusPointCollection newPoints = new StylusPointCollection();
                for (int j = 0; j < surface.Strokes[i].StylusPoints.Count; j++)
                {
                    StylusPoint p = surface.Strokes[i].StylusPoints[j];
                    if (horizontally)
                    {
                        p.Y = val - p.Y;
                    }
                    else
                    {
                        p.X = val - p.X;
                    }
                    newPoints.Add(p);
                }
                surface.Strokes[i].StylusPoints = newPoints;
            }
        }

        public void RotationTransform(object sender, EventArgs e)
        {
            bool is90 = (sender as MenuItem).Name == "rotation90";

            double rotationAngle = is90 ? 90d : 180d;

            double centerX = surface.ActualWidth / 2;
            double centerY = surface.ActualHeight / 2;
            double scaleVertical = surface.ActualHeight / surface.ActualWidth;
            double scaleHorizontal = surface.ActualWidth / surface.ActualHeight;          
            
            
            switch (rotationState)
            {
                case RotateIteration.None:
                    transGroup = new TransformGroup();
                    surface.RenderTransform = transGroup;
                    if (is90)
                    {
                        transGroup.Children.Add(new ScaleTransform(scaleVertical, scaleVertical, centerX, centerY));
                        rotationState++;
                    }
                    else
                    {
                        rotationState = RotateIteration.Second;
                    }
                    break;
                case RotateIteration.First:                    
                    if (is90)
                    {
                        transGroup.Children.Add(new ScaleTransform(scaleHorizontal, scaleHorizontal, centerX, centerY));
                        rotationState++;
                    }
                    else
                    {
                        rotationState = RotateIteration.Third;
                    }                 
                    break;
                case RotateIteration.Second:
                    if (is90)
                    {
                        transGroup.Children.Add(new ScaleTransform(scaleVertical, scaleVertical, centerX, centerY));
                        rotationState++;
                    }
                    else
                    {
                        rotationState = RotateIteration.None;
                        surface.RenderTransform = null;
                    }
                    break;
                case RotateIteration.Third:
                    if (is90)
                    {
                        transGroup.Children.Add(new ScaleTransform(scaleHorizontal, scaleHorizontal, centerX, centerY));
                        rotationState = RotateIteration.None;
                        surface.RenderTransform = null;
                    }
                    else
                    {
                        rotationState  = RotateIteration.First;
                    } 
                    break;
            }
            transGroup.Children.Add(new RotateTransform(rotationAngle, centerX, centerY));
        }

    }
}
