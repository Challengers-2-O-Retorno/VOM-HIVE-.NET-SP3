using Microsoft.ML;
using VOM_HIVE.AI.Model;

namespace VOM_HIVE.AI.Engine
{
    public class MarkovChainGenerator
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public MarkovChainGenerator(IEnumerable<ProductDescriptionData> trainingData)
        {
            _mlContext = new MLContext();
            Train(trainingData);
        }

        private void Train(IEnumerable<ProductDescriptionData> trainingData)
        {
            var trainingDataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(ProductDescriptionData.Description))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(ProductDescriptionData.Description)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            _model = pipeline.Fit(trainingDataView);
        }

        public string GenerateDescription(string input)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ProductDescriptionData, ProductDescriptionPrediction>(_model);
            var prediction = predictionEngine.Predict(new ProductDescriptionData { Description = input });

            return prediction.PredictedDescription;
        }
    }
}