using General;
using UnityEngine;

namespace City
{
    public class SpawnCoordinate : MonoBehaviour
    {
        [SerializeField] private GameObject[] playerObject = new GameObject[4];
        [SerializeField] private bool active = true;

        private void Start()
        {
            if (!active) return;
            switch (DoNotDeleteInfo.GETLevelNo())
            {
                case 2:
                    playerObject[0].SetActive(true);
                    break;
                case 3:
                    playerObject[2].SetActive(true);
                    break;
                case 4:
                    playerObject[3].SetActive(true);
                    break;
                default:
                    playerObject[DoNotDeleteInfo.GETLevelNo()].SetActive(true);
                    break;
            }
        }
    }
}