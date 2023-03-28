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
using System.Xml;

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

            // Check if an item has been selected in the comboBox1 control
            if (comboBox1.SelectedItem != null)
            {
                clientSettings.DFIntTaskSchedulerTargetFps = int.Parse(comboBox1.SelectedItem.ToString());
            }
            else
            {
                // Display an error message if no item has been selected
                MessageBox.Show("Please select a value from the drop-down list.");
                return;
            }

            // Serialize the ClientAppSettings object to JSON and write it to a file
            string json = JsonConvert.SerializeObject(clientSettings, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Path.Combine(clientSettingsPath, "ClientAppSettings.json"), json);

            MessageBox.Show("Roblox FPS cap unlocked.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the path to the latest version of Roblox
            string robloxPath = GetLatestRobloxPath();

            // Delete the "ClientSettings" folder and its contents
            string clientSettingsPath = Path.Combine(robloxPath, "ClientSettings");
            if (Directory.Exists(clientSettingsPath))
            {
                Directory.Delete(clientSettingsPath, true);
                MessageBox.Show("FPS unlocker disabled successfully.");
            }
            else
            {
                MessageBox.Show("FPS unlocker is already disabled.");
            }
        }
    }

    public class ClientAppSettings
    {
        public int DFIntTaskSchedulerTargetFps { get; set; }
    }
}