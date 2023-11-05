using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDcontroller : MonoBehaviour
{
    // Components
    [SerializeField] private Image[] hearths;
    [SerializeField] private Sprite[] hearthStateSprites;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        FullHealthPlayer();
    }

    private void Update()
    {
        UpdateHealthPlayer();
    }

    private void FullHealthPlayer()
    {
        for (int i = 0; i < hearths.Length; i++)
        {
            hearths[i].sprite = hearthStateSprites[0];
        }
    }

    private void UpdateHealthPlayer()
    {
        int i = player.GetComponent<PlayerController>().health % 2;
        int j = player.GetComponent<PlayerController>().health / 2;

        if (i == 0 && j != 4)
        {
            hearths[j].sprite = hearthStateSprites[2]; // Vacio
        }
        else if (i == 1)
        {
            hearths[j].sprite = hearthStateSprites[1]; // Mitad
        }
    }
    private void OnGUI()
    {
        scoreText.text = (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().pickUps * 50).ToString("0000");

    }
}
