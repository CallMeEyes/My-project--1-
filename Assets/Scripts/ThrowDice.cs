using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowDice : MonoBehaviour
{
    [SerializeField] private Image dice1;
    [SerializeField] private Image dice2;

    // Sprites for each dice face
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;

    private int dice1Numb = 0;
    private int dice2Numb = 0;

    private System.Random rng;

    private void Start()
    {
        rng = new System.Random();
    }

    // Method called when the dice roll button is clicked
    public void OnButtonClick()
    {
        RollDice();
        UpdateDiceSprites();
    }

    // Roll both dice
    private void RollDice()
    {
        dice1Numb = rng.Next(1, 7);
        dice2Numb = rng.Next(1, 7);
    }

    // Update the sprites based on the dice roll
    private void UpdateDiceSprites()
    {
        dice1.sprite = GetSpriteForNumber(dice1Numb);
        dice2.sprite = GetSpriteForNumber(dice2Numb);
    }

    // Get the appropriate sprite for a given dice number
    private Sprite GetSpriteForNumber(int number)
    {
        switch (number)
        {
            case 1: return sprite1;
            case 2: return sprite2;
            case 3: return sprite3;
            case 4: return sprite4;
            case 5: return sprite5;
            case 6: return sprite6;
            default: throw new ArgumentOutOfRangeException(nameof(number), "Dice number must be between 1 and 6");
        }
    }

    // Getter for dice1 value
    public int GetDice1()
    {
        return dice1Numb;
    }

    // Getter for dice2 value
    public int GetDice2()
    {
        return dice2Numb;
    }
}
