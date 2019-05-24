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
    public sealed partial class MediaCollectionImage : UserControl
    {
        public MediaCollectionImage()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        void BuildInteractions()
        {
            FiDelete.CursorOnHover();
            FiDelete.LiftOnHover(1.1f,1.1f,200);
           
            GContainer.PointerEntered += (s, e) =>
            {
                FiDelete.Fade(1, 333).Start();
            };

            GContainer.PointerExited += (s, e) =>
            {
                FiDelete.Fade(0, 333).Start();
            };
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            BuildInteractions();
        }
    }
}
