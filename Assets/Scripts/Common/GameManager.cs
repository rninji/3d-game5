using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Constants;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject playerPrefab;

    public EGameState GameState { get; private set; }
    
    private GameObject _player;
    private Canvas _canvas;
    private bool _isCursorLock;
    
    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _canvas = GetCanvas();
        
        switch (scene.name)
        {
            case "Main":
                if (_player)
                {
                    Destroy(_player);
                    _player = null;
                }
                break;
            case "Stage01":
            case "Stage02":
                var spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
                if (_player)
                {
                    _player.SetActive(true);
                    _player.transform.position = spawnPoint.position;
                    _player.transform.rotation = spawnPoint.rotation;
                }
                else
                {
                    _player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
                    DontDestroyOnLoad(_player);
                }
                break;
        }
        
        SetGameState(EGameState.Play);
    }
    
    protected override void OnSceneUnloaded(Scene scene)
    {
        _canvas = null;
        _player.SetActive(false);
    }

    public void LoadScene(ESceneName sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    IEnumerator LoadSceneAsyncCoroutine(ESceneName sceneName)
    {
        SetGameState(EGameState.Pause);
        
        // 로딩 화면 띄우기
        var loadingPanelPrefab = Resources.Load<GameObject>("LoadingPanel");
        var loadingPanelObject = Instantiate(loadingPanelPrefab, _canvas.transform);
        var loadingPanelController = loadingPanelObject.GetComponent<LoadingPanelController>();
        
        // 로딩 창 표시
        bool showDone = false;
        loadingPanelController.Show(() => showDone = true);
        yield return new WaitUntil(() => showDone);
        
        // 씬 로드 진행
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName.ToString());
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            loadingPanelController.SetProgress(asyncOperation.progress);
            yield return null;
        }
        loadingPanelController.SetProgress(1f);
        asyncOperation.allowSceneActivation = true;

        bool hideDone = false;
        loadingPanelController.Hide(()=>hideDone = true);
        yield return new WaitUntil(() => hideDone);
        
        Destroy(loadingPanelObject);
    }

    Canvas GetCanvas()
    {
        var canvasObject = GameObject.FindGameObjectWithTag("Canvas");
        Canvas result = null;

        if (!canvasObject)
        {
            canvasObject = new GameObject("Canvas");
            result = canvasObject.AddComponent<Canvas>();
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();

            result.renderMode = RenderMode.ScreenSpaceOverlay;
            result.tag = "Canvas";
        }
        else
        {
            result = canvasObject.GetComponent<Canvas>();
        }
        return result;
    }

    public void SetCursorLock()
    {
        Cursor.visible = _isCursorLock;
        Cursor.lockState = _isCursorLock ? CursorLockMode.None : CursorLockMode.Locked;
        _isCursorLock = !_isCursorLock;
    }

    public void SetGameState(EGameState state)
    {
        if (GameState == state) return;
        
        GameState = state;
    }
}
