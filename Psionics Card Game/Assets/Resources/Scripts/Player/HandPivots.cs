using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HandPivots: NetworkBehaviour
{
    public uint playerHandPivotNetId;
    public uint opPlayerHandPivotNetId;
    public override void OnStartServer()
    {
        GameObject parent = null;
        if (hasAuthority)
        {
            playerHandPivotNetId = this.netId;
            parent = GameObject.Find("HandArea");
        }
        else
        {
            opPlayerHandPivotNetId = this.netId;
            parent = GameObject.Find("OpHandArea");
        }
            
        this.transform.SetParent(parent.transform);
        this.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);
    }
}
