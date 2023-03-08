using General;
using UnityEngine;

namespace City
{
    public class SpawnCoordinate : MonoBehaviour
    {
        [Header("Player")] [SerializeField] private GameObject[] playerObject = new GameObject[4];
        [Header("Dialog")] [SerializeField] private GameObject firstDialog;

        private void Start()
        {
            switch (DoNotDeleteInfo.GETLevelNo())
            {
                case 0:
                    playerObject[0].SetActive(true);
                    firstDialog.SetActive(true);
                    break;
                case 3:
                    playerObject[0].SetActive(true);
                    break;
                case 4:
                    playerObject[2].SetActive(true);
                    break;
                case 5:
                    playerObject[3].SetActive(true);
                    break;
                default:
                    playerObject[DoNotDeleteInfo.GETLevelNo()].SetActive(true);
                    break;
            }
        }
    }
}