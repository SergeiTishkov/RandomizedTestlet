using RandomizedTestlet.library.Enums;

namespace RandomizedTestlet.library.Entities
{
    public class Testlet
    {
        public const int BEGINNING_PRETEST_ITEMS_AMOUNT = 2;
        public const int PRETEST_ITEMS_AMOUNT = 4;
        public const int OPERATIONAL_ITEMS_AMOUNT = 6;
        private List<Item> _items;

        public Testlet(string testletId, List<Item> items)
        {
            ArgumentNullException.ThrowIfNull(testletId, nameof(testletId));
            ArgumentNullException.ThrowIfNull(items, nameof(items));

            if (items.Count(i => i.ItemType == ItemTypeEnum.Pretest) != PRETEST_ITEMS_AMOUNT)
            {
                throw new ArgumentException($"Amount of Items with ItemType = {ItemTypeEnum.Pretest} is not equal {PRETEST_ITEMS_AMOUNT}");
            }

            if (items.Count(i => i.ItemType == ItemTypeEnum.Operational) != OPERATIONAL_ITEMS_AMOUNT)
            {
                throw new ArgumentException($"Amount of Items with ItemType = {ItemTypeEnum.Operational} is not equal {OPERATIONAL_ITEMS_AMOUNT}");
            }

            TestletId = testletId;
            _items = items;
        }

        public string TestletId { get; private set; }

        public List<Item> Randomize()
        {
            var pretestItems = new List<Item>();
            var operationalItems = new List<Item>();

            foreach (var item in _items)
            {
                if (item.ItemType == ItemTypeEnum.Pretest)
                {
                    pretestItems.Add(item);
                }
                else
                {
                    operationalItems.Add(item);
                }
            }

            pretestItems = Shuffle(pretestItems);

            var begginningPretestItems = pretestItems.Take(BEGINNING_PRETEST_ITEMS_AMOUNT);
            
            var randomizedOtherItems = Shuffle(pretestItems.Skip(BEGINNING_PRETEST_ITEMS_AMOUNT).Concat(operationalItems));

            return begginningPretestItems.Concat(randomizedOtherItems).ToList();
        }

        private List<Item> Shuffle(IEnumerable<Item> items)
        {
            var random = new Random();
            var list = items.ToList();

            int swapIndex1 = list.Count;

            while (swapIndex1 > 1)
            {
                swapIndex1--;

                int swaoIndex2 = random.Next(swapIndex1 + 1);

                var value = list[swaoIndex2];
                list[swaoIndex2] = list[swapIndex1];
                list[swapIndex1] = value;
            }

            return list;
        }
    }
}