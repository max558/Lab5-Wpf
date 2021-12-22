using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Ink;

namespace _02___GraphicReader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /*
     * На основе контейнера компоновки InkCanvas разработать графический редактор с функциями рисования мышью и стирания мышью. 
     * В приложении должно присутствовать главное меню с пунктами «Открыть», «Сохранить» и «Выход», а также панель ToolBar с кнопками для выбора режима. 
     * Дизайн приложения и, по желанию, дополнительную функциональность разработать самостоятельно.
     */

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

    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_OpenGraphic(object sender, RoutedEventArgs e)
        {
            Image imageControl;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Графические файлы (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }


            /*
             * Здесь не много не доразобрался может быть особенность Canvas:
             * Картинка открывается, но как подложка, т.е. рисовать на ней можно удалять (только только что нарисованное), 
             * НО вот ее (подгруженную картинку) редактировать не получется.
             */

            try
            {
                //Получение картинки
                BitmapImage image = new BitmapImage(new Uri(openFileDialog.FileName));
                imageControl = new Image();
                imageControl.Source = image;                
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Ошибка чтения картинки.");
                return;
            }
            //Загрузка изображения в Canvas
            if (imageControl!=null)
            {
               graphicRead.Children.Add(imageControl);
                
            }
        }

        private void MenuItem_Click_SaveGraphic(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Графические файлы (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg|Все файлы (*.*)|*.*";
            saveFileDialog.Title = "Сохранить как...";
            if (saveFileDialog.ShowDialog()!=true)
            {
                return;
            }
            var rtb = new RenderTargetBitmap((int)graphicRead.ActualWidth, (int)graphicRead.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            rtb.Render(graphicRead);
            
            PngBitmapEncoder BufferSave = new PngBitmapEncoder();
            BufferSave.Frames.Add(BitmapFrame.Create(rtb));
            BufferSave.Save(File.OpenWrite(saveFileDialog.FileName));
        }

        private void MenuItem_Click_ExitGraphic(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ComboBox_SelectionChanged_Color(object sender, SelectionChangedEventArgs e)
        {
            int idSelColor = (sender as ComboBox).SelectedIndex;
            if (graphicRead == null)
            {
                return;
            }
            PropertyCanvas propertyCanvas = new PropertyCanvas(graphicRead);
            propertyCanvas.ColorDraw(idSelColor);
        }

        private void Button_Click_Small(object sender, RoutedEventArgs e)
        {
            PropertyCanvas propertyCanvas = new PropertyCanvas(graphicRead);
            propertyCanvas.SizePen(2,2);
        }

        private void Button_Click_Medium(object sender, RoutedEventArgs e)
        {
            PropertyCanvas propertyCanvas = new PropertyCanvas(graphicRead);
            propertyCanvas.SizePen(7, 7);
        }

        private void Button_Click_Big(object sender, RoutedEventArgs e)
        {
            PropertyCanvas propertyCanvas = new PropertyCanvas(graphicRead);
            propertyCanvas.SizePen(15, 15);
        }

        private void Button_Click_AllClear(object sender, RoutedEventArgs e)
        {
            PropertyCanvas propertyCanvas = new PropertyCanvas(graphicRead);
            propertyCanvas.AllClear();
        }

        private void Button_Click_Eraser(object sender, RoutedEventArgs e)
        {
            PropertyCanvas propertyCanvas = new PropertyCanvas(graphicRead);
            propertyCanvas.Eraser();
        }

        private void Button_Click_Inc(object sender, RoutedEventArgs e)
        {
            PropertyCanvas propertyCanvas = new PropertyCanvas(graphicRead);
            propertyCanvas.Pincil();
        }
    }
}
