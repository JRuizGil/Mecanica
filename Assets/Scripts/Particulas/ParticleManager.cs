using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gestion la emisi�n y el movimiento de part�culas.
public class ParticleManager : MonoBehaviour
{
    // Tipos de emisi�n de part�culas.
    public enum EmissionType
    {
        RandomSphere,
        RandomCircle,
        Upward,
        Forward
    }

    [Header("Part�culas")]
    public GameObject particulaSinRotarPrefab; 
    public Transform Posicion; //Posici�n desde donde se emiten las part�culas.

    public float vMin = 3f; 
    public float vMax = 6f; 

    public float aMin = -1f; 
    public float aMax = 1f; 

    public float tiempoDeVidamin = 2f; 
    public float tiempoDeVidaMax = 5f; 

    public int paticulasPool = 10; //Cantidad de part�culas en la pool.

    [Header("Emisi�n")]
    public EmissionType formaEmision = EmissionType.RandomSphere; //Forma de emisi�n de las part�culas.
    public bool IsActive = false; 

    protected List<ParticleEmmisor> particlePool = new List<ParticleEmmisor>(); // Pool de part�culas.

    //crea la pool de part�culas y comenzando la rutina de emisi�n.
    void Start()
    {
        CreateParticlePool();
        StartCoroutine(ParticleSpawnerRoutine());
    }

    //Crea y almacena un conjunto de part�culas desactivadas listas para usarse.
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

    //emite part�culas peri�dicamente si est� activado.
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


    //Forma de emisi�n.
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

    //Actualiza las part�culas activas en cada frame fixed.
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
