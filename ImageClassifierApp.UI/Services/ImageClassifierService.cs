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

        // --- SINGLETON PATTERN AREAS ---
        private static ImageClassifierService _instance;
        private static readonly object _lock = new object();

        // Global access point
        public static ImageClassifierService Instance
        {
            get
            {
                // Thread-safe (Concurrent safe) Singleton check
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImageClassifierService();
                    }
                    return _instance;
                }
            }
        }

        private ImageClassifierService()
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
                // train_data.txt dosyasındaki yolları okuyup resimleri doğrudan byte dizisine çeviriyoruz
                var lines = File.ReadAllLines(trainDataPath);
                var list = new System.Collections.Generic.List<ModelInput>();

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split(',');
                    if (parts.Length < 2) continue;

                    string path = parts[0];
                    string label = parts[1];

                    if (File.Exists(path))
                    {
                        list.Add(new ModelInput
                        {
                            ImageBytes = File.ReadAllBytes(path), // Resmi diske gidip ham byte olarak okuyoruz
                            Label = label
                        });
                    }
                }

                // Oluşturduğumuz listeyi ML.NET'in anlayacağı IDataView yapısına dönüştürüyoruz
                IDataView dataView = _mlContext.Data.LoadFromEnumerable(list);

                // 2. Data Preprocessing Pipeline
                // Verimiz doğrudan byte dizisi olarak yüklendiği için ek bir imaj dönüşümüne gerek kalmadı!
                var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey", inputColumnName: nameof(ModelInput.Label))
                    // Image Classification Eğiticisi (Doğrudan input isimli byte dizisini besliyoruz)
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

            // Arayüzden gelen dosya yolunu, tahmin motoruna vermeden önce ham byte dizisine çeviriyoruz
            var input = new ModelInput
            {
                ImageBytes = File.ReadAllBytes(imagePath)
            };

            // Somut tahmin işleme noktası
            return _predictionEngine.Predict(input);
        }

        public double EvaluateModel(string testDataPath)
        {
            if (_trainedModel == null)
            {
                throw new InvalidOperationException("No trained model was found to be evaluated.");
            }

            IDataView testDataView = _mlContext.Data.LoadFromTextFile<ModelInput>(path: testDataPath, hasHeader: false, separatorChar: ',');
            IDataView predictions = _trainedModel.Transform(testDataView);

            var metrics = _mlContext.MulticlassClassification.Evaluate(predictions, labelColumnName: "LabelKey", predictedLabelColumnName: nameof(ModelOutput.PredictedLabel));

            // Log-Loss or MacroAccuracy can be returned, for the portfolio we choose MacroAccuracy.
            return metrics.MacroAccuracy;
        }
    }
}