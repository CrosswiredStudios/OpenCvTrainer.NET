using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace OpenCvTrainer.ViewModel
{
    public class MediaCollectionViewModel
    {
        public ObservableCollection<MediaCollectionItemViewModel> Media { get; }

        public MediaCollectionViewModel()
        {
            Media = new ObservableCollection<MediaCollectionItemViewModel>();

            Media.Add(new MediaCollectionItemViewModel());
        }
    }
}
