using UnityEngine;

namespace OverBridge
{
    public class PlayerInput : MonoBehaviour
    {
        private BridgeController _bridgeController;

        void Awake()
        {
            _bridgeController = FindObjectOfType<BridgeController>();
        }

        void Update()
        {
            if (Input.touchCount > 0 || Input.GetKey(KeyCode.Mouse0))
            {
                _bridgeController.IsBuild = true;
            }
            else
            {
                _bridgeController.IsBuild = false;
            }
        }
    }
}

