using System.IO;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
	//public static PlayerData playerData;
	public static string file = "player.data";

	static string path = Application.persistentDataPath + "/" + file;

	public static AllDecks defaultData = new AllDecks();


	public static void Save(AllDecks playerData)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Create(path);
		binaryFormatter.Serialize(fileStream, playerData);
		fileStream.Close();
	}


	public static AllDecks Load()
	{
		if (File.Exists(path))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileStream = File.Open(path, FileMode.Open);
            AllDecks playerData = (AllDecks)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
			return playerData;
		}
		else
		{
            // Insert lists to the 2d List
            for (int i = 0; i < 3; i++)
            {
                List<string> s = new List<string>();
                defaultData.MyCards.Add(s);
            }
            return defaultData;
		}
	}

	public static void Delete()
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}

}