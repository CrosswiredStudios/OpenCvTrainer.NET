using OpenCvTrainer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class MediaCollection : UserControl
    {
        MediaCollectionViewModel MediaCollectionViewModel { get; }

        public MediaCollection()
        {
            MediaCollectionViewModel = new MediaCollectionViewModel();

            InitializeComponent();

            Loaded += OnLoaded;
        }

        void BuildInteractions()
        {
            GContainer.DragOver += async (s, e) =>
            {
                var deferral = e.GetDeferral();

                try
                {
                    var dataView = e.DataView;
                    if (dataView.Contains(StandardDataFormats.StorageItems))
                    {
                        var items = await dataView.GetStorageItemsAsync();

                        foreach (var item in items)
                        {
                            switch (item)
                            {
                                case StorageFile file:
                                    switch (file.ContentType)
                                    {
                                        case "image/jpeg":
                                        case "image/jpg":
                                        case "image/png":
                                        case "video/mp4":
                                            continue;
                                        // If at least one file type is unsupported, do not allow any of them
                                        default:
                                            e.AcceptedOperation = DataPackageOperation.None;
                                            deferral.Complete();
                                            return;
                                    }
                                case StorageFolder folder:
                                    // TODO: Add folder drop support
                                    return;
                            }

                        }

                        e.AcceptedOperation = DataPackageOperation.Copy;
                        deferral.Complete();
                        return;
                    }

                    e.AcceptedOperation = DataPackageOperation.None;
                    deferral.Complete();
                }
                catch
                {
                    e.AcceptedOperation = DataPackageOperation.None;
                    deferral.Complete();
                }
            };

            GContainer.Drop += async (s, e) =>
            {
                try
                {
                    if (e.DataView.Contains(StandardDataFormats.StorageItems))
                    {
                        var items = (await e.DataView.GetStorageItemsAsync()).Cast<StorageFile>();

                        if (items.Count() > 0)
                        {
                            foreach (var item in items)
                            {
                                try
                                {
                                    switch (item.ContentType)
                                    {
                                        case "image/jpeg":
                                        case "image/jpg":
                                        case "image/png":
                                            // Handle Images
                                            break;
                                        case "video/mp4":
                                            // Handle Videos
                                            break;
                                    }
                                }
                                catch { }
                            }
                        }

                    }
                }
                catch
                {

                }
            };
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            BuildInteractions();
        }
    }
}
