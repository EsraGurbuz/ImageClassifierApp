using System;
using System.Drawing;
using System.Windows.Forms;
using ImageClassifierApp.Services;

namespace ImageClassifierApp.UI
{
    public partial class MainForm : Form
    {
        private string _selectedImagePath;

        public MainForm()
        {
            InitializeComponent();
        }

        // When the "Select Image" button is clicked (Event-Driven)
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Resim Dosyalarý (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = openFileDialog.FileName;
                    picSelectedImage.Image = Image.FromFile(_selectedImagePath);
                }
            }
        }

        // When the "Classify" button is clicked
        private void btnClassify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedImagePath))
            {
                MessageBox.Show("Please select an image first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // SINGLETON USAGE: We don't use 'new', we call the single instance.
                var classifier = ImageClassifierService.Instance;

                var result = classifier.ClassifyImage(_selectedImagePath);

                lblResult.Text = $"Prediction: {result.PredictedLabel}";
                lblResult.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}