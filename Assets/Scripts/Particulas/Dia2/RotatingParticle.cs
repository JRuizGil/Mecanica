using UnityEngine;

//Particula que rota sobre un eje.
public class RotatingParticle : ParticleEmmisor
{

    private Vector3 velocidadAngular; 
    private Vector3 aceleracionAngular; 
    private Vector3 ejeRotacion;
    private float inercia;

    //Inicializa la part�cula con par�metros de movimiento y rotaci�n.
    public void Initialize(Vector3 dir, float minSpeed, float maxSpeed,
                           float minAcc, float maxAcc, float minLife, float maxLife,
                           Vector3 startPosition, float minAngularSpeed, float maxAngularSpeed,
                           float minAngularAcc, float maxAngularAcc, float minMass, float maxMass,
                           Vector3 axis, Vector3 initialAngularImpulse)
    {
        //Llama al m�todo de inicializaci�n de la clase base.
        base.Initialize(dir, minSpeed, maxSpeed, minAcc, maxAcc, minLife, maxLife, startPosition);

        ejeRotacion = axis; // Establece el eje de rotaci�n.

        velocidadAngular = initialAngularImpulse; // Asigna la velocidad angular inicial.
        aceleracionAngular = ejeRotacion * Random.Range(minAngularAcc, maxAngularAcc); // Define la aceleraci�n angular.

        // Calcula la inercia de la part�cula basada en su masa.
        float mass = Random.Range(minMass, maxMass);
        inercia = (2 * mass) / 5;
    }

    // Actualiza la rotaci�n de la part�cula en cada frame.
    public void Rotacion(float deltaTime)
    {
        if (!IsActive) return; // Si la part�cula no est� activa, no actualiza su rotaci�n.

        // Aplica la aceleraci�n angular a la velocidad angular usando la inercia.
        velocidadAngular += (aceleracionAngular * deltaTime) / inercia;

        // Rota la part�cula en el espacio global.
        transform.Rotate(velocidadAngular * deltaTime, Space.World);
    }
}
