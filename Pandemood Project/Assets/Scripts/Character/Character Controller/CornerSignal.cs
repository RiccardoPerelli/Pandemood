using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Character_Controller
{
    public class CornerSignal : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer1;
        [SerializeField] private SpriteRenderer spriteRenderer2;

        private void OnCollisionEnter(Collision other)
        {
            var obj = other.gameObject;
            if (obj.CompareTag("Corner") || obj.CompareTag("Corner1") || obj.CompareTag("Corner2") ||
                obj.CompareTag("Corner3"))
            {
                spriteRenderer1.enabled = true;
                spriteRenderer2.enabled = true;
            }
                
        }
        
        
        private void OnCollisionExit(Collision other)
        {
            var obj = other.gameObject;
            if (obj.CompareTag("Corner") || obj.CompareTag("Corner1") || obj.CompareTag("Corner2") ||
                obj.CompareTag("Corner3"))
            {
                spriteRenderer1.enabled = false;
                spriteRenderer2.enabled = false;
            }
                
        }
    }
}
