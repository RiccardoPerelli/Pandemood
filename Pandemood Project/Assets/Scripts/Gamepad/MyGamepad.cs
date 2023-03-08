using XInputDotNetPure;

namespace Gamepad
{
    public static class MyGamepad
    {
        public const float DoorVibration = 0.6f;
        public const float StartCrack = 0.3f;
        public const float Pushing = 0.5f;
        public const float MachineVibration = 0.5f;
        public static readonly float[] CrackingIncrease = {0.5f, 0.7f, 1f, 1.5f};

        private static float _savedLeft, _savedRight;
        private static bool _isVibrating;

        private static PlayerIndex? FetchPlayerIndex()
        {
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                return PlayerIndex.One;
            if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                return PlayerIndex.Two;
            if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                return PlayerIndex.Three;
            if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                return PlayerIndex.Four;
            return null;
        }

        public static void SetVibration(float left, float right)
        {
            _isVibrating = true;
            var index = FetchPlayerIndex();
            _savedLeft = left;
            _savedRight = right;
            if (index.HasValue)
            {
                GamePad.SetVibration(index.Value, left, right);
            }
        }

        public static void ResumeVibration()
        {
            if (_isVibrating)
                SetVibration(_savedLeft, _savedRight);
        }

        public static void SetVibration(float value)
        {
            SetVibration(value, value);
        }

        public static bool IsVibrating()
        {
            return _isVibrating;
        }

        public static bool IsGamepadConnected()
        {
            return FetchPlayerIndex().HasValue;
        }

        public static void StopVibration()
        {
            _isVibrating = false;
            var index = FetchPlayerIndex();
            if (index.HasValue)
            {
                GamePad.SetVibration(index.Value, 0, 0);
            }
        }

        public static void PauseVibration()
        {
            var index = FetchPlayerIndex();
            if (index.HasValue)
            {
                GamePad.SetVibration(index.Value, 0, 0);
            }
        }
    }
}