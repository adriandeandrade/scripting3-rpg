public class PlayerData
{
    public SlotPanelData inventoryPanel;
    public SlotPanelData equipmentPanel;
    public PlayerStatData playerStats;
    public TransformData transformData;

    public PlayerData(SlotPanelData inventoryPanel, SlotPanelData equipmentPanel, PlayerStatData playerStats, TransformData transformData)
    {
        this.inventoryPanel = inventoryPanel;
        this.equipmentPanel = equipmentPanel;
        this.playerStats = playerStats;
        this.transformData = transformData;
    }
}
