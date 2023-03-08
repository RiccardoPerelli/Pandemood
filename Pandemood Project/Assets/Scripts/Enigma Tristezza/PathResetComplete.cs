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
<<<<<<< .merge_file_a16820
        private bool _hasAudiowall;
=======

>>>>>>> .merge_file_a20140
        private bool _reset;

        private bool _open;
        private Vector3 _target1,_target2;

        private void Start()
        {
            var position = rockDoor.transform.position;
            _target1 = new Vector3(position.x, position.y, position.z + 3f);
            var position1 = transform.position;
            _target2 = new Vector3(position1.x, position1.y, position1.z + 3f);
<<<<<<< .merge_file_a16820
            _hasAudiowall = audioWall != null;
=======
>>>>>>> .merge_file_a20140
        }

        private void Update()
        {
<<<<<<< .merge_file_a16820
            if (Input.GetKeyDown(KeyCode.F8))
            {
                OpenDoor();
            }
            if (_open)
            {
                var step = 3f * Time.deltaTime; // calculate distance to move
=======
            if (_open)
            {
                float step = 3f * Time.deltaTime; // calculate distance to move
>>>>>>> .merge_file_a20140
                var position = rockDoor.transform.position;
                position = Vector3.MoveTowards(position, _target1, step);
                rockDoor.transform.position = position;
                transform.position = Vector3.MoveTowards(transform.position, _target2, step);
<<<<<<< .merge_file_a16820
                var dist = Vector3.Distance(position, _target1);
=======
                float dist = Vector3.Distance(position, _target1);
>>>>>>> .merge_file_a20140
                if (dist < 0.01f)
                {
                    Destroy(this);
                }
            }
            else
                ControlIfComplete();
        }

<<<<<<< .merge_file_a16820
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("PlayerPath")) return;
            Reset();
            audioReset.Play();
            GetComponent<DrawLine>().ResetLine();
        }

        private void Reset()
        {
            for (var i = 0; i < cells.Length && !_open; i++) {
=======
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
>>>>>>> .merge_file_a20140
                cells[i].GetComponent<Collider>().isTrigger = true;
                cells[i].GetComponent<ChangeMaterialPath>().activate = false;
                cells[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = transparentMat;
            }
        }

<<<<<<< .merge_file_a16820
        private void ControlIfComplete()
        {
            var n = 0;
            foreach (var t in cells)
            {
                if (t.GetComponent<ChangeMaterialPath>().activate)
=======
        void ControlIfComplete()
        {
            int n = 0;
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].GetComponent<ChangeMaterialPath>().activate)
>>>>>>> .merge_file_a20140
                    n++;
                else
                    break;
            }

<<<<<<< .merge_file_a16820
            if (n != cells.Length) return;
            for (var i = 0; i < n; i++)
            {
                cells[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = rockGreenMat;
                Destroy(cells[i].GetComponent<ChangeMaterialPath>());
            }
            OpenDoor();
=======
            if (n == cells.Length)
            {
                for (int i = 0; i < n; i++)
                {
                    cells[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = rockGreenMat;
                    Destroy(cells[i].GetComponent<ChangeMaterialPath>());
                }
                OpenDoor();
            }
>>>>>>> .merge_file_a20140
        }

        void OpenDoor()
        {
<<<<<<< .merge_file_a16820
            if(_hasAudiowall)
=======
            if(audioWall!=null)
>>>>>>> .merge_file_a20140
                audioWall.Play();
            _open = true;
        }
    }
}
