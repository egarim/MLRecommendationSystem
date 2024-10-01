namespace MLRecommendationSystem;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    [Test]
        public void TestComputeCoOccurrences()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = 1, ItemIds = new List<int> { 1, 2, 3 } },
                new Transaction { TransactionId = 2, ItemIds = new List<int> { 1, 2 } },
                new Transaction { TransactionId = 3, ItemIds = new List<int> { 2, 3 } },
                new Transaction { TransactionId = 4, ItemIds = new List<int> { 1, 3 } }
            };

            var coOccurrence = new ItemCoOccurrence();

            // Act
            coOccurrence.ComputeCoOccurrences(transactions);
            var topRelatedToItem1 = coOccurrence.GetTopRelatedItems(1, 2);

            // Assert
            Assert.IsNotNull(topRelatedToItem1);
            Assert.AreEqual(2, topRelatedToItem1.Count);
            Assert.AreEqual(2, topRelatedToItem1[0]); // Item 2 is most co-occurring with Item 1
            Assert.AreEqual(3, topRelatedToItem1[1]); // Item 3 is next
        }

        [Test]
        public void TestSaveAndLoadModel()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = 1, ItemIds = new List<int> { 1, 2, 3 } },
                new Transaction { TransactionId = 2, ItemIds = new List<int> { 1, 2 } }
            };

            var coOccurrence = new ItemCoOccurrence();
            coOccurrence.ComputeCoOccurrences(transactions);
            var modelPath = "cooccurrence_model.json";

            // Act
            coOccurrence.SaveModel(modelPath);

            var newCoOccurrence = new ItemCoOccurrence();
            newCoOccurrence.LoadModel(modelPath);
            var topRelatedToItem1 = newCoOccurrence.GetTopRelatedItems(1, 2);

            // Assert
            Assert.IsNotNull(topRelatedToItem1);
            Assert.AreEqual(2, topRelatedToItem1.Count);
            Assert.AreEqual(2, topRelatedToItem1[0]);
            Assert.AreEqual(3, topRelatedToItem1[1]);
        }
}