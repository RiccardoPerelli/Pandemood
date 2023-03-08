using System;
using UnityEngine;
using General;

namespace UI
{
    public class mainMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            DoNotDeleteInfo.ResetInfo();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
