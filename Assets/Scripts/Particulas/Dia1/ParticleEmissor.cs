using UnityEngine;

// Esta clase maneja la emisión y movimiento de partículas.
public class ParticleEmmisor : MonoBehaviour
{
    private Vector3 direccion; 
    private float Velocidad; 
    private float aceleracion; 
    private float lifeTime; 
    private float elapsedTime = 0f; 
    public bool IsActive { get; private set; } = false; //Estado de la partícula.

    // Inicializa la partícula con valores aleatorios dentro de los rangos establecidos.
    public void Initialize(Vector3 dir, float minSpeed, float maxSpeed,
                           float minAcc, float maxAcc, float minLife, float maxLife,
                           Vector3 startPosition)
    {
        direccion = dir;
        Velocidad = Random.Range(minSpeed, maxSpeed);
        aceleracion = Random.Range(minAcc, maxAcc);
        lifeTime = Random.Range(minLife, maxLife);
        elapsedTime = 0f;
        transform.position = startPosition;
        IsActive = true;
        gameObject.SetActive(true);
    }

    // Actualiza la posición y velocidad de la partícula en cada frame.
    public void UpdateParticle(float deltaTime)
    {
        if (!IsActive) return; // Si no está activa, no hace nada.

        transform.position += direccion * Velocidad * deltaTime; // Movimiento en la dirección establecida.
        Velocidad += aceleracion * deltaTime; // Se actualiza la velocidad con la aceleración.

        elapsedTime += deltaTime; // Aumenta el tiempo transcurrido.
        if (elapsedTime >= lifeTime) // Si supera su tiempo de vida, se reinicia.
        {
            ResetP();
        }
    }

    // Activa o desactiva la partícula.
    public void ActivarP(bool state)
    {
        IsActive = state;
    }

    // Desactiva y resetea la partícula.
    public void ResetP()
    {
        IsActive = false;
        gameObject.SetActive(false);
    }
}
