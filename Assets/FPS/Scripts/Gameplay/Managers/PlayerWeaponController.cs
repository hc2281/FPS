using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class PlayerWeaponController : MonoBehaviour
    {
        public PlayerWeaponsManager firstWeaponManager;
        public PlayerWeaponsManager secondWeaponManager;
        public AudioClip dualGunsEquipSound;
        private AudioSource audioSource;
        public enum WeaponState
        {
            SingleGun,
            DualGuns
        }
        public WeaponState currentWeaponState;
        public DifficultyController difficultyController;
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            SetWeaponState(WeaponState.SingleGun);
        }

        // Update is called once per frame
        void Update()
        {
            if (difficultyController.DifficultyLevel == "easy")
            {
                SetWeaponState(WeaponState.DualGuns);
            }
            else
            {
                SetWeaponState(WeaponState.SingleGun);
            }
        }

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
                    
                    // Player the audio
                    if (dualGunsEquipSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(dualGunsEquipSound);
                    }
                    break;
            }
        }
    }
}
