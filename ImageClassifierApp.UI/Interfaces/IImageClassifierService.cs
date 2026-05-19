using System.Threading.Tasks;
using ImageClassifierApp.Models;

namespace ImageClassifierApp.Interfaces
{
    public interface IImageClassifierService
    {
        // Method to use the model

        Task TrainModelAsync(string trainDataPath);

        // Method to classify a single image
        ModelOutput ClassifyImage(string imagePath);

        // Method to evaluate the model's performance (metrics)
        double EvaluateModel(string testDataPath);
    }
}