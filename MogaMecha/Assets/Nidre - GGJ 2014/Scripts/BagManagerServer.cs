using UnityEngine;
using System.Collections;

public static class BagManagerServer {

    public static BagSpawnerServer bagSpawner;

    public static void RespawnBag()
    {
        if (bagSpawner)
        {
            bagSpawner.RespawnNewBag();
        }
    }

}
