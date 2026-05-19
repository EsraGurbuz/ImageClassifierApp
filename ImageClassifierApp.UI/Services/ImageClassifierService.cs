using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ML;
using ImageClassifierApp.Interfaces;
using ImageClassifierApp.Models;

namespace ImageClassifierApp.Services
{
    public class ImageClassifierService : IImageClassifierService
    {
        private readonly MLContext _mlContext;
        private ITransformer _trainedModel;
        private PredictionEngine<ModelInput, ModelOutput> _predictionEngine;

        public ImageClassifierService()
        {
            //MLContext is the heart of ML.NET operations.
            // By providing a seed value, we ensure that the results are deterministic (consistent) across runs.
            _mlContext = new MLContext(seed: 1);
        }

        public async Task TrainModelAsync(string trainDataPath)
        {
            // To prevent CPU/GPU blocking and adhere to asynchronous programming principles, we use Task.
            await Task.Run(() =>
            {
                // 1. Data Loading
                // The trainDataPath will contain a text/csv file with image paths and labels.
                IDataView dataView = _mlContext.Data.LoadFromTextFile<ModelInput>(
                    path: trainDataPath,
                    hasHeader: false,
                    separatorChar: ',');

                // 2. Data Preprocessing Pipeline
                // We read the image paths, load the images into memory, and resize them to dimensions that ML.NET can understand (e.g., 224x224).
                var pipeline = _mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: null, inputColumnName: nameof(ModelInput.ImagePath))
                    .Append(_mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: 224, imageHeight: 224, inputColumnName: "input"))
                    .Append(_mlContext.Transforms.ExtractPixels(outputColumnName: "input", inputColumnName: "input"))
                    .Append(_mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey", inputColumnName: nameof(ModelInput.Label)))
                    // Image Classification Algorithm (Deep Learning architecture)
                    .Append(_mlContext.MulticlassClassification.Trainers.ImageClassification(
                        featureColumnName: "input",
                        labelColumnName: "LabelKey"))
                    .Append(_mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: nameof(ModelOutput.PredictedLabel), inputColumnName: "PredictedLabel"));

                // 3. Model Training
                _trainedModel = pipeline.Fit(dataView);

                // 4. Create Prediction Engine (Optimized for single predictions)
                _predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_trainedModel);
            });
        }

        public ModelOutput ClassifyImage(string imagePath)
        {
            if (_predictionEngine == null)
            {
                throw new InvalidOperationException("Model henüz eğitilmedi veya yüklenmedi! Lütfen önce modeli eğitin.");
            }

            var input = new ModelInput { ImagePath = imagePath };

            // Concrete prediction processing point
            return _predictionEngine.Predict(input);
        }

        public double EvaluateModel(string testDataPath)
        {
            if (_trainedModel == null)
            {
                throw new InvalidOperationException("Değerlendirilecek eğitilmiş bir model bulunamadı.");
            }

            IDataView testDataView = _mlContext.Data.LoadFromTextFile<ModelInput>(path: testDataPath, hasHeader: false, separatorChar: ',');
            IDataView predictions = _trainedModel.Transform(testDataView);

            var metrics = _mlContext.MulticlassClassification.Evaluate(predictions, labelColumnName: "LabelKey", predictedLabelColumnName: nameof(ModelOutput.PredictedLabel));

            // Log-Loss or MacroAccuracy can be returned, for the portfolio we choose MacroAccuracy.
            return metrics.MacroAccuracy;
        }
    }
}