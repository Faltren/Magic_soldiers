using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour {
	
	public GameObject TorchLight;
	public GameObject MainFlame;
	public GameObject BaseFlame;
	public GameObject Etincelles;
	public GameObject Fumee;
    public float MaxLightIntensity;
    public float IntensityLight;
	

	void Start ()
    {
        float r = 0f;
        float g = 180f;
        float b = 255f;
        float a = 255f;

        TorchLight.GetComponent<Light>().color = new Color(r, g, b, a);
        TorchLight.GetComponent<Light>().range = 10;
        TorchLight.GetComponent<Light>().intensity = IntensityLight;

        MainFlame.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 20f;
        BaseFlame.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 15f;
        Etincelles.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 7f;
        Fumee.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 12f;
	}
	

	void Update ()
    {
        TorchLight.GetComponent<Light>().intensity = Mathf.Lerp(0.005f, 0.0075f, Mathf.Cos(Time.time * 10));
        TorchLight.GetComponent<Light>().range = 10;
        TorchLight.GetComponent<Light>().color = new Color(255f, 150f, 0f, 255f);

        MainFlame.GetComponent<ParticleSystem>().emissionRate = 50 * 20f;
        BaseFlame.GetComponent<ParticleSystem>().emissionRate = 50 * 15f;
        Etincelles.GetComponent<ParticleSystem>().emissionRate = 50 * 7f;
        Fumee.GetComponent<ParticleSystem>().emissionRate = 50 * 12f;

	}
}
