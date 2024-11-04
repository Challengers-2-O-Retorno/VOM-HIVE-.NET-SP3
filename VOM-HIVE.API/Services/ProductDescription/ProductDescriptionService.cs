using VOM_HIVE.AI.Engine;
using VOM_HIVE.AI.Model;

namespace VOM_HIVE.API.Services.ProductDescription
{
    public class ProductDescriptionService
    {
        private readonly MarkovChainGenerator _markovChainGenerator;
        private readonly List<string> _descriptions;

        public ProductDescriptionService(IEnumerable<ProductDescriptionData> trainingData)
        {
            _markovChainGenerator = new MarkovChainGenerator(trainingData);
            _descriptions = trainingData.Select(d => d.Description).ToList();
        }

        //public string GenerateDescription(string category, string features)
        //{
        //    string startingPhrase = $"O produto na categoria {category} possui as seguintes características: {features}.";
        //    return _markovChainGenerator.GenerateDescription(startingPhrase);
        //}

        public string GenerateDescription(string category, string features)
        {
            // Cria uma frase inicial com base na categoria e nas características
            var startingPhrase = $"O produto é um {category} que oferece {features}.";
            string generatedDescription = _markovChainGenerator.GenerateDescription(startingPhrase);

            // Retorna a descrição gerada, mas verifica se é vazia ou não
            return string.IsNullOrWhiteSpace(generatedDescription) ? "Descrição não gerada. Tente novamente." : generatedDescription;
        }
    }
}
