using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Blade.Players
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controlls.IPlayerActions
    {
        [SerializeField] private LayerMask whatIsGround;
        
        public event Action OnAttackPressed;
        public event Action OnRollingPressed;
        public event Action<bool> OnSkillPressed;

        public Vector2 MovementKey { get; private set; }
        private Controlls _controls;

        private Vector3 _worldPosition; //이게 마우스의 월드 좌표
        private Vector2 _screenPosition; //이게 마우스가 위치한 화면좌표

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controlls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public Vector3 GetWorldPosition()
        {
            Camera mainCam = Camera.main; //Unity2022부터 내부 캐싱이 되서 그냥 써도 돼.
            Debug.Assert(mainCam != null, "No main camera in this scene");
            
            Ray cameraRay = mainCam.ScreenPointToRay(_screenPosition);
            if (Physics.Raycast(cameraRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
            {
                _worldPosition = hit.point;
            }

            return _worldPosition;
        }
        
        public void OnPointer(InputAction.CallbackContext context)
        {
            _screenPosition = context.ReadValue<Vector2>();
        }
    }
}