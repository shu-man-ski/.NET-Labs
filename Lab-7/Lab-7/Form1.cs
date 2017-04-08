﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_7
{
    public partial class Form1 : Form
    {
        Animal animal;
        Zoo zoo;
        public Form1()
        {
            InitializeComponent();

            animalType.Items.Add("Amphibians");
            animalType.Items.Add("Birds");
            animalType.Items.Add("Fish");
            animalType.Items.Add("Mammals");
            animalType.Items.Add("Reptiles");

            habitatContinent.Items.Add("Africa");
            habitatContinent.Items.Add("Antarctida");
            habitatContinent.Items.Add("Asia");
            habitatContinent.Items.Add("Australia");
            habitatContinent.Items.Add("Europe");
            habitatContinent.Items.Add("North America");
            habitatContinent.Items.Add("South America");

            animalDateOfReceipt.Format = DateTimePickerFormat.Short;
            
            zoo = new Zoo();

            InitFirstRow();
        }

        private void InitFirstRow()
        {
            for (int x = 0; x < 9; x++)
            {
                DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                Column.Width = 95;
                dataGridView1.Columns.Add(Column);
            }
            dataGridView1.Rows.Add("Name", "Type", "Age", "Record on Red Book", "Date of receipt",
                                    "Habitat continent", "Habitat latitude", "Habitat longitude", "Description");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckErrors())
            {
                try
                {
                    animal = new Animal();
                    animal.Name = animalName.Text.ToString();
                    animal.Type = animalType.Text.ToString();
                    animal.Age = (int)animalAge.Value;
                    if (animalIsRedBookYes.Checked)
                        animal.IsRcordOnRedBook = true;
                    else
                        animal.IsRcordOnRedBook = false;
                    animal.DateOfReceipt = animalDateOfReceipt.Text.ToString();
                    animal.Habitat = new Habitat();
                    animal.Habitat.Continent = habitatContinent.Text.ToString();
                    animal.Habitat.Latitude = habitatLatitude.Text.ToString();
                    animal.Habitat.Longitude = habitatLongitude.Text.ToString();
                    animal.Description = description.Text.ToString();
                    zoo.Add(animal);
                    zoo.SaveToXML();
                }
                catch (Exception) { }
            }
        }
        private bool CheckErrors()
        {
            try
            {
                if (animalName.Text == "")
                    error.SetError(animalName, "Enter name");
                else if (animalName.Text.Length < 3)
                    error.SetError(animalName, "Too short the name");
                else
                    error.SetError(animalName, "");

                if (animalType.Items.IndexOf(animalType.Text) < 0)
                    error.SetError(animalType, "Select from list");
                else
                    error.SetError(animalType, "");

                if (animalAge.Value == 0)
                    error.SetError(animalAge, "Enter age");
                else
                    error.SetError(animalAge, "");

                if (!animalIsRedBookYes.Checked && !animalIsRedBookNo.Checked)
                {
                    error.SetError(animalIsRedBookYes, "Select one of the options");
                    error.SetError(animalIsRedBookNo, "Select one of the options");
                }
                else
                {
                    error.SetError(animalIsRedBookYes, "");
                    error.SetError(animalIsRedBookNo, "");
                }

                if (habitatContinent.Items.IndexOf(habitatContinent.Text) < 0)
                    error.SetError(habitatContinent, "Select from list");
                else
                    error.SetError(habitatContinent, "");
            }
            catch (Exception) { }

            if (error.GetError(animalName) != "Enter name" && error.GetError(animalName) != "Too short the name"
                        && error.GetError(animalType) != "Select from list"
                        && error.GetError(animalAge) != "Enter age"
                        && error.GetError(animalIsRedBookYes) != "Select one of the options" && error.GetError(animalIsRedBookNo) != "Select one of the options"
                        && error.GetError(habitatContinent) != "Select from list")
                return false;
            else
                return true;
        }
        private void buttonToRestore_Click(object sender, EventArgs e)
        {
            zoo.LoadFromXML();
        }
        public void AddRowOnDataGrid()
        {
            dataGridView1.Rows.Add(animal.Name.ToString(), animal.Type.ToString(), animal.Age.ToString(),
                    animal.IsRcordOnRedBook.ToString(), animal.DateOfReceipt.ToString(),
                    animal.Habitat.Continent.ToString(), animal.Habitat.Latitude.ToString(), animal.Habitat.Longitude.ToString(),
                    animal.Description.ToString());
        }
    }
}
