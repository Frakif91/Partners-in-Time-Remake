using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBlock : MonoBehaviour
{
    public string blockName = string.Empty;
    public int blockIndex = 0;
    public bool is_current = false;
    private GameObject block_manager; //Parent d'un bloc

    // Start is called before the first frame update
    void Start()
    {
        block_manager = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {

    }

    void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject is BattleBrother) ;
    }

    void OnCollisionEnter(Collision collision)
    {

    }
}
