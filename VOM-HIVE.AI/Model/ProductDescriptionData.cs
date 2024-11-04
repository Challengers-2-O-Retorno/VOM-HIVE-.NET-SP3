using Microsoft.ML.Data;

namespace VOM_HIVE.AI.Model
{
    public class ProductDescriptionData
    {
        public string Description { get; set; }
    }

    public class ProductDescriptionPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedDescription { get; set; }
    }
}
