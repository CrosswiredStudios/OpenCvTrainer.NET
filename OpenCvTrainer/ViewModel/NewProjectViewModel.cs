using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCvTrainer.ViewModel
{
    public class NewProjectViewModel
    {
        public IEnumerable<string> DetectionMethods { get; } = new List<string>()
        {
            "YOLO",
            "YOLO v2",
            "YOLO v3"
        };

        public string ProjectName { get; set; }
    }
}
