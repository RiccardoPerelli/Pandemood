using UnityEngine;

namespace Blocks.MovingPlatform
{
    public class PlayerParenting : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Transform root;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(root);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(null);
            }
        }
    }
}
