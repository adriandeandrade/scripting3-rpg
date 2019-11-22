
using System.Collections.Generic;

namespace enjoii.Items
{
    public class ConsumableItem : Item
    {
        public int benefitAmount;

        public ConsumableItem(int id, string name, string description, string fileName, int benefitAmount, Dictionary<string, int> stats) : base(id, name, description, fileName, stats)
        {
            this.benefitAmount = benefitAmount;
        }
    }
}

