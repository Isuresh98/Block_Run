using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamColorChange : MonoBehaviour
{
    public Color bloomColor = Color.white;
    public float bloomIntensity = 1.0f;

    private Bloom bloomEffect;
    public float bloomSoftKnee = 1f; // adjust the soft knee value


    private void Start()
    {
        // Get the PostProcessingBehaviour component attached to the Camera
        PostProcessVolume postProcessVolume = GetComponent<PostProcessVolume>();

        // Get the Bloom effect from the PostProcessingBehaviour component
        postProcessVolume.profile.TryGetSettings(out bloomEffect);
        bloomEffect.softKnee.value = bloomSoftKnee;
    }

    private void Update()
    {
        float min = 0.5f;
        float max = 1.0f;
        float green = Random.Range(min, max);
        float blue = Random.Range(min, max);
        bloomColor = new Color(1.0f, green, blue);
        // Generate random color
       // bloomColor = new Color(Random.value, Random.value, Random.value);

        // Generate random intensity value between 0.5 and 2.0
        bloomIntensity = Random.Range(100f, 150f);

        
    }

   public void ColorAndIntensity()
    {
       

        // Set the bloom color and intensity values
        bloomEffect.color.value = bloomColor;
        bloomEffect.intensity.value = bloomIntensity;
    }
}
