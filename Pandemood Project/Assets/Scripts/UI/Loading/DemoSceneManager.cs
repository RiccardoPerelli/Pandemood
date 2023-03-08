using System.Collections;
using General;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Loading
{
    public class DemoSceneManager : MonoBehaviour
    {
        public bool activateLoading = true;
        private LoadingScreenManager _loadingManager;
        [SerializeField] private float waitTime = 2;

        private void Start()
        {
            Time.timeScale = 1f;
        }

        private void Awake()
        {
            _loadingManager = transform.GetComponentInChildren<LoadingScreenManager>();
            StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            yield return new WaitForSeconds(waitTime); AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(DoNotDeleteInfo.GetSceneToLoad());

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
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
}
