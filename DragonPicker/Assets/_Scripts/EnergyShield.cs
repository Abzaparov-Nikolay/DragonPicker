using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyShield : MonoBehaviour
{

    public TextMeshProUGUI scoreGT;
    public AudioSource audioSource;

    private void Start()
    {
        var scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        scoreGT.text = "0";
    }

    void Update()
    {
        var mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        var mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        var pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collided = collision.gameObject;
        if(collided.tag == "Dragon Egg")
        {
            Destroy(collided);
        }
        var score = int.Parse(scoreGT.text);
        score += 1;
        if(score == 1)
        {
            DragonPicker.SaveAchivement("You got one baby dragon");
        }
        scoreGT.text = score.ToString();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
