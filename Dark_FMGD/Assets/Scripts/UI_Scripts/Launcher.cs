using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

//���photon view ���ڷ������Ƿ�۲��ɫ
public class Launcher : MonoBehaviourPunCallbacks
{
    public TMP_InputField playerName;
    public TMP_InputField roomName;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //���ӷ�����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        //PhotonNetwork.JoinLobby();
    }
    //roomList��һ��ֻ��һ��Ԫ�أ������roomԪ����ĳ�ͻ����˳��ķ�����Ϣ
    /*public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
        }
    }*/
    public void playGame()
    {
        Debug.Log("connect succeed");
        //������ߴ�������
        if (roomName.text.Length == 0 || !PhotonNetwork.JoinOrCreateRoom(roomName.text, new Photon.Realtime.RoomOptions()
        {
            MaxPlayers = 4
        }, default))
        {
            Debug.Log("JoinOrCreateRoom False");
        }
        else
        {
            PhotonNetwork.NickName = playerName.text;
            Debug.Log("JoinOrCreateRoom Succeed");
        }
    }
    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        Debug.Log("PhotonNetwork.LoadLevel(1);");
        PhotonNetwork.LoadLevel(1);
        //PhotonNetwork.Instantiate("fox",new Vector3(0,0,0),Quaternion.identity,0);
        Debug.Log("Create succeed");
    }

}
