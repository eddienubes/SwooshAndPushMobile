using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class SaveSystem
{
    public static void SavePlayer()
    {
        string jsonLoc = JsonConvert.SerializeObject(LocationManager.CurrentLocation);
        string jsonPlayer = JsonConvert.SerializeObject(Player.PlayerStats);

        // string pathPlayer = Path.Combine(Application.persistentDataPath, "playerconfig.json");
        // string pathLoc = Path.Combine(Application.persistentDataPath, "locationconfig.json");

        string pathPlayer = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\playerconfig.json";
        string pathLoc = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\locationconfig.json";

        File.WriteAllText(pathPlayer, jsonPlayer);
        File.WriteAllText(pathLoc, jsonLoc);
    }

    public static void LoadPlayer()
    {

        if (PlayerPrefs.GetInt("FIRSTLAUNCH", 1) == 1)
        {
            Player.PlayerStats = new PlayerStats();
            LocationManager.CurrentLocation = new Location();

            // PlayerPrefs.SetInt("FIRSTLAUNCH", 0);99999999999
            SavePlayer();
        }
        else
        {
            // string pathPlayer = Path.Combine(Application.persistentDataPath, "playerconfig.json");
            // string pathLoc = Path.Combine(Application.persistentDataPath, "locationconfig.json");

            string pathPlayer = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\playerconfig.json";
            string pathLoc = "D:\\CODE\\Unity\\SwooshAndPushMobile\\SwooshAndPush\\Assets\\Data\\locationconfig.json";

            string jsonPlayer = File.ReadAllText(pathPlayer);
            string jsonLoc = File.ReadAllText(pathLoc);


            Player.PlayerStats = JsonConvert.DeserializeObject<PlayerStats>(jsonPlayer);
            LocationManager.CurrentLocation = JsonConvert.DeserializeObject<Location>(jsonLoc);
        }
    }
}
