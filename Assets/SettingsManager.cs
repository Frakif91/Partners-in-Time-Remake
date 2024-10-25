using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown characterDropdown;

    void Start()
    {
        // Charger la sélection précédente
        int characterType = PlayerPrefs.GetInt("CharacterType", 2); // 2D par défaut
        characterDropdown.value = characterType == 2 ? 0 : 1;

        // Ajouter un listener pour gérer les changements
        characterDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(characterDropdown);
        });
    }

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0)
        {
            PlayerPrefs.SetInt("CharacterType", 2);
        }
        else if (dropdown.value == 1)
        {
            PlayerPrefs.SetInt("CharacterType", 3);
        }
    }

    public int GetCharacterType()
    {
        return PlayerPrefs.GetInt("CharacterType", 2); // 2D par défaut
    }
}