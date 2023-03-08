using General;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class characterUI : MonoBehaviour
    {
        private bool _selected;
        public void SetFemale()
        {
            if(_selected ) return;
            _selected = true;
            PlayerPrefs.SetInt("Character", 1);
            DoNotDeleteInfo.SetSceneToLoad(6);
            SceneManager.LoadScene(8);
        }

        public void SetMale()
        {
            if(_selected) return;
            _selected = true;
            PlayerPrefs.SetInt("Character", 0);
            DoNotDeleteInfo.SetSceneToLoad(6);
            SceneManager.LoadScene(8);
        }
    }
}
