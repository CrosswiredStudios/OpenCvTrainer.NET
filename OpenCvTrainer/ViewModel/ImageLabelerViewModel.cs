using OpenCvTrainer.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OpenCvTrainer.ViewModel
{
    public class ImageLabelerViewModel : ViewModelBase
    {
        ImageSource imageSource;

        public ObservableCollection<BoundingBox> BoundingBoxes { get; }
        public BoundingBox CurrentBoundingBox { get; set; }
        public ImageSource ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                RaisePropertyChanged();
            }
        }

        public ImageLabelerViewModel()
        {
            BoundingBoxes = new ObservableCollection<BoundingBox>();
            ImageSource = new BitmapImage(new Uri("ms-appx:///Resources/1.jpg"));
        }
    }
}
