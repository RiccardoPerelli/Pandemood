using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class characterUI : MonoBehaviour
    {
        private bool _selected;
        public void setFemale()
        {
            if(_selected ) return;
            _selected = true;
            PlayerPrefs.SetInt("Character", 1);
            SceneManager.LoadSceneAsync(1);
        }

        public void setMale()
        {
            if(_selected) return;
            _selected = true;
            PlayerPrefs.SetInt("Character", 0);
            SceneManager.LoadSceneAsync(1);
        }
    }
}
