using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


/* The purpose of this script is: to create a saving system
 * We save/load the class AllDecks
 * 
 * When the player first starts we load the previous save
 * If none exists, we load an empty All Decks class
 * 
 * The class is static, in order to be accessed from anywhere
*/


public static class SaveSystem
{
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