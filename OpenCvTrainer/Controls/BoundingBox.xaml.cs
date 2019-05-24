using Microsoft.Toolkit.Uwp.UI.Animations;
using OpenCvTrainer.Extensions;
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
    public sealed partial class BoundingBox : UserControl
    {
        public bool IsAdjustingNE { get; private set; }
        public bool IsAdjustingNW { get; private set; }
        public bool IsAdjustingSE { get; private set; }
        public bool IsAdjustingSW { get; private set; }
        public Point StartPoint { get; set; }

        public bool IsAdjusting => IsAdjustingNE || IsAdjustingNW || IsAdjustingSE || IsAdjustingSW;

        public BoundingBox()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        void BuildInteractions()
        {
            RpContainer.PointerEntered += (s, e) => 
            {
                ENW.Fade(1).Start();
                ENE.Fade(1).Start();
                ESE.Fade(1).Start();
                ESW.Fade(1).Start();
            };
            RpContainer.PointerExited += (s, e) =>
            {
                ENW.Fade(0).Start();
                ENE.Fade(0).Start();
                ESE.Fade(0).Start();
                ESW.Fade(0).Start();
            };
            ENW.CursorOnHover();
            ENW.PointerPressed += (s, e) =>
            {
                IsAdjustingNW = true;
            };
            ENE.CursorOnHover();
            ENE.PointerPressed += (s, e) =>
            {
                IsAdjustingNE = true;
            };
            ESE.CursorOnHover();
            ESE.PointerPressed += (s, e) =>
            {
                IsAdjustingSE = true;
            };
            //ESE.PointerReleased += (s, e) =>
            //{
            //    e.Handled = false;
            //};
            ESW.CursorOnHover();
            ESW.PointerPressed += (s, e) =>
            {
                IsAdjustingSW = true;
            };
            
            //RpContainer.PointerReleased += (s, e) =>
            //{
            //    e.Handled = false;
            //};
        }

        public void FinishAdjusting()
        {
            IsAdjustingNE = false;
            IsAdjustingNW = false;
            IsAdjustingSE = false;
            IsAdjustingSW = false;
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            BuildInteractions();
        }
    }
}
