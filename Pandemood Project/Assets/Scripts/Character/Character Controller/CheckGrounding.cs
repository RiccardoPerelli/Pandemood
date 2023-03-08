using UnityEngine;

namespace Character.Character_Controller
{
    public class CheckGrounding : MonoBehaviour
    {

        public delegate void Falling();
        public event Falling OnFalling;
        public delegate void Grounding();
        public event Grounding OnGrounding;

        private bool _isGrounding = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Pushable"))
            {
                if (OnGrounding != null && !_isGrounding)
                {
                    OnGrounding();
                    _isGrounding = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Pushable"))
            {
                if (OnFalling != null && _isGrounding)
                {
                    OnFalling();
                    _isGrounding = false;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Pushable"))
            {
                if (OnGrounding != null && !_isGrounding)
                {
                    OnGrounding();
                    _isGrounding = true;
                }
            }
        }
    }
}
