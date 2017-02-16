using Microsoft.VisualStudio.Docker.Shared.UI.Scaffolding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.VisualStudio.Docker.Shared.UI
{
    public class DockerScaffoldingModel
    {
        public DockerScaffoldingModel(TargetOS defaultTargetOS, string[] dockerComposeProjects)
        {
            DefaultTargetOS = defaultTargetOS;
            DockerComposeProjects = new List<DockerComposeProject>();
            foreach(var p in dockerComposeProjects)
            {
                DockerComposeProjects.Add(new DockerComposeProject(p));
            }
        }

        public TargetOS DefaultTargetOS { get; set; }

        public IList<DockerComposeProject> DockerComposeProjects { get; set; }
    }
}
