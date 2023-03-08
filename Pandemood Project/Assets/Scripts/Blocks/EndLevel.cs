using General;
using PostProcessing;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Blocks
{
    public class EndLevel : MonoBehaviour
    {
        [SerializeField] private int scene;

        private ChangeProfile _changeProfile;
        [FormerlySerializedAs("_panel")] [SerializeField] private Image panel;
        [SerializeField] private int currentLevel;
        [SerializeField] private float fadeoutTime = 5;
        private float _changeTime; 
        private bool _changing;
        // Start is called before the first frame update
        // Update is called once per frame

        private void Start()
        {
            _changeProfile = FindObjectOfType<ChangeProfile>();
        }

        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.F9) && Application.isEditor)
            {
                DoNotDeleteInfo.SetLevelNo(currentLevel+1);
                DoNotDeleteInfo.SetSceneToLoad(5);
                _changeProfile.Change();
                _changing = true;
            }
            if (!_changing) return;
            var color = panel.color;
            _changeTime += Time.deltaTime;
            panel.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, _changeTime / fadeoutTime));
            if (_changeTime >= fadeoutTime + 0.1f)
            {
                SceneManager.LoadScene(8);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            DoNotDeleteInfo.SetLevelNo(currentLevel+1);
            DoNotDeleteInfo.SetSceneToLoad(5);
            if(SceneManager.GetActiveScene().buildIndex==4)
                DoNotDeleteInfo.SetSceneToLoad(7);
            _changeProfile.Change();
            _changing = true;
        }

    }
}
