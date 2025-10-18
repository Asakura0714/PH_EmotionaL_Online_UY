using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.Unicode;
using static UnityEngine.Rendering.CoreUtils;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class GameLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner _runner = default;
    [SerializeField] private TextMeshProUGUI proUGUI = default;
    [SerializeField] private TextMeshProUGUI proUGUI2 = default;

    [SerializeField] private Button roomCreateBtn = default;
    [SerializeField] private Button destoryBtn = default;
    [SerializeField] private Button reloadBtn = default;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private async void Start()
    {
        roomCreateBtn.onClick.AddListener(async() =>
        {
            await JoinOrCreateSession($"TestRoom:{UnityEngine.Random.Range(0,10)}");
        });

        destoryBtn.onClick.AddListener(async () =>
        {
            await ShutdownSession();
        });

        reloadBtn.onClick.AddListener(async () =>
        {
            await ReloadSessinList();
        });

        _runner.ProvideInput = true;
        _runner.AddCallbacks(this);
    }

    private void Log(string str)
    {
        Debug.Log(str);
    }

    public async UniTask JoinOrCreateSession(string sessionName)
    {
        // もう一度呼ぶとロビー側が最新化され、Host時も登録が安定する
        await _runner.JoinSessionLobby(SessionLobby.ClientServer);

        var result = await _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient, // あれば参加、なければ作成
            SessionName = sessionName
            // Scene 指定なし＝今のシーンを使う
        });

        if (result.Ok)
            proUGUI.text = $"Joined or Created: {sessionName}";
        else
            proUGUI.text = $"[Session] Start failed: {result.ShutdownReason}";
    }

    public async UniTask ShutdownSession()
    {
        if (_runner == null)
        {
            return;
        }

        if (_runner.IsRunning)
        {
            _runner.Shutdown();

            var res = await _runner.JoinSessionLobby(SessionLobby.ClientServer);

            proUGUI.text = "";
            proUGUI2.text = "";
        }
    }

    public async UniTask ReloadSessinList()
    {
        var res = await _runner.JoinSessionLobby(SessionLobby.ClientServer);
    }


    // ---- 必要最小の空実装 ----
    void INetworkRunnerCallbacks.OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    void INetworkRunnerCallbacks.OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    void INetworkRunnerCallbacks.OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    void INetworkRunnerCallbacks.OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    void INetworkRunnerCallbacks.OnInput(NetworkRunner runner, NetworkInput input) { }
    void INetworkRunnerCallbacks.OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    void INetworkRunnerCallbacks.OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner) { }
    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    void INetworkRunnerCallbacks.OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    void INetworkRunnerCallbacks.OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    void INetworkRunnerCallbacks.OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    void INetworkRunnerCallbacks.OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        if (sessionList.Count <= 0)
        {
            proUGUI2.text = "";
            return;
        }

        Log($"Room数:{sessionList.Count}");
        proUGUI2.text = ($"RoomCnt:{sessionList.Count},RoomName:{sessionList[0].Name}");
    }
    void INetworkRunnerCallbacks.OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    void INetworkRunnerCallbacks.OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    void INetworkRunnerCallbacks.OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    void INetworkRunnerCallbacks.OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    void INetworkRunnerCallbacks.OnSceneLoadDone(NetworkRunner runner) { }
    void INetworkRunnerCallbacks.OnSceneLoadStart(NetworkRunner runner) { }
}
