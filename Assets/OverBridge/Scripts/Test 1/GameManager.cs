using UnityEngine;
namespace OverBridge
{
    public class GameManager : MonoBehaviour
    {
        public static int Score = 0;

        private void Awake()
        {
            Score = 0;
        }
    }
}

