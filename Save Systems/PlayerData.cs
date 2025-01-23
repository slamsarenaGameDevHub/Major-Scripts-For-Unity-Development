using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int CurrentLevel;
    public int Money;
    

    public PlayerData(InventoryAsset inventory)
    {
        Money = inventory.Money;
        CurrentLevel=inventory.Level;
        
    }
}
