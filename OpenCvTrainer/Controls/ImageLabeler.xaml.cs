using OpenCvTrainer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OpenCvTrainer.Controls
{
    public sealed partial class ImageLabeler : UserControl
    {
        public ImageLabelerViewModel ImageLabelerViewModel { get; }
        public bool IsAdjustingBoundingBox { get; private set; }
        public bool IsCreatingBoundingBox { get; private set; }

        public ImageLabeler()
        {
            ImageLabelerViewModel = new ImageLabelerViewModel();

            InitializeComponent();

            Loaded += OnLoaded;
        }

        void BuildInteractions()
        {
            

            GContainer.PointerEntered += (s, e) => 
            {
                LHorizontal.Visibility = LVertical.Visibility = Visibility.Visible;
            };

            GContainer.PointerExited += (s, e) =>
            {
                LHorizontal.Visibility = LVertical.Visibility = Visibility.Collapsed;
            };

            GContainer.PointerMoved += (s, e) =>
            {
                LHorizontal.Y1 = LHorizontal.Y2 = e.GetCurrentPoint(this).Position.Y;
                LVertical.X1 = LVertical.X2 = e.GetCurrentPoint(this).Position.X;

                if (IsAdjustingBoundingBox || IsCreatingBoundingBox) UpdateBoundingBox(e.GetCurrentPoint(this).Position);
            };
            GContainer.PointerPressed += (s, e) =>
            {
                var pointer = e.GetCurrentPoint(this);

                switch (e.KeyModifiers)
                {
                    case Windows.System.VirtualKeyModifiers.None:
                        break;
                    case Windows.System.VirtualKeyModifiers.Control:
                        break;
                    case Windows.System.VirtualKeyModifiers.Menu:
                        break;
                    case Windows.System.VirtualKeyModifiers.Shift:
                        break;
                    case Windows.System.VirtualKeyModifiers.Windows:
                        break;
                }

                if (pointer.Properties.IsLeftButtonPressed)
                {
                    var results = VisualTreeHelper.FindElementsInHostCoordinates(pointer.Position, GContainer);
                    var selectedBox = results.FirstOrDefault(x => x is BoundingBox);
                    if (selectedBox != null)
                    { }
                    else
                    {
                        var adjustingBox = GContainer.Children.FirstOrDefault(x => x is BoundingBox bb && bb.IsAdjusting) as BoundingBox;

                        if (adjustingBox != null)
                        {
                            IsAdjustingBoundingBox = true;
                            ImageLabelerViewModel.CurrentBoundingBox = adjustingBox;
                        }
                        else
                        {
                            StartCreatingBoundingBox(e.GetCurrentPoint(this).Position);
                        }
                    }
                }
                else if(pointer.Properties.IsRightButtonPressed)
                {
                    
                }
            };
            GContainer.PointerReleased += (s, e) =>
            {
                IsAdjustingBoundingBox = false;
                StopCreatingBoundingBox();
                var boundingBoxes = GContainer.Children.Where(x => x is BoundingBox).Select(x => x as BoundingBox);

                foreach (var bb in boundingBoxes)
                    bb.FinishAdjusting();
            };
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            BuildInteractions();
        }

        void StopCreatingBoundingBox()
        {
            if (!IsCreatingBoundingBox) return;
            IsCreatingBoundingBox = false;
        }

        void StartCreatingBoundingBox(Point startPoint)
        {
            ImageLabelerViewModel.CurrentBoundingBox = new BoundingBox() { Margin = new Thickness(startPoint.X, startPoint.Y, ActualWidth - startPoint.X, ActualHeight - startPoint.Y) };
            GContainer.Children.Add(ImageLabelerViewModel.CurrentBoundingBox);
            IsCreatingBoundingBox = true;
        }

        void UpdateBoundingBox(Point endPoint)
        {
            if (IsAdjustingBoundingBox)
            {
                Thickness newMargins;

                switch (ImageLabelerViewModel.CurrentBoundingBox)
                {
                    case BoundingBox bb when bb.IsAdjustingNE:
                        newMargins = new Thickness(ImageLabelerViewModel.CurrentBoundingBox.Margin.Left, endPoint.Y, ActualWidth - endPoint.X, ImageLabelerViewModel.CurrentBoundingBox.Margin.Bottom);
                        break;
                    case BoundingBox bb when bb.IsAdjustingNW:
                        newMargins = new Thickness(endPoint.X, endPoint.Y, ImageLabelerViewModel.CurrentBoundingBox.Margin.Right, ImageLabelerViewModel.CurrentBoundingBox.Margin.Bottom);
                        break;
                    case BoundingBox bb when bb.IsAdjustingSE:
                        newMargins = new Thickness(ImageLabelerViewModel.CurrentBoundingBox.Margin.Left, ImageLabelerViewModel.CurrentBoundingBox.Margin.Top, ActualWidth - endPoint.X, ActualHeight - endPoint.Y);
                        break;
                    case BoundingBox bb when bb.IsAdjustingSW:
                        newMargins = new Thickness(endPoint.X, ImageLabelerViewModel.CurrentBoundingBox.Margin.Top, ImageLabelerViewModel.CurrentBoundingBox.Margin.Right, ActualHeight - endPoint.Y);
                        break;
                }

                ImageLabelerViewModel.CurrentBoundingBox.Margin = newMargins;
            }

            if (IsCreatingBoundingBox)
                ImageLabelerViewModel.CurrentBoundingBox.Margin = new Thickness(ImageLabelerViewModel.CurrentBoundingBox.Margin.Left, ImageLabelerViewModel.CurrentBoundingBox.Margin.Top, ActualWidth - endPoint.X, ActualHeight - endPoint.Y);
            
        }
    }
}
