using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MIDI_Player
{
    class Touche
    {
        private double x;
        private double y;
        private Color color;
        private bool isActived;

        public Touche(double x, double y, Color color, bool isActived=false)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.isActived = isActived;

            draw();
        }

        public bool actived()
        {
            return this.isActived;
        }

        public void setActived(bool actived)
        {
            this.isActived = actived;
        }

        private void draw()
        {
            /*Line myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = 1;
            myLine.X2 = 50;
            myLine.Y1 = 1;
            myLine.Y2 = 50;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            gameArea.Children.Add(myLine);*/
        }
    }
}
