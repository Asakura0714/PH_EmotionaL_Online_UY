using UnityEngine;


public class TestScene
{
    private void Start()
    {
        //PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        //PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    ///　マスターサーバーへ接続が成功したとき
    /// </summary>
    public  void OnConnectedToMaster()
    {
        /*
        string roomName = "Room";
        var roomOption = new RoomOptions();

        //ルームに参加(roomName存在しないときには作成して参加する)
        bool isJoin = PhotonNetwork.JoinOrCreateRoom(roomName, roomOption, TypedLobby.Default);
        if(isJoin)
        {
            Debug.Log("マスターサーバーに参加参加しました");
        }
        else
        {
            Debug.Log("マスターサーバーに参加していません");
        }
        */
    }

    /// <summary>
    /// ゲームサーバーへ接続が成功したとき
    /// </summary>
    public  void OnJoinedRoom()
    {
        /*
        Debug.Log("ゲームサーバーに参加参加しました");

        var pos = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        var obj = PhotonNetwork.Instantiate("Avatar", pos, Quaternion.identity);

        if (obj != null)
        {
            Debug.Log("インスタンスの生成が完了しました");
        }*/
    }

    public  void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);


        Debug.Log("ゲームサーバーに接続が失敗しました"+message);
    }
}
