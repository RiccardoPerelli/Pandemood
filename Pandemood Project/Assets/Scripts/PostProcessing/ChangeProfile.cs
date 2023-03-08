using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessing
{
    public class ChangeProfile : MonoBehaviour
    {
        private PostProcessVolume _postProcessVolume;
        public static int _currentProfileIndex;
        private ColorGrading _previousGrading;
        [SerializeField] private int indexToChange;

        [SerializeField] private PostProcessProfile[] newProfile = new PostProcessProfile[5];

        // Start is called before the first frame update
        private void Start()
        {
            _postProcessVolume = GetComponent<PostProcessVolume>();
            _postProcessVolume.profile = newProfile[_currentProfileIndex];
        }

        public void Change()
        {
            if (indexToChange <= _currentProfileIndex) return;
            _currentProfileIndex = indexToChange;
        }
    }
}