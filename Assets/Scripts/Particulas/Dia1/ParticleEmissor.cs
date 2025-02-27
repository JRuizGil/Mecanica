using UnityEngine;

// Esta clase maneja la emisi�n y movimiento de part�culas.
public class ParticleEmmisor : MonoBehaviour
{
    private Vector3 direccion; 
    private float Velocidad; 
    private float aceleracion; 
    private float lifeTime; 
    private float elapsedTime = 0f; 
    public bool IsActive { get; private set; } = false; //Estado de la part�cula.

    // Inicializa la part�cula con valores aleatorios dentro de los rangos establecidos.
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

    // Actualiza la posici�n y velocidad de la part�cula en cada frame.
    public void UpdateParticle(float deltaTime)
    {
        if (!IsActive) return; // Si no est� activa, no hace nada.

        transform.position += direccion * Velocidad * deltaTime; // Movimiento en la direcci�n establecida.
        Velocidad += aceleracion * deltaTime; // Se actualiza la velocidad con la aceleraci�n.

        elapsedTime += deltaTime; // Aumenta el tiempo transcurrido.
        if (elapsedTime >= lifeTime) // Si supera su tiempo de vida, se reinicia.
        {
            ResetP();
        }
    }

    // Activa o desactiva la part�cula.
    public void ActivarP(bool state)
    {
        IsActive = state;
    }

    // Desactiva y resetea la part�cula.
    public void ResetP()
    {
        IsActive = false;
        gameObject.SetActive(false);
    }
}
