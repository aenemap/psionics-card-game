using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private uint _spawnPointNetworkId;
    public uint SpawnPointNetworkId
    {
        get { return _spawnPointNetworkId; }
        set { _spawnPointNetworkId = value; }
    }
    private void Awake()
    {
        PlayerSpawnSystem.AddSpawnPoint(transform);
    }

    private void OnDestroy()
    {
        PlayerSpawnSystem.RemoveSpawnPoint(transform);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(transform.position, 20f);
    //}
}
