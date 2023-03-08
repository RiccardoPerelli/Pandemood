using UnityEngine;

namespace Blocks
{
    public class DespawnBase : MonoBehaviour
    {
        private int _cont;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (_cont == 3)
            {
                transform.position = new Vector3(1000, 1000, 100);
                Destroy(this, 0.2f);
            }

            _cont++;
        }
    }
}