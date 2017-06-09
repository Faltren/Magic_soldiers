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
    public int level;

	void Start ()
    {
        float r = 255f;
        float g = 150f;
        float b = 0f;
        float a = 255f;
        IntensityLight = 0.01f;

        TorchLight.GetComponent<Light>().color = new Color(r, g, b, a);
        TorchLight.GetComponent<Light>().range = 33;
        TorchLight.GetComponent<Light>().intensity = IntensityLight;

        MainFlame.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 20f;
        BaseFlame.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 15f;
        Etincelles.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 7f;
        Fumee.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 12f;
	}
	

	void Update ()
    {
        if (Getlevel() == level)
        {
            IntensityLight = 0.01f;
            MaxLightIntensity = 0.005f;

            TorchLight.GetComponent<Light>().intensity = Mathf.Lerp(IntensityLight, MaxLightIntensity, Mathf.Cos(Time.time * 20));
            TorchLight.GetComponent<Light>().range = 33;
            TorchLight.GetComponent<Light>().color = new Color(255f, 150f, 0f, 255f);

            MainFlame.GetComponent<ParticleSystem>().emissionRate = 50 * 20f;
            BaseFlame.GetComponent<ParticleSystem>().emissionRate = 50 * 15f;
            Etincelles.GetComponent<ParticleSystem>().emissionRate = 50 * 7f;
            Fumee.GetComponent<ParticleSystem>().emissionRate = 50 * 12f;
        }
        else
        {
            TorchLight.GetComponent<Light>().intensity = 0;
        }
        

	}


    private int Getlevel()
    {
        switch (Canvas_UI_Online.levelName)
        {
            case "Level1":
                return 1;
            case "Level2":
                return 2;
            case "Level3":
                return 3;
            case "Level4":
                return 4;
            case "Level5":
                return 5;
            case "Level6":
                return 6;
            case "Level7":
                return 7;
            case "Level8":
                return 8;
            default:
                return 1;
        }
    
    }

}


