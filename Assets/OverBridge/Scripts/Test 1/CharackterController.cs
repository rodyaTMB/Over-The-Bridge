
using UnityEngine;
using UnityEngine.Events;

namespace OverBridge
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharackterController : MonoBehaviour
    {
        [SerializeField] private BridgeController _bridgeController;
        [SerializeField] private GameObject bridgeVisual;
        [SerializeField] private float speed;

        public Transform buildBridgePoin;

        private HUDCOntroller _hud;

        private bool isMovement = false;

        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        void Start()
        {
            _bridgeController.BridgeDown.AddListener(MovementPlayer);
            _hud = GameObject.FindFirstObjectByType<HUDCOntroller>();

            _bridgeController.DeffoltBridge();
        }

        private void FixedUpdate()
        {
            Vector3 exitBridgePos = bridgeVisual.transform.GetChild(1).transform.position;
            if (Vector3.Distance(transform.position, exitBridgePos) < 0.1f)
            {
                _bridgeController.DeffoltBridge();
            }
        }

        void OnCollisionExit2D(Collision2D collsion)
        {
            if (collsion.gameObject.CompareTag("Bridge"))
            {
                rb.linearVelocityX = 0;
            }
        }

        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if (other.gameObject.CompareTag("Island"))
        //     {
        //         _bridgeController.DeffoltBridge();
        //     }
        // }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("AddScore"))
            {
                GameManager.Score++;
                _hud.UPDScore();
            }
        }

        void MovementPlayer()
        {
            rb.linearVelocityX = speed;
        }
    }
}

