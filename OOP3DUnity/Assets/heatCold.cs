using UnityEngine;
using UnityEngine.PostProcessing;

public class heatCold : MonoBehaviour
{
    public float heat;
    public float cold;
    public float smooth = 0.1f;

    float currentTemperature = 0f;
    float targetTemperature = 0f;
    GameObject player;
    PostProcessingBehaviour postProcessingBehaviour;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player)
            postProcessingBehaviour = player.GetComponentInChildren<PostProcessingBehaviour>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Round(targetTemperature) != Mathf.Round(currentTemperature))
        {
            currentTemperature = Mathf.Lerp(currentTemperature, targetTemperature, smooth * Time.deltaTime);
            setTemperature(currentTemperature);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        targetTemperature = heat;
    }

    private void OnTriggerExit(Collider other)
    {
        targetTemperature = cold;
    }

    void setTemperature(float temperature)
    {
        if (postProcessingBehaviour)
        {
            ColorGradingModel.Settings colorGradingSettings = new ColorGradingModel.Settings();
            colorGradingSettings = postProcessingBehaviour.profile.colorGrading.settings;
            colorGradingSettings.basic.temperature = temperature;
            postProcessingBehaviour.profile.colorGrading.settings = colorGradingSettings;
        }
    }
}