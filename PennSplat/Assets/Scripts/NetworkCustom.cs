using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkCustom : NetworkManager
{

    public GameObject[] characters;
    private int group1 = 0;
    private int group2 = 0;

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        /*
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);
        */

        GameObject player;
        //Transform startPos = GetStartPosition();

        if (group1 > group2)
        {
            Debug.Log("Add orange");
            player = Instantiate(characters[1], new Vector3(10, 0, 0), Quaternion.identity) as GameObject;
            group2++;
        }
        else
        {
            Debug.Log("Add purple");
            player = Instantiate(characters[0], Vector3.zero, Quaternion.identity) as GameObject;
            group1++;

        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }

    /*
    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = chosenCharacter;

        ClientScene.AddPlayer(conn, 0, test);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }
    */

}