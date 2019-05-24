using OpenCvTrainer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCvTrainer
{
    public class OpenCvTrainer
    {
        public static OpenCvTrainer Instance { get; } = new OpenCvTrainer();

        public StorageService Storage { get; }

        OpenCvTrainer()
        {
            Storage = new StorageService();
        }
    }
}
