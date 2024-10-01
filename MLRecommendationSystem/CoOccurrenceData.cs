namespace MLRecommendationSystem;

public class CoOccurrenceData
{
    public int ItemId { get; set; }
    public int RelatedItemId { get; set; }
    public float Score { get; set; } // Co-occurrence count
}