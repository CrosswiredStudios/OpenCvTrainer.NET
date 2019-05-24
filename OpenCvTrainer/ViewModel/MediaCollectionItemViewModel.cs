using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OpenCvTrainer.ViewModel
{
    public class MediaCollectionItemViewModel
    {
        public string FileName { get; }
        public ImageSource Thumbnail { get; }

        public MediaCollectionItemViewModel()
        {
            FileName = "TestFile.jpg";
            Thumbnail = new BitmapImage(new Uri("ms-appx:///Resources/1.jpg"));
        }
    }
}
