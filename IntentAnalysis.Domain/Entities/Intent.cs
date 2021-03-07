using System;
using IntentAnalysis.Domain.ValueObjects;
using Microsoft.ML;
using Microsoft.ML.Models;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace IntentAnalysis.Domain.Entities
{
    public class Intent
    {

        [ColumnName("PredictedLabel")]
        public string PredictedLabel;


        private const string dataPath = "Data/intents.txt";
        private const string testPath = "Data/testData.txt";
        public PredictionModel<IntentData, Intent> Train()
        {
            var pipeline = GetPipeline();
            var model = pipeline.Train<IntentData, Intent>();
            return model;
        }

        public LearningPipeline GetPipeline()
        {
            var pipeline = new LearningPipeline();
            pipeline.Add(new TextLoader<IntentData>(dataPath, separator: "tab"));
            pipeline.Add(new TextFeaturizer(outputColumn: "Features", inputColumns: "Text"));
            pipeline.Add(new Dictionarizer("Label"));
            pipeline.Add(new StochasticDualCoordinateAscentClassifier());
            pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });

            return pipeline;
        }
        public void Evaluate()
        {
            var testData = new TextLoader<IntentData>(testPath, useHeader: false, separator: "tab");
            var evaluator = new ClassificationEvaluator();
            var metrics = evaluator.Evaluate(Train(), testData);

            Console.WriteLine();
            Console.WriteLine("PredictionModel quality metrics evaluation");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Accuracy Micro: {metrics.AccuracyMicro * 100}%");
            Console.WriteLine($"Accuracy Macro: {metrics.AccuracyMacro * 100}%");
        }
    }
}