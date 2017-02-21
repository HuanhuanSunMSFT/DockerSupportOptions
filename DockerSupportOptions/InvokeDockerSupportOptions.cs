//------------------------------------------------------------------------------
// <copyright file="InvokeDockerSupportOptions.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Docker.Shared.UI.Scaffolding;
using System.Windows;
using Microsoft.VisualStudio.Docker.Shared.UI;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.VisualStudio.Settings;

namespace DockerSupportOptions
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class InvokeDockerSupportOptions
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("5d07901b-65cc-453b-a68d-37df09652c81");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeDockerSupportOptions"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private InvokeDockerSupportOptions(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static InvokeDockerSupportOptions Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new InvokeDockerSupportOptions(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var projects = GenerateDockerComposeProjects(ReadCountFromFile());

            var setting = GetSettingsStore(TargetOS.NanoServer, projects);

            var model = new DockerScaffoldingModel(setting.LastSelectedTargetOS.Value, setting.LastSelectedComposeProjects);

            var viewModel = new DockerScaffoldingViewModel(model);
            var dialog = new DockerScaffoldingDialog(viewModel);
            if (dialog.ShowDialog() == true)
            {
                var selectedDockerComposeProjects = viewModel.AvailableDockerComposeProjects.Where(p => p.ApplyTo).Select(p => p.ProjectName).ToArray();
                string displayProjects = string.Empty;
                foreach(var p in selectedDockerComposeProjects)
                {
                    displayProjects += p + Environment.NewLine;
                }

                SaveSettings(viewModel.SelectedTargetOS, selectedDockerComposeProjects);
                
                MessageBox.Show("Window is closed: " + viewModel.SelectedTargetOS.ToString() + Environment.NewLine + displayProjects);
            }
        }
        
        private string[] GenerateDockerComposeProjects(int count)
        {
            var projects = new List<string>();
            for(int i=0; i<count; i++)
            {
                projects.Add($"dcproj{i}");
            }
            return projects.ToArray();
        }

        private int ReadCountFromFile()
        {
            var countString = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "count.txt"));
            int count;
            if (!int.TryParse(countString, out count))
            {
                count = new Random().Next(0, 20);
            }
            return count;
        }

        private const string DockerToolsCollectionPath = "DockerTools";
        private const string DockerToolsTargetOSProperty = "TargetOS";
        private const string DockerToolsAvailableComposeProjectsProperty = "ComposeProjects";
        private const string DockerToolsSelectedComposeProjectsProperty = "SelectedComposeProjects";

        private Settings GetSettingsStore(TargetOS? defaultTargetOS, string[] selectedProjects)
        {
            var settingsManager = new ShellSettingsManager(ServiceProvider);
            var settingsStore = settingsManager.GetReadOnlySettingsStore(SettingsScope.UserSettings);
            if (settingsStore.CollectionExists(DockerToolsCollectionPath))
            {
                var targetOSString = settingsStore.GetString(DockerToolsCollectionPath, DockerToolsTargetOSProperty);
                var availableProjectsString = settingsStore.GetString(DockerToolsCollectionPath, DockerToolsAvailableComposeProjectsProperty);

                if (!string.IsNullOrEmpty(targetOSString))
                {
                    defaultTargetOS = Enum.Parse(typeof(TargetOS), targetOSString) as TargetOS?;
                }

                if (!string.IsNullOrEmpty(availableProjectsString))
                {
                    selectedProjects = availableProjectsString.Split('&');
                }
            }
            

            return new Settings(defaultTargetOS, selectedProjects);
        }

        private void SaveSettings(TargetOS targetOS, string[] selectedProjects)
        {
            var settingsManager = new ShellSettingsManager(ServiceProvider);
            var settingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
            if (!settingsStore.CollectionExists(DockerToolsCollectionPath))
            {
                settingsStore.CreateCollection(DockerToolsCollectionPath);
            }

            settingsStore.SetString(DockerToolsCollectionPath, DockerToolsTargetOSProperty, targetOS.ToString());
            settingsStore.SetString(DockerToolsCollectionPath, DockerToolsAvailableComposeProjectsProperty, string.Join("&", selectedProjects));
        }

        private class Settings
        {
            public Settings(TargetOS? targetOS, string[] selectedProjects)
            {
                LastSelectedTargetOS = targetOS;
                LastSelectedComposeProjects = selectedProjects;
            }

            public TargetOS? LastSelectedTargetOS { get; set; }

            public string[] LastSelectedComposeProjects { get; set; }
        }
    }
}
