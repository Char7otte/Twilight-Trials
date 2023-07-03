using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    public int PointToUnlock;
    //public GameObject[] doorList;

    private bool activated = true;
    public Animator lever_anim;

    public GameObject door;
    private Animator door_anim;

    
    // Start is called before the first frame update
    void Start()
    {
        //foreach (GameObject door in doorList) 
        //{
        //    door.SetActive(false);
        //}

        door_anim = door.GetComponent<Animator>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) 
        {
            if (GameManager.instance.GetPointCount() >= PointToUnlock)
            {
                AudioManager.instance.PlayDoorSfx();

                if (activated == false) {
                    activated = true;
                    lever_anim.SetBool("is_activated", true);
                    door_anim.SetBool("is_activated", true);
                }          
                else {
                    activated = false;
                    lever_anim.SetBool("is_activated", false);
                    door_anim.SetBool("is_activated", false);
                }
            }
            else
            {
                AudioManager.instance.PlayErrorSfx();
            }
        }
    }
}
