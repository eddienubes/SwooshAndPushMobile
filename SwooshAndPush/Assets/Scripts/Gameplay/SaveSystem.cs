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

        //string pathLoc = Path.Combine(Application.persistentDataPath, "playerconfig.json");
        //string pathPlayer = Path.Combine(Application.persistentDataPath, "locationconfig.json");

        string pathLoc = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\playerconfig.json";
        string pathPlayer = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\locationconfig.json";

        File.WriteAllText(pathLoc, jsonPlayer);
        File.WriteAllText(pathPlayer, jsonLoc);
    }

    public static void LoadPlayer(out Location loc, out PlayerStats stats)
    {
        //string pathLoc = Path.Combine(Application.persistentDataPath, "playerconfig.json");
        //string pathPlayer = Path.Combine(Application.persistentDataPath, "locationconfig.json");

        string pathLoc = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\playerconfig.json";
        string pathPlayer = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\locationconfig.json";

        string jsonPlayer = File.ReadAllText(pathLoc);
        string jsonLoc = File.ReadAllText(pathPlayer);

        stats = JsonUtility.FromJson<PlayerStats>(jsonPlayer);
        loc = JsonUtility.FromJson<Location>(jsonLoc);
    }


}
