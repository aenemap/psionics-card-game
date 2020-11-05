using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Xml.Serialization;

[System.Serializable]
public class NetworkIdentityReference : NetworkBehaviour
{
    public uint NetworkId { get; private set; }

    public NetworkIdentity Value
    {
        get
        {
            if (NetworkId == 0)
                return null;
            if (_networkIdentityCached == null)
                _networkIdentityCached = NetworkIdentity.spawned[NetworkId];

            return _networkIdentityCached;
        }
    }

    private NetworkIdentity _networkIdentityCached = null;

    public NetworkIdentityReference() { }

    public NetworkIdentityReference(NetworkIdentity networkIdentity)
    {
        if (networkIdentity == null)
            return;

        NetworkId = networkIdentity.netId;
        _networkIdentityCached = networkIdentity;
    }

    public NetworkIdentityReference(uint networkId)
    {
        NetworkId = networkId;
    }
}

public static class NetworkIdentityReferenceReaderWriter
{
    public static void WriteNetworkIdentityReference(this NetworkWriter writer, NetworkIdentityReference nir)
    {
        if (nir == null || nir.Value == null)
            writer.WriteUInt32(0);
        else
        {
            writer.WriteUInt32(nir.Value.netId);
        }
    }

    public static NetworkIdentityReference ReadNetworkIdentityReference(this NetworkReader reader)
    {
        return new NetworkIdentityReference(reader.ReadUInt32());
    }
}
     
