using UnityEngine;
using Unity.FPS.Gameplay;
using Unity.FPS.Game;
public class DamageAdjuster : MonoBehaviour
{
    ProjectileBase m_ProjectileBase;

    void OnEnable()
    {
        m_ProjectileBase = GetComponent<ProjectileBase>();
        DebugUtility.HandleErrorIfNullGetComponent<ProjectileBase, ProjectileChargeParameters>(m_ProjectileBase,
            this, gameObject);

        m_ProjectileBase.OnShoot += OnShoot;
    }
    void OnShoot()
    {
        // Apply the parameters based on projectile charge
        ProjectileStandard proj = GetComponent<ProjectileStandard>();
        if (proj)
        {
            string Difficulty = DifficultyController.Instance.DifficultyLevel;
            if (Difficulty == "hard")
            {

                proj.Damage *= 1.5f;
                proj.Speed *= 1.5f;
                proj.Radius *= 1.5f;
                Debug.Log("Damage Adjusted to hard mode.");
            }
            else
            {
                proj.Damage *= 1f;
                proj.Speed *= 1f;
                proj.Radius *= 1f;
            }
        }
        else
             Debug.Log("No ProjectileStandard Found.");
    }
}
