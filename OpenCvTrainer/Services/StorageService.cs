using Newtonsoft.Json;
using OpenCvTrainer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace OpenCvTrainer.Services
{
    public class StorageService
    {
        public string AppFolderPath => ApplicationData.Current.LocalFolder.Path;
        public string ProjectFilePath(Project project) => Path.Combine(ProjectFolderPath(project), $"{project.Name}.cvt");
        public string ProjectFolderPath(Project project) => Path.Combine(AppFolderPath, project.Name);

        public Project LoadProject(string projectFilePath)
        {
            return JsonConvert.DeserializeObject<Project>(File.ReadAllText(projectFilePath));
        }

        public void SaveProject(Project project)
        {
            // Generate project directory path
            var projectDirectory = ProjectFolderPath(project);

            // If no directory exists for this project
            if (!Directory.Exists(projectDirectory))
                // Create it
                Directory.CreateDirectory(projectDirectory);

            // Write the project info to file
            File.WriteAllText(ProjectFilePath(project), JsonConvert.SerializeObject(project));
        }
    }
}
