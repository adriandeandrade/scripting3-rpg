using System;

[Serializable]
public class SlotPanelData
{
    public BaseItemSlotData[] SavedSlots;

    public SlotPanelData(int amountOfSavedSlots)
    {
        SavedSlots = new BaseItemSlotData[amountOfSavedSlots];
    }
}
