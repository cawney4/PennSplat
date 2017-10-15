using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkScript : NetworkManager
{

    public Transform spawnPosition;
    public int curPlayer;
    //public GameObject[] spawnPrefabs;

    private int group1 = 0;
    private int group2 = 0;


    //Called on client when connect
    public override void OnClientConnect(NetworkConnection conn)
    {

        // Create message to set the player
        IntegerMessage msg = new IntegerMessage(curPlayer);

        // Call Add player and pass the message
        ClientScene.AddPlayer(conn, 0, msg);
    }

    // Server
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        if (group1 > group2)
        {
            //Select the prefab from the spawnable objects list
            var playerPrefab = spawnPrefabs[1];
            group2++;
        } else
        {
            var playerPrefab = spawnPrefabs[0];
            group1++;
        }

        // Create player object with prefab
        var player = Instantiate(playerPrefab, spawnPosition.position, Quaternion.identity) as GameObject;

        // Add player object for connection
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}