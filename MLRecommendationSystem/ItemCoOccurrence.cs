using System.Text.Json;
using Microsoft.ML;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MLRecommendationSystem;

public class ItemCoOccurrence
{
    // Dictionary to hold co-occurrence counts
    private Dictionary<int, Dictionary<int, int>> coOccurrenceCounts = new Dictionary<int, Dictionary<int, int>>();

    public void ComputeCoOccurrences(List<Transaction> transactions)
    {
        foreach (var transaction in transactions)
        {
            var items = transaction.ItemIds.Distinct().ToList();
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = i + 1; j < items.Count; j++)
                {
                    int itemA = items[i];
                    int itemB = items[j];

                    // Update co-occurrence for itemA
                    if (!coOccurrenceCounts.ContainsKey(itemA))
                        coOccurrenceCounts[itemA] = new Dictionary<int, int>();
                    if (!coOccurrenceCounts[itemA].ContainsKey(itemB))
                        coOccurrenceCounts[itemA][itemB] = 0;
                    coOccurrenceCounts[itemA][itemB] += 1;

                    // Update co-occurrence for itemB
                    if (!coOccurrenceCounts.ContainsKey(itemB))
                        coOccurrenceCounts[itemB] = new Dictionary<int, int>();
                    if (!coOccurrenceCounts[itemB].ContainsKey(itemA))
                        coOccurrenceCounts[itemB][itemA] = 0;
                    coOccurrenceCounts[itemB][itemA] += 1;
                }
            }
        }
    }

    public List<int> GetTopRelatedItems(int itemId, int topN)
    {
        if (!coOccurrenceCounts.ContainsKey(itemId))
            return new List<int>();

        var relatedItems = coOccurrenceCounts[itemId]
            .OrderByDescending(kv => kv.Value)
            .Take(topN)
            .Select(kv => kv.Key)
            .ToList();

        return relatedItems;
    }
    public void LoadModel(string modelPath)
    {
        var json = File.ReadAllText(modelPath);
        coOccurrenceCounts = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, int>>>(json);
    }

    public void SaveModel(string modelPath)
    {
        var json = JsonConvert.SerializeObject(coOccurrenceCounts);
        File.WriteAllText(modelPath, json);
    }

}