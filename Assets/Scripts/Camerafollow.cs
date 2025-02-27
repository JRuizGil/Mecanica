using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    private bool isPlayerActive = false;

    void Update()
    {
        BuscarJugador();
        transform.LookAt(playerTransform);
    }
    void BuscarJugador()
    {        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {            
            playerTransform = player.transform;
            isPlayerActive = player.activeInHierarchy; 
        }
        else 
        {            
            isPlayerActive = false;
        }
    }
}
