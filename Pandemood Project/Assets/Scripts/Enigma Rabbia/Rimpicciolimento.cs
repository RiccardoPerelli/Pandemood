using UnityEngine;

namespace Enigma_Rabbia
{
    public class Rimpicciolimento : MonoBehaviour
    {
        public delegate void Small();
        public static event Small SmallTriggered;
        public delegate void Big();
        public static event Big BigTriggered;

        public bool shrink;

        public GameObject particleEffect;


        private CapsuleCollider _collider;

        private void Start()
        {
            _collider = GetComponent<CapsuleCollider>();
        }

        // Update is called once per frame
        private void Update()
        {
            if(Input.GetButtonDown("Shrink"))
            {
                var transform1 = transform;
                if (!shrink)
                {
                    transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    SmallTriggered?.Invoke();
                    shrink = true;
                    GameObject tmp = Instantiate(particleEffect, transform1.position, transform1.rotation);
                    tmp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }
                else
                {
                    var position = transform1.position;
                    Vector3 start = position, end = position;
                    start.y += _collider.radius + 0.01f;
                    end.y += _collider.height + 0.01f;
                    var col = new Collider[1];
                    if(Physics.OverlapCapsuleNonAlloc(start, end, _collider.radius, col, ~LayerMask.GetMask("Player")) != 0) return;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    BigTriggered?.Invoke();
                    shrink = false;
                    Instantiate(particleEffect, position, transform1.rotation);
                }
            }
        }
    }
}
