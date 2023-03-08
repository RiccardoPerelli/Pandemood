using General;
using UnityEngine;

namespace Enigma_Tristezza
{
    public class SeashellOpenClose : MonoBehaviour
    {
        public GameObject upperShell;
        public GameObject Perl;
        public AudioSource AudioOpenSeashell;
        public bool open = false;

        private Animator animator;
        private InteractableObject interact;

        private static readonly int Open = Animator.StringToHash("open");

        // Start is called before the first frame update
        void Start()
        {
            animator = upperShell.GetComponent<Animator>();
            animator.SetBool(Open, false);
            if (Perl != null)
            {
                interact = Perl.GetComponent<InteractableObject>();
                interact.cantPickUp = true;
                interact._images = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bubble"))
            {
                if (!open)
                {
                    open = true;
                    animator.SetBool(Open, true);
                    AudioOpenSeashell.Play();
                    if (Perl != null)
                    {
                        interact.cantPickUp = false;
                        interact._images = true;
                    }
                }
                else
                {
                     open = false;
                     animator.SetBool(Open, false);
                     AudioOpenSeashell.Play();
                    if (Perl != null)
                    {
                        interact.cantPickUp = true;
                        interact._images = false;
                    }
                }
            }
        }
    }
}
