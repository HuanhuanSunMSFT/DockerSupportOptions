using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.VisualStudio.Docker.Shared.UI.Scaffolding
{
    public class DockerComposeProject
    {
        public DockerComposeProject(string projectName, bool applyTo = true)
        {
            ProjectName = projectName;
            ApplyTo = applyTo;
        }

        public string ProjectName { get; set; }

        public bool ApplyTo { get; set; }
    }
}
