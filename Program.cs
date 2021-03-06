using System;
using Microsoft.ML.Models;
using Microsoft.ML;

namespace IntentAnalysis
{
    class Program
    {
        const string dataPath = "Data/intents.txt";
        const string testPath = "Data/testData.txt";
        static void Main(string[] args)
        {
            var trainer = new IntentTrainer();
            var model = trainer.Train();
            Evaluate(model);

            Console.WriteLine("> Please enter a message to be analyzed.");
            var input = Console.ReadLine();

            while (input != "exit")
            {
                var prediction = model.Predict(new IntentData { Text = input, Label = "" });

                Console.WriteLine(prediction.PredictedLabel);

                Console.WriteLine("> Please enter a message to be analyzed.");
                input = Console.ReadLine();
            }
        }
        public static void Evaluate(PredictionModel<IntentData, IntentPrediction> model)
        {
            var testData = new TextLoader<IntentData>(testPath, useHeader: false, separator: "tab");
            var evaluator = new ClassificationEvaluator();
            var metrics = evaluator.Evaluate(model, testData);

            Console.WriteLine();
            Console.WriteLine("PredictionModel quality metrics evaluation");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Accuracy Micro: {metrics.AccuracyMicro * 100}%");
            Console.WriteLine($"Accuracy Macro: {metrics.AccuracyMacro * 100}%");
        }
    }
}