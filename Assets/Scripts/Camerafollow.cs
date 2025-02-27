using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    private bool isPlayerActive = false;

    void Update()
    {
        // Si el jugador no está asignado o no está activo, buscarlo en la escena
        if (playerTransform == null || !isPlayerActive)
        {
            BuscarJugador();
        }

        // Si el jugador ha sido encontrado y está activo, hacer que la cámara lo mire
        if (playerTransform != null && isPlayerActive)
        {
            transform.LookAt(playerTransform);
        }
    }

    void BuscarJugador()
    {
        // Buscar el objeto con el tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Asignar el transform del jugador y marcarlo como activo
            playerTransform = player.transform;
            isPlayerActive = player.activeInHierarchy; // Solo se marca como activo si el objeto está activo
        }
        else
        {
            // Si no encuentra el jugador, marcar que no está activo
            isPlayerActive = false;
        }
    }
}
