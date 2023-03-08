using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSceneManager : MonoBehaviour
{
    public bool activateLoading = true;
    private LoadingScreenManager _loadingManager;

    private void Start()
    {
        _loadingManager = transform.GetComponentInChildren<LoadingScreenManager>();
    }

    private void Update()
    {
        if (activateLoading)
        {
            activateLoading = false;
            _loadingManager.RevealLoadingScreen();
        }
    }
}
