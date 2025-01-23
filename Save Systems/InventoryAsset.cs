using UnityEngine;
[CreateAssetMenu(fileName = "Inventory Asset", menuName = "Inventory Asset")]
public class InventoryAsset : ScriptableObject
{
    public int Money;
    public int Level;


    public void DeleteInventory()
    {
        Money=0;
        Level=0;
        SavedPlayerData.SaveData(new PlayerData(this));
    }
}
