using System;

[Serializable] 
public class BaseItemSlotData
{
    public int itemID;

    public BaseItemSlotData(int itemID)
    {
        this.itemID = itemID;
    }
}

[Serializable]
public class SlotPanelSaveData
{
    public BaseItemSlotData[] SavedSlots;

    public SlotPanelSaveData(int amountOfSavedSlots)
    {
        SavedSlots = new BaseItemSlotData[amountOfSavedSlots];
    }
}
