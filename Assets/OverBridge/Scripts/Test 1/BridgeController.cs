using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace OverBridge
{
    public class BridgeController : MonoBehaviour
    {
        public UnityEvent BridgeDown;

        private PlayerInput _playerInput;
        float currentLength = 0f;
        float buildSpeed = 1f;
        public bool IsBuild = false;
        bool isBridgeCompleate = false;
        [SerializeField] private GameObject bridgeVisual;
        [SerializeField] private CharackterController _charackterController;

        void Awake()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            Debug.Log(_playerInput == false ? "Объект не найден" : "Объект найден");
        }

        void FixedUpdate()
        {
            if (IsBuild && bridgeVisual.transform.localScale.y <= 0f)
            {
                BuildBridge();
            }
            else if (IsBuild && bridgeVisual.transform.localScale.y > 0f)
            {
                BuildBridge();
            }
            else if (!IsBuild && bridgeVisual.transform.localScale.y > 0f)
            {
                if (!isBridgeCompleate)
                {
                    DownBridge();
                }                
            }
        }

        void BuildBridge()
        {
            currentLength += buildSpeed * Time.fixedDeltaTime;

            bridgeVisual.transform.localScale = new Vector3(1f, currentLength, 1f);
        }

        void DownBridge()
        {
            Debug.Log("Мост падает");

            
            float currentZAngle = bridgeVisual.transform.eulerAngles.z;
            
            if (currentZAngle > 180f) currentZAngle -= 360f;

            if (currentZAngle <= -90f)
            {
                Debug.Log("Мост полностью опустился");
                BridgeDown.Invoke();
                Debug.Log("Персонаж начал идти прямо");
                isBridgeCompleate = true;
                return;
            }

            // Плавное вращение
            float rotation = 30f * Time.fixedDeltaTime;
            bridgeVisual.transform.Rotate(0, 0, -rotation);
        }

        public void DeffoltBridge()
        {
            bridgeVisual.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            bridgeVisual.transform.localScale = new Vector3(1f, 0f, 1f);
            currentLength = 0f;

            bridgeVisual.transform.position = _charackterController.buildBridgePoin.position;

            isBridgeCompleate = false;
        }
    }
}


