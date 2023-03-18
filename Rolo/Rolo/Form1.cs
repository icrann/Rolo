using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Rolo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the path to the latest version of Roblox
            string robloxPath = GetLatestRobloxPath();

            // Create a new folder called "ClientSettings" in the Roblox directory
            string clientSettingsPath = Path.Combine(robloxPath, "ClientSettings");
            Directory.CreateDirectory(clientSettingsPath);

            // Create a new ClientAppSettings object and set its DFIntTaskSchedulerTargetFps property
            ClientAppSettings clientSettings = new ClientAppSettings();
            clientSettings.DFIntTaskSchedulerTargetFps = int.Parse(comboBox1.SelectedItem.ToString());

            // Serialize the ClientAppSettings object to JSON and write it to a file
            string json = JsonConvert.SerializeObject(clientSettings, Formatting.Indented);
            File.WriteAllText(Path.Combine(clientSettingsPath, "ClientAppSettings.json"), json);

            MessageBox.Show("Roblox unlocked to selected FPS.");
        }

        private string GetLatestRobloxPath()
        {
            string robloxVersionsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roblox\\Versions");

            // Get the latest directory in the Roblox versions folder
            DirectoryInfo latestDirectory = new DirectoryInfo(robloxVersionsPath).GetDirectories().OrderByDescending(d => d.CreationTime).FirstOrDefault();

            if (latestDirectory != null)
            {
                return latestDirectory.FullName;
            }
            else
            {
                MessageBox.Show("Unable to locate Roblox installation directory.");
                throw new Exception("Unable to locate Roblox installation directory.");
            }
        }
    }

    public class ClientAppSettings
    {
        public int DFIntTaskSchedulerTargetFps { get; set; }
    }
}
