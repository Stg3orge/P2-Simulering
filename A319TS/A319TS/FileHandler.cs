using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

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
            FileStream file = null;
            try
            {
                OpenFileDialog fileOpen = new OpenFileDialog();
                fileOpen.Filter = "TSP Files|*.tsp";
                if (fileOpen.ShowDialog() == DialogResult.OK)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    file = new FileStream(fileOpen.FileName, FileMode.Open);
                    Project project = (Project)formatter.Deserialize(file);
                    return project;
                }
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return null;
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }
        static public void SaveProject(Project project)
        { 
            FileStream file = null;
            try
            {
                SaveFileDialog fileSave = new SaveFileDialog();
                fileSave.AddExtension = true;
                fileSave.DefaultExt = "tsp";
                fileSave.Filter = "TSP Files|*.tsp";
                if (fileSave.ShowDialog() == DialogResult.OK)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    file = new FileStream(fileSave.FileName, FileMode.Create);
                    formatter.Serialize(file, project);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }
        static public void SaveSimulation(SimulationData data)
        {
            FileStream file = null;
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + data.Filename;
                BinaryFormatter formatter = new BinaryFormatter();
                file = new FileStream(path, FileMode.Create);
                formatter.Serialize(file, data); 
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }
    }
}