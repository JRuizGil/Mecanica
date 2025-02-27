using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gestion la emisión y el movimiento de partículas.
public class ParticleManager : MonoBehaviour
{
    // Tipos de emisión de partículas.
    public enum EmissionType
    {
        RandomSphere,
        RandomCircle,
        Upward,
        Forward
    }

    [Header("Partículas")]
    public GameObject particulaSinRotarPrefab; 
    public Transform Posicion; //Posición desde donde se emiten las partículas.

    public float vMin = 3f; 
    public float vMax = 6f; 

    public float aMin = -1f; 
    public float aMax = 1f; 

    public float tiempoDeVidamin = 2f; 
    public float tiempoDeVidaMax = 5f; 

    public int paticulasPool = 10; //Cantidad de partículas en la pool.

    [Header("Emisión")]
    public EmissionType formaEmision = EmissionType.RandomSphere; //Forma de emisión de las partículas.
    public bool IsActive = false; 

    protected List<ParticleEmmisor> particlePool = new List<ParticleEmmisor>(); // Pool de partículas.

    //crea la pool de partículas y comenzando la rutina de emisión.
    void Start()
    {
        CreateParticlePool();
        StartCoroutine(ParticleSpawnerRoutine());
    }

    //Crea y almacena un conjunto de partículas desactivadas listas para usarse.
    protected virtual void CreateParticlePool()
    {
        for (int i = 0; i < paticulasPool; i++)
        {
            GameObject particula = Instantiate(particulaSinRotarPrefab, Posicion.position, Quaternion.identity);
            ParticleEmmisor mover = particula.AddComponent<ParticleEmmisor>();
            mover.gameObject.SetActive(false);
            particlePool.Add(mover);
        }
    }

    //emite partículas periódicamente si está activado.
    private IEnumerator ParticleSpawnerRoutine()
    {
        while (true)
        {
            if (IsActive)
            {
                emitirParticularotatoria();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    //Activado y variables de la particula de la pool.
    protected virtual void emitirParticularotatoria()
    {
        int activeCount = 0;

        foreach (var particle in particlePool)
        {
            if (!particle.IsActive)
            {
                Vector3 direction = GetEmissionDirection();
                particle.gameObject.SetActive(true);
                particle.Initialize(direction, vMin, vMax, aMin, aMax, tiempoDeVidamin, tiempoDeVidaMax, Posicion.position);
                activeCount++;
            }
        }
    }


    //Forma de emisión.
    public Vector3 GetEmissionDirection()
    {
        switch (formaEmision)
        {
            case EmissionType.RandomSphere:
                return Random.insideUnitSphere.normalized;

            case EmissionType.RandomCircle:
                return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

            case EmissionType.Upward:
                return Vector3.up;

            case EmissionType.Forward:
                return transform.forward;

            default:
                return Random.insideUnitSphere.normalized;
        }
    }

    //Actualiza las partículas activas en cada frame fixed.
    void FixedUpdate()
    {
        float deltaTime = Time.deltaTime;
        foreach (var particle in particlePool)
        {
            if (particle.IsActive)
            {
                particle.UpdateParticle(deltaTime);
            }
        }
    }
}
