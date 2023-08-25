using UnityEngine;
using Unity.FPS.Game;
public class EndLoading : MonoBehaviour
{
    public HeartRateService heartRateService;
    public PanelController panel;

    void Start()
    {
        heartRateService.OnConnected += panel.HideLoadingUI;
    }

    private void OnDestroy()
    {
        heartRateService.OnConnected -= panel.HideLoadingUI;
    }

}