using Microsoft.ML.Data;

namespace ImageClassifierApp.Models
{
    public class ModelInput
    {
        // ML.NET'in görüntüyü doğrudan ham byte dizisi olarak yüklemesini sağlıyoruz
        [ColumnName("input")]
        public byte[] ImageBytes { get; set; }

        [ColumnName("Label")]
        public string Label { get; set; }
    }
}