using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Networking.Types;

public class PlayerSpawnSystem : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;


    private static List<Transform> spawnPoints = new List<Transform>();

    private int nextIndex = 0;

    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);
        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    public static void RemoveSpawnPoint(Transform transform)
    {
        spawnPoints.Remove(transform);
    }

    public override void OnStartServer()
    {
        NetworkManagerCardGame.OnServerReadied += SpawnPlayer;
    }

    [ServerCallback]
    private void OnDestroy()
    {
        NetworkManagerCardGame.OnServerReadied -= SpawnPlayer;
    }

    [Server]
    public void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

        if (spawnPoint == null)
        {
            Debug.Log("Missing spawn point for player" + nextIndex.ToString());
            return;
        }

        GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, playerPrefab.transform.rotation);
        NetworkServer.Spawn(playerInstance, conn);

        //if (spawnPoints[nextIndex].name == "SpawnPoint")
        //{
        //    GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, playerPrefab.transform.rotation, spawnPoints[nextIndex].transform);
        //    //playerInstance.transform.SetParent(spawnPoints[nextIndex].transform);
        //    NetworkServer.Spawn(playerInstance, conn);
        //    RpcSetParent(playerInstance, spawnPoints[nextIndex].name.ToString());
        //}
        //else
        //{
        //    GameObject opPlayerInstance = Instantiate(opponentPlayerPrefab, spawnPoints[nextIndex].position, opponentPlayerPrefab.transform.rotation, spawnPoints[nextIndex].transform);
        //    //opPlayerInstance.transform.SetParent(spawnPoints[nextIndex].transform);
        //    NetworkServer.Spawn(opPlayerInstance, conn);
        //    RpcSetParent(opPlayerInstance, spawnPoints[nextIndex].name.ToString());
        //}



        nextIndex++;
    }

    [ClientRpc]
    public void RpcSetParent(GameObject obj, string parentName)
    {
        GameObject parentObj = GameObject.Find(parentName);
        if (parentObj != null)
        {
            obj.transform.SetParent(parentObj.transform);
            obj.transform.position = parentObj.transform.position;
            obj.transform.rotation = parentObj.transform.rotation;
        }


    }

    //public override void OnStartClient()
    //{
    //    GameObject parent = null;
    //    for (int i = 0; i < spawnPoints.Count; i++)
    //    {
    //        if (spawnPoints[i].name == "SpawnPoint")
    //        {
    //            parent = GameObject.Find(spawnPoints[i].name);
    //            if (parent != null)
    //                playerInstance.transform.SetParent(parent.transform);
    //        }
    //        else
    //        {
    //            parent = GameObject.Find(spawnPoints[i].name);
    //            if (parent != null)
    //                opPlayerInstance.transform.SetParent(parent.transform);
    //        }
    //    }
    //}
}
