using UnityEngine;
using UnityEngine.Serialization;

namespace Enigma_Tristezza
{
    public class PathResetComplete : MonoBehaviour
    {
        [FormerlySerializedAs("Transparent_mat")] public Material transparentMat;
        [FormerlySerializedAs("RockGreen_mat")] public Material rockGreenMat;
        [FormerlySerializedAs("RockDoor")] public Transform rockDoor;
        [FormerlySerializedAs("AudioWall")] public AudioSource audioWall;
        [FormerlySerializedAs("AudioReset")] public AudioSource audioReset;
        public GameObject[] cells;

        private bool _reset;

        private bool _open;
        private Vector3 _target1,_target2;

        private void Start()
        {
            var position = rockDoor.transform.position;
            _target1 = new Vector3(position.x, position.y, position.z + 3f);
            var position1 = transform.position;
            _target2 = new Vector3(position1.x, position1.y, position1.z + 3f);
        }

        private void Update()
        {
            if (_open)
            {
                float step = 3f * Time.deltaTime; // calculate distance to move
                var position = rockDoor.transform.position;
                position = Vector3.MoveTowards(position, _target1, step);
                rockDoor.transform.position = position;
                transform.position = Vector3.MoveTowards(transform.position, _target2, step);
                float dist = Vector3.Distance(position, _target1);
                if (dist < 0.01f)
                {
                    Destroy(this);
                }
            }
            else
                ControlIfComplete();
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Reset();
                audioReset.Play();
                GetComponent<DrawLine>().ResetLine();
            }
        }

        void Reset()
        {
            for (int i = 0; i < cells.Length && !_open; i++) {
                cells[i].GetComponent<Collider>().isTrigger = true;
                cells[i].GetComponent<ChangeMaterialPath>().activate = false;
                cells[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = transparentMat;
            }
        }

        void ControlIfComplete()
        {
            int n = 0;
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].GetComponent<ChangeMaterialPath>().activate)
                    n++;
                else
                    break;
            }

            if (n == cells.Length)
            {
                for (int i = 0; i < n; i++)
                {
                    cells[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = rockGreenMat;
                    Destroy(cells[i].GetComponent<ChangeMaterialPath>());
                }
                OpenDoor();
            }
        }

        void OpenDoor()
        {
            if(audioWall!=null)
                audioWall.Play();
            _open = true;
        }
    }
}
