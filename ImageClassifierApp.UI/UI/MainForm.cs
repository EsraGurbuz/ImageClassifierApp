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
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

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

        private async void btnTrain_Click(object sender, EventArgs e)
        {
            btnTrain.Enabled = false; // We prevent the button from being pressed again when the training starts.
            lblResult.Text = "Model is training... Please wait (UI will not freeze).";
            lblResult.ForeColor = Color.Orange;

            try
            {
                // Write the full path of your own train_data.txt file here
                string trainDataPath = @"C:\Users\Esra\source\repos\ImageClassifierApp\ImageClassifierApp.UI\MyDataset\train_data.txt";
                string testDataPath = @"C:\Users\Esra\source\repos\ImageClassifierApp\ImageClassifierApp.UI\MyDataset\test_data.txt";

                var classifier = ImageClassifierService.Instance;

                // ASYNCHRONOUS CALL: The UI will not freeze while training
                await classifier.TrainModelAsync(trainDataPath);

                // As soon as the model is trained, we measure the accuracy with the test data
                double accuracy = classifier.EvaluateModel(testDataPath);

                lblResult.Text = $"Training Completed! Accuracy: %{accuracy * 100:F2}";
                lblResult.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during training: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblResult.Text = "Training failed.";
                lblResult.ForeColor = Color.Red;
            }
            finally
            {
                btnTrain.Enabled = true; // We re-enable the button after the operation is complete
            }
        }
    }
}