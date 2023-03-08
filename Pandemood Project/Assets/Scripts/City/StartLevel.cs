using General;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace City
{
    public class StartLevel : MonoBehaviour
    {
        private static readonly bool[] FirstEnter = {true, true, true, true};

        [SerializeField] private int sceneToLoad;
        [SerializeField] private int sceneIndex;

        
        [SerializeField] private Image panel;
        private bool _changing;

        private float _changeTime;

        private bool _interactable;
        
        [SerializeField] private float fadeoutTime = 2;

        private void Update()
        {
            if (_interactable && Input.GetButton("Interact"))
            {
                _changing = true;
            }

            if (!_changing) return;
            var color = panel.color;
            _changeTime += Time.deltaTime;
            panel.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, _changeTime / fadeoutTime));
            if (_changeTime >= fadeoutTime +0.1)
            {
                LoadLevel();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player") || _changing) return;
            if (FirstEnter[sceneIndex])
            {
                FirstEnter[sceneIndex] = false;
                _changing = true;
            }
            else
            {
                _interactable = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_interactable) _interactable = false;
        }

        private void LoadLevel()
        {
            DoNotDeleteInfo.SetLevelNo(sceneIndex);
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}
