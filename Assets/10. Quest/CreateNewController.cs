using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewController : MonoBehaviour
{
    [SerializeField] Text roleText;
    [SerializeField] Text raceText;

    string[] listRole = { "Mage", "Warrior", "Hunter", "Assasin", "Paladin", "Barbarian" };
    string[] listRace = { "Human", "Dwarves", "Elf"};

    int indexRole = 0;
    int indexRace = 0;

    public void BtnPlusRole()
    {
        indexRole++;
        if (indexRole > listRole.Length-1)
        {
            indexRole = 0;
        }
        roleText.text = listRole[indexRole];
    }

    public void BtnMinusRole()
    {
        indexRole--;
        if (indexRole < 0)
        {
            indexRole = listRole.Length - 1;
        }
        roleText.text = listRole[indexRole];
    }

    public void BtnPlusRace()
    {
        indexRace++;
        if (indexRace > listRace.Length - 1)
        {
            indexRace = 0;
        }
        raceText.text = listRace[indexRace];
    }

    public void BtnMinusRace()
    {
        indexRace--;
        if (indexRace < 0)
        {
            indexRace = listRace.Length - 1;
        }
        raceText.text = listRace[indexRace];
    }
}
