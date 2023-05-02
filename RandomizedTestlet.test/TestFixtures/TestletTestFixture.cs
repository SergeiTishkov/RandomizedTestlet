using NUnit.Framework;

namespace RandomizedTestlet.test.TestFixtures
{
    [TestFixture]
    public class TestletTestFixture
    {
        static IEnumerable<TestCaseData> GetTestItemsDataSource()
        {
            yield return new TestCaseData(
                new List<Item>
                {
                    new Item { ItemId = "some test item Id 1", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 2", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 3", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 4", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 5", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 6", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 7", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 8", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 9", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 10", ItemType = ItemTypeEnum.Operational },
                }
            );

            yield return new TestCaseData(
                new List<Item>
                {
                    new Item { ItemId = "some test item Id 1", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 2", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 3", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 4", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 5", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 6", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 7", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 8", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 9", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 10", ItemType = ItemTypeEnum.Pretest },
                }
            );

            yield return new TestCaseData(
                new List<Item>
                {
                    new Item { ItemId = "some test item Id 1", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 2", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 3", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 4", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 5", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 6", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 7", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 8", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 9", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 10", ItemType = ItemTypeEnum.Operational },
                }
            );

            yield return new TestCaseData(
                new List<Item>
                {
                    new Item { ItemId = "some test item Id 1", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 2", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 3", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 4", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 5", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 6", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 7", ItemType = ItemTypeEnum.Pretest },
                    new Item { ItemId = "some test item Id 8", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 9", ItemType = ItemTypeEnum.Operational },
                    new Item { ItemId = "some test item Id 10", ItemType = ItemTypeEnum.Pretest },
                }
            );
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeDoesntChangeTheCountOfTestTypes(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedTestItems = testlet.Randomize();

            Assert.AreEqual(randomizedTestItems.Count, 10);
            Assert.AreEqual(randomizedTestItems.Count(item => item.ItemType == ItemTypeEnum.Pretest), 4);
            Assert.AreEqual(randomizedTestItems.Count(item => item.ItemType == ItemTypeEnum.Operational), 6);
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeDoesntChangeItemObjects(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedTestItems = testlet.Randomize();

            items.ForEach(item =>
            {
                Assert.IsNotNull(randomizedTestItems.Any(randomizedTestItem => object.ReferenceEquals(randomizedTestItem, item)));
            });
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeDoesntDuplicateItemObjects(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedTestItems = testlet.Randomize();

            items.ForEach(item =>
            {
                Assert.DoesNotThrow(() =>
                {
                    randomizedTestItems.Single(randomizedTestItem => object.ReferenceEquals(randomizedTestItem, item));
                });
            });
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeReturnsFirstTwoPretests(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedTestItems = testlet.Randomize();

            Assert.IsTrue(items.Take(2).All(item => item.ItemType == ItemTypeEnum.Pretest));
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeReturnsLastEightInRandom(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedItems1 = testlet.Randomize().Skip(2).ToList();
            var randomizedItems2 = testlet.Randomize().Skip(2).ToList();

            // sometime this test might eventually fail (and this is expected!)
            // so to reduce the amount of false fails we use 2 arraus of results here
            Assert.IsTrue(
                Enumerable.Range(0, 10)
                    .Count(i =>
                    {
                        return items[i].Id.Equals(randomizedItems1[i].Id) ||
                               items[i].Id.Equals(randomizedItems2[i].Id) ||
                               randomizedItems1[i].Id.Equals(randomizedItems2[i].Id);
                    }) > 2
            );
        }
    }
}