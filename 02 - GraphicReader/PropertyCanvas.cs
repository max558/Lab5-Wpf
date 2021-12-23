using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace _02___GraficReader
{
    public class PropertyCanvas
    {
        InkCanvas inkCanvas;
        public PropertyCanvas(InkCanvas inc)
        {
            inkCanvas = inc;
        }
        public void ColorDraw(int iDcolor)
        {
            switch (iDcolor)
            {
                case 1:
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
                    break;
                case 2:
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Blue;
                    break;
                case 3:
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Yellow;
                    break;

                default:
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
                    break;
            }
        }
        public void SizePen(double heigtPen, double widthPen)
        {
            inkCanvas.DefaultDrawingAttributes.Width = widthPen;
            inkCanvas.DefaultDrawingAttributes.Height = heigtPen;
        }
        public void AllClear()
        {
            inkCanvas.Strokes.Clear();
        }
        public void Eraser()
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }
        public void Pincil()
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

    }
}
