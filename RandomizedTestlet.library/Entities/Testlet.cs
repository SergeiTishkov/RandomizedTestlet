using RandomizedTestlet.library.Enums;

namespace RandomizedTestlet.library.Entities
{
    public class Testlet
    {
        public string TestletId;
        private List<Item> Items;
        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            var pretestItems = new List<Item>();
            var operationalItems = new List<Item>();

            foreach (var item in Items)
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

            return pretestItems.Take(2).Concat(Shuffle(pretestItems.Skip(2), operationalItems)).ToList();
        }

        private List<Item> Shuffle(params IEnumerable<Item>[] items)
        {
            var rng = new Random();
            var list = items.SelectMany(i => i).ToList();

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