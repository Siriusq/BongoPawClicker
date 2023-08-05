using System;
using System.CodeDom.Compiler;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BongoPawClicker
{
    /// <summary>
    /// PositionSelector.xaml
    /// </summary>
    public partial class PositionSelector : Window
    {
        //Screen Coords
        private Point mouseDownPoint;
        private Point mouseUpPoint;
        private Point currentPoint;

        private bool randomAreaSelection = false;
        private bool isDragging = false;

        //Selected Area Parameters
        private int x = 0;
        private int y = 0;
        private int w = 0;
        private int h = 0;

        //The window coordinates are offset from the screen coordinates,
        //so using the screen coordinates directly to draw the rectangle will result in an inaccurate rectangle,
        //hence the window coordinates are used instead.
        private Point previewRectangleStartPoint;
        private Point previewRectangleEndPoint;

        public PositionSelector(bool randomAreaEnable)
        {
            InitializeComponent();
            randomAreaSelection = randomAreaEnable;
        }

        private void PositionSelectorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseDownPoint = PointToScreen(e.GetPosition(this));
            x = (int)mouseDownPoint.X;
            y = (int)mouseDownPoint.Y;

            if (randomAreaSelection)
            {
                previewRectangleStartPoint = e.GetPosition(this);
                isDragging = true;
                SelectedAreaPreview.Visibility = Visibility.Visible;
            }
        }

        private void PositionSelectorCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (randomAreaSelection)
            {
                mouseUpPoint = PointToScreen(e.GetPosition(this));
                w = (int)mouseUpPoint.X - x;
                h = (int)mouseUpPoint.Y - y;

                //If the user's selection direction is not from top-left to bottom-right.
                //Convert
                if (w < 0)
                {
                    x += w;
                    w *= -1;
                }
                if (h < 0)
                {
                    y += h;
                    h *= -1;
                }
                isDragging = false;
                SelectedAreaPreview.Visibility = Visibility.Hidden;
            }

            //Update TextBox in main window and close position selector
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.UpdateClickPositionTextBox(x, y, w, h);
                //Make the main window visible again
                mainWindow.Opacity = 1;
                Close();
            }

        }

        private void PositionSelectorCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //Update Realtime Mouse Coordinates Labels
            currentPoint = PointToScreen(e.GetPosition(this));
            XCoordsPreview.Content = (int)currentPoint.X;
            YCoordsPreview.Content = (int)currentPoint.Y;

            //Draw Preview Rectangle
            previewRectangleEndPoint = e.GetPosition(this);
            if (randomAreaSelection && isDragging)
            {
                SelectedAreaPreview.Margin = new Thickness(
                    Math.Min((int)previewRectangleStartPoint.X, (int)previewRectangleEndPoint.X),
                    Math.Min((int)previewRectangleStartPoint.Y, (int)previewRectangleEndPoint.Y),
                    0, 0);
                SelectedAreaPreview.Width = Math.Abs((int)previewRectangleEndPoint.X - (int)previewRectangleStartPoint.X);
                SelectedAreaPreview.Height = Math.Abs((int)previewRectangleEndPoint.Y - (int)previewRectangleStartPoint.Y);
            }
        }

        //Press Esc to close the selector without any point clicked
        private void PositionSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (Owner is MainWindow mainWindow)
                {
                    //Make the main window visible again
                    mainWindow.Opacity = 1;
                }
                Close();
            }
        }
    }
}
