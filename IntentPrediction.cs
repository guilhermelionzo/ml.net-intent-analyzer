using Microsoft.ML.Runtime.Api;

namespace IntentAnalysis
{
    public class IntentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabel;
    }
}