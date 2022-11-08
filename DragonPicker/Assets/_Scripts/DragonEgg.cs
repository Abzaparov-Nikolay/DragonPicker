using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEgg : MonoBehaviour
{
    public AudioSource audioSource;
    public static float bottomY = -18;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();

        var rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    void Update()
    {
        if(transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
            DragonPicker apScript = Camera.main.GetComponent<DragonPicker>();
            apScript.DragonEggDestroyed();
        }
    }
}
