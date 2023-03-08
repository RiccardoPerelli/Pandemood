using UnityEngine;

namespace General
{
    public class DoNotDeleteInfo : MonoBehaviour
    {
        private static int _levelNo; //number of level you completed
        private static bool _character; //false is male, true is girl
        // Start is called before the first frame update
       
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
    }
}
