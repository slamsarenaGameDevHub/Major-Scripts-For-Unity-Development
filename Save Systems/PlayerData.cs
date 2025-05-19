using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int CurrentLevel;
    public int Money;
    

    public PlayerData(PlayerAsset playerAsset)
    {
        Money = playerAsset.Money;
        CurrentLevel=playerAsset.Level;
        
    }
}
