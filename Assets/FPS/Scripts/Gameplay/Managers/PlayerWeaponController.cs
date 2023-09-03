using UnityEngine;
using Unity.FPS.Bluetooth;

namespace Unity.FPS.Gameplay
{
    public class PlayerWeaponController : MonoBehaviour
    {
        public PlayerWeaponsManager firstWeaponManager;
        public PlayerWeaponsManager secondWeaponManager;

        private string difficulty;
        public enum WeaponState
        {
            SingleGun,
            DualGuns
        }
        public WeaponState currentWeaponState;

        void Update()
        {
            difficulty = DifficultyController.DifficultyLevel;
            if (difficulty == "easy")
            {
                SetWeaponState(WeaponState.DualGuns);
            }
            else
            {
                SetWeaponState(WeaponState.SingleGun);
            }
        }

        //private void OnEnable()
        //{
        //    HeartRateDDA heartRateInstance = FindObjectOfType<HeartRateDDA>();
        //    if (heartRateInstance != null)
        //    {
        //        heartRateInstance.OnDifficultyChanged += HandleDifficultyChanged;
        //    }
        //}

        //private void OnDisable()
        //{
        //    HeartRateDDA heartRateInstance = FindObjectOfType<HeartRateDDA>();
        //    if (heartRateInstance != null)
        //    {
        //        heartRateInstance.OnDifficultyChanged -= HandleDifficultyChanged;
        //    }
        //}

        //private void HandleDifficultyChanged(string difficulty)
        //{
        //    if (difficulty == "easy")
        //    {
        //        SetWeaponState(WeaponState.DualGuns);
        //    }
        //    else
        //    {
        //        SetWeaponState(WeaponState.SingleGun);
        //    }
        //}

        public void SetWeaponState(WeaponState state)
        {
            currentWeaponState = state;

            switch (state)
            {
                case WeaponState.SingleGun:
                    firstWeaponManager.enabled = true;
                    secondWeaponManager.enabled = false;
                    break;
                case WeaponState.DualGuns:
                    firstWeaponManager.enabled = true; 
                    secondWeaponManager.enabled = true;
                    firstWeaponManager.AimingWeaponPosition = firstWeaponManager.DefaultWeaponPosition;
                    break;
            }
        }
    }
}
