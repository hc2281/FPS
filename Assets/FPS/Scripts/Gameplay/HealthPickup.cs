using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class HealthPickup : Pickup
    {
        [Header("Parameters")] [Tooltip("Amount of health to heal on pickup")]
        private int HealAmount;
        protected override void OnPicked(PlayerCharacterController player)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth && playerHealth.CanPickup())
            {
                string currentDifficulty = DifficultyController.DifficultyLevel;
                switch (currentDifficulty)
                {
                    case "easy":
                        HealAmount = 30;
                        break;
                    case "medium":
                        HealAmount = 25;
                        break;
                    case "hard":
                        HealAmount = 20;
                        break;
                }
                playerHealth.Heal(HealAmount);
                PlayPickupFeedback();
                Destroy(gameObject);
            }
        }
    }
}