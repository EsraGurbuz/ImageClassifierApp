using Microsoft.ML.Data;

namespace ImageClassifierApp.Models
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public string ImagePath { get; set; }

        [LoadColumn(1)]
        public string Label { get; set; }
    }
}