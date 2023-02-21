using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using Newtonsoft;
using System.Text.RegularExpressions;

namespace MIS3033Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Select a text file";

            if (openFileDialog.ShowDialog() == true)
            {
                InputFileTextBox.Text = openFileDialog.FileName;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select the input file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                string inputFilePath = openFileDialog.FileName;

                // Read the input file and clean the data
                string inputData = File.ReadAllText(inputFilePath);
                inputData = Regex.Replace(inputData, @"\t|\n|\r", " "); // replace tabs, newlines and carriage returns with spaces
                inputData = Regex.Replace(inputData, @"\s+", " "); // replace multiple spaces with a single space
                inputData = inputData.Trim(); // remove leading and trailing spaces

                // Open a save file dialog to select the output file format and location
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string outputFilePath = saveFileDialog.FileName;

                    // Write the cleaned data to the output file
                    File.WriteAllText(outputFilePath, inputData);
                }
            }
        }
    }
}
