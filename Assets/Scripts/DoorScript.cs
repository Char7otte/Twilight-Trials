using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int PointToUnlock;

    public GameObject text;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player")) 
        {
            if (GameManager.instance.GetPointCount() >= PointToUnlock)
            {
                AudioManager.instance.PlayDoorSfx();
                Destroy(gameObject);
            }
            else 
            {
                AudioManager.instance.PlayErrorSfx();
                StartCoroutine(SpawnText());
            }
        }
    }

    IEnumerator SpawnText() {
        text.SetActive(true);
        yield return new WaitForSeconds(3);
        text.SetActive(false);
    }
}
