using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace A319TS
{
    static class FileHandler
    {
        static public Project NewProject()
        {
            GUIMenuFileNew fileNew = new GUIMenuFileNew();
            fileNew.ShowDialog();
            if (fileNew.NewProject != null)
                return fileNew.NewProject;
            else return null;
        }
        static public Project OpenProject()
        {
            try
            {
                OpenFileDialog fileOpen = new OpenFileDialog();
                fileOpen.Filter = "TSP Files|*.tsp";
                if (fileOpen.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer reader = new XmlSerializer(typeof(Project));
                    StreamReader file = new StreamReader(fileOpen.FileName);
                    Project project = (Project)reader.Deserialize(file);
                    file.Close();
                    return project;
                }
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        static public void SaveProject(Project project)
        {
            try
            {
                SaveFileDialog fileSave = new SaveFileDialog();
                fileSave.AddExtension = true;
                fileSave.DefaultExt = "tsp";
                fileSave.Filter = "TSP Files|*.tsp";
                if (fileSave.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer writer = new XmlSerializer(typeof(Project));
                    FileStream file = File.Create(fileSave.FileName);
                    writer.Serialize(file, project);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}