namespace ImageClassifierApp.Models
{
    public class ModelOutput
    {
        public string PredictedLabel { get; set; }
        public float[] Score { get; set; }
    }
}