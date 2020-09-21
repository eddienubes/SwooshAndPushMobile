using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using UnityEditor;

public static class SaveSystem
{
    public static void SavePlayer(Location loc, PlayerStats stats)
    {
        string jsonLoc = JsonUtility.ToJson(loc);
        string jsonPlayer = JsonUtility.ToJson(stats);

        string pathPlayer = Path.Combine(Application.persistentDataPath, "playerconfig.json");
        string pathLoc = Path.Combine(Application.persistentDataPath, "locationconfig.json");

        //string pathLoc = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\playerconfig.json";
        //string pathPlayer = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\locationconfig.json";

        File.WriteAllText(pathPlayer, jsonPlayer);
        File.WriteAllText(pathLoc, jsonLoc);
    }

    public static void LoadPlayer(out Location loc, out PlayerStats stats)
    {

        if (PlayerPrefs.GetInt("FIRSTLAUNCH", 1) == 1)
        {
            PlayerStats playerStats = new PlayerStats();
            PlayerPrefs.SetInt("FIRSTLAUNCH", 0);
            Location location = new Location();
            SavePlayer(location, playerStats);
        }
        
        string pathPlayer = Path.Combine(Application.persistentDataPath, "playerconfig.json");
        string pathLoc = Path.Combine(Application.persistentDataPath, "locationconfig.json");

        //string pathLoc = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\playerconfig.json";
        //string pathPlayer = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\locationconfig.json";

        string jsonPlayer = File.ReadAllText(pathPlayer);
        string jsonLoc = File.ReadAllText(pathLoc);
        stats = JsonUtility.FromJson<PlayerStats>(jsonPlayer);
        loc = JsonUtility.FromJson<Location>(jsonLoc);
    }
}
