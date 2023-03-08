using UnityEngine;
using PostProcessing;
namespace General
{
    public static class DoNotDeleteInfo
    {
        private static int _levelNo; //number of level you completed
        private static bool _character; //false is male, true is girl
        private static int _sceneToLoad;

        public static int GetSceneToLoad()
        {
            return _sceneToLoad;
        }
        
        
        public static void SetSceneToLoad(int scene)
        {
            _sceneToLoad = scene;
        }
        public static bool GETCharacter()
        {
            return _character;
        }
        public static void SetCharacter(bool yourChoice)
        {
            _character = yourChoice;
        }
        public static int GETLevelNo()
        {
            return _levelNo;
        }
        public static void SetLevelNo(int newLevel)
        {
            _levelNo = newLevel;
        }
        public static void ResetInfo()
        {
            _levelNo = 0;
            _character = false;
            PostProcessing.ChangeProfile._currentProfileIndex=0;

    }
    }
}
