using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PurchasedItems
{
    public static void SaveList(List<string> stringList)
    {
        string filePath = Application.persistentDataPath + "/CraftedItems.bs";
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(stream, stringList);
        }
    }

    public static List<string> LoadList()
    {
        string filePath = Application.persistentDataPath + "/CraftedItems.bs";
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                return (List<string>)formatter.Deserialize(stream);
            }
        }
        return new List<string>(); // Return an empty list if the file doesn't exist
    }

    public static void AddToList(string newItem)
    {
        string filePath = Application.persistentDataPath + "/CraftedItems.bs";
        // Load the existing list
        List<string> stringList = LoadList();

        // Add the new item to the list
        stringList.Add(newItem);

        // Save the updated list
        SaveList(stringList);
    }
	public static void DeleteWeapons(){
		string filePath = Application.persistentDataPath + "/CraftedItems.bs";
		if(File.Exists(filePath))
		{
			File.Delete(filePath);
		}
	}

   
}
