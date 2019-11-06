namespace enjoii.Items
{
    public interface IItemContainer
    {
        bool CanAddItem(Item item, int itemQuantity = 1);
        bool AddItem(Item item);
        Item RemoveItem(string itemID);
        bool RemoveItem(Item item);

        void ClearContainer();
        int ItemCount(string itemID);
    }
}