using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class HealthPickup : Pickup
    {
        [Header("Parameters")] [Tooltip("Amount of health to heal on pickup")]
        private int HealAmount;
        public MeshRenderer HealthPickupRenderer;
        public Material GreenMaterial;
        public Material RedMaterial;
        protected override void OnPicked(PlayerCharacterController player)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth && playerHealth.CanPickup())
            {
                string currentDifficulty = DifficultyController.Instance.DifficultyLevel;
                switch (currentDifficulty)
                {
                    case "easy":
                        HealAmount = 30;
                        HealthPickupRenderer.material = RedMaterial;
                        break;
                    case "medium":
                        HealAmount = 20;
                        break;
                    case "hard":
                        HealthPickupRenderer.material = GreenMaterial;
                        HealAmount = 10;
                        break;
                }
                playerHealth.Heal(HealAmount);
                PlayPickupFeedback();
                Destroy(gameObject);
            }
        }
    }
}