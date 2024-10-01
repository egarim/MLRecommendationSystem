namespace MLRecommendationSystem;

public class Simulation
{
    static void Test()
    {
        // Example usage
        var transactions = new List<Transaction>
        {
            new Transaction { TransactionId = 1, ItemIds = new List<int> { 1, 2, 3 } },
            new Transaction { TransactionId = 2, ItemIds = new List<int> { 1, 2 } },
            new Transaction { TransactionId = 3, ItemIds = new List<int> { 2, 3 } },
            new Transaction { TransactionId = 4, ItemIds = new List<int> { 1, 3 } }
        };

        var coOccurrence = new ItemCoOccurrence();
        coOccurrence.ComputeCoOccurrences(transactions);
        var recommendations = coOccurrence.GetTopRelatedItems(1, 2);

        Console.WriteLine("Top recommendations for item 1:");
        foreach (var item in recommendations)
        {
            Console.WriteLine($"Item {item}");
        }

        // Save the model
        var modelPath = "cooccurrence_model.json";
        coOccurrence.SaveModel(modelPath);

        // Load the model
        var newCoOccurrence = new ItemCoOccurrence();
        newCoOccurrence.LoadModel(modelPath);
        var newRecommendations = newCoOccurrence.GetTopRelatedItems(1, 2);

        Console.WriteLine("Recommendations from loaded model:");
        foreach (var item in newRecommendations)
        {
            Console.WriteLine($"Item {item}");
        }
    }
}