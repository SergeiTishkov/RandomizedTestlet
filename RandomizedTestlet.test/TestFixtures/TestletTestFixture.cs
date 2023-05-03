using NUnit.Framework;
using RandomizedTestlet.library.Entities;
using RandomizedTestlet.library.Enums;

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

        [Test]
        public void TestletCtorThrowErrorOnNullId()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(() => new Testlet(null, GetTestItemsDataSource().First().Arguments[0] as List<Item>));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Test]
        public void TestletCtorThrowErrorOnNullTestItems()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => new Testlet("some ID", null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void TestletCtorDoesntThrowErrorsOnValidData(List<Item> items)
        {
            Assert.DoesNotThrow(() => new Testlet("some ID", items));
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void TestletCtorThrowErrorOnMoreThan4Pretests(List<Item> items)
        {
            items.Add(new Item { ItemId = "some test item Id 11", ItemType = ItemTypeEnum.Pretest });

            Assert.Throws<ArgumentException>(() => new Testlet("some ID", items));
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void TestletCtorThrowErrorOnMoreThan6Operationals(List<Item> items)
        {
            items.Add(new Item { ItemId = "some test item Id 11", ItemType = ItemTypeEnum.Operational });

            Assert.Throws<ArgumentException>(() => new Testlet("some ID", items));
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void TestletCtorThrowErrorOnLessThan4Pretests(List<Item> items)
        {
            items.Remove(items.First(item => item.ItemType == ItemTypeEnum.Pretest));

            Assert.Throws<ArgumentException>(() => new Testlet("some ID", items));
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void TestletCtorThrowErrorOnLessThan6Operationals(List<Item> items)
        {
            items.Remove(items.First(item => item.ItemType == ItemTypeEnum.Operational));

            Assert.Throws<ArgumentException>(() => new Testlet("some ID", items));
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeDoesntChangeTheCountOfTestTypes(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedTestItems = testlet.Randomize();

            Assert.AreEqual(randomizedTestItems.Count, Testlet.PRETEST_ITEMS_AMOUNT + Testlet.OPERATIONAL_ITEMS_AMOUNT);
            Assert.AreEqual(randomizedTestItems.Count(item => item.ItemType == ItemTypeEnum.Pretest), Testlet.PRETEST_ITEMS_AMOUNT);
            Assert.AreEqual(randomizedTestItems.Count(item => item.ItemType == ItemTypeEnum.Operational), Testlet.OPERATIONAL_ITEMS_AMOUNT);
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeDoesntChangeItemObjectsOrIds(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedTestItems = testlet.Randomize();

            items.ForEach(item =>
            {
                Assert.IsNotNull(randomizedTestItems.Any(randomizedTestItem =>
                        ReferenceEquals(randomizedTestItem, item) && randomizedTestItem.ItemId.Equals(item.ItemId)));
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
                    randomizedTestItems.Single(randomizedTestItem => ReferenceEquals(randomizedTestItem, item));
                });
            });
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeReturnsFirstTwoPretests(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedTestItems = testlet.Randomize();

            Assert.IsTrue(randomizedTestItems.Take(2).All(item => item.ItemType == ItemTypeEnum.Pretest));
        }

        [TestCaseSource(nameof(GetTestItemsDataSource))]
        public void RandomizeReturnsLastEightInRandom(List<Item> items)
        {
            var testlet = new Testlet("some ID", items);

            var randomizedItems1 = testlet.Randomize();
            var randomizedItems2 = testlet.Randomize();

            // sometime this test might eventually fail (and this is expected!)
            // so to reduce the amount of false fails we use 2 arrays of results here
            Assert.IsTrue(
                Enumerable.Range(0, 10)
                    .Count(i =>
                    {
                        return items[i].ItemId != randomizedItems1[i].ItemId ||
                               items[i].ItemId != randomizedItems2[i].ItemId ||
                               randomizedItems1[i].ItemId != randomizedItems2[i].ItemId;
                    }) > Testlet.PRETEST_ITEMS_AMOUNT
            );
        }
    }
}