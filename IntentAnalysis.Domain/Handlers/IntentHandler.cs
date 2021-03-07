using Microsoft.ML;
using IntentAnalysis.Domain.ValueObjects;
using IntentAnalysis.Domain.Entities;

namespace IntentAnalysis.Domain.Handler
{
    public class IntentHandler
    {
        const string dataPath = "Data/intents.txt";
        const string testPath = "Data/testData.txt";
        private readonly PredictionModel<IntentData, Intent> _model;
        
        public IntentHandler()
        {
            var intent = new Intent();
            _model = intent.Train();
            intent.Evaluate();
        }

        public string ReturnIntent(string input) => _model.Predict(new IntentData { Text = input, Label = "" }).PredictedLabel;

    }
}