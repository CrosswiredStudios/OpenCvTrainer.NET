using OpenCvTrainer.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCvTrainer.ViewModel
{
    public class BoundsCollectionViewModel : ViewModelBase
    {
        public ObservableCollection<BoundingBox> Items { get; }

        public BoundsCollectionViewModel()
        {
            Items = new ObservableCollection<BoundingBox>();
        }
    }
}
