using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject character2D;
    public GameObject character3D;

    void Start()
    {
        int characterType = PlayerPrefs.GetInt("CharacterType", 2); // 2D par défaut

        if (characterType == 2)
        {
            character2D.SetActive(true);
            character3D.SetActive(false);
        }
        else if (characterType == 3)
        {
            character2D.SetActive(false);
            character3D.SetActive(true);
        }
    }
}