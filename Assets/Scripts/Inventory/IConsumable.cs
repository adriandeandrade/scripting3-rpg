using enjoii.Characters;
using enjoii.Items;

public interface IConsumable
{
    void Use(Player player, ConsumableItem consumable);
}
