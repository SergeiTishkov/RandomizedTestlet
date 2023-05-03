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
            var rng = new Random();
            var list = items.ToList();

            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}