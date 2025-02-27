using UnityEngine;
using System.Collections.Generic;

// Esta clase maneja la emisi�n de part�culas con rotaci�n.
public class RotatingParticleEmitter : ParticleManager
{
    [Header("Rotaci�n")]
    public float vAngularMin = 10f; 
    public float vAngularMax = 50f; 
    public float aAngularMin = -5f; 
    public float aAngularMax = 5f; 
    public float masaMin = 0.5f; 
    public float masaMax = 2f; 
    public bool rotacionAleatoria; //Rotacion aleatria activable.
    public Vector3 ejeRotacionAleatoria; //Ejes de rotacion aleatorios.

    [Header("Prefab de Part�cula receptora")]
    public GameObject particulaPrefab; //Prefab de la part�cula.

    //Crea y almacena un conjunto de part�culas que rotan.
    protected override void CreateParticlePool()
    {        
        if (particulaPrefab == null)
        {
            Debug.LogError("Prebab de particula rotatoria no asignada");
            return;
        }

        //Generador de particulas.
        for (int i = 0; i < paticulasPool; i++)
        {
            GameObject particula = Instantiate(particulaPrefab, Posicion.position, Quaternion.identity);

            //Intenta obtener el componente RotatingParticle, si no lo tiene, lo a�ade.
            RotatingParticle mover = particula.GetComponent<RotatingParticle>();
            if (mover == null)
            {
                mover = particula.AddComponent<RotatingParticle>();
            }
            mover.gameObject.SetActive(false);
            particlePool.Add(mover);
        }
    }

    //Activa y configura las part�culas inactivas de la pool con los par�metros de rotaci�n.
    protected override void emitirParticularotatoria()
    {
        foreach (var particle in particlePool)
        {
            if (!particle.IsActive)
            {
                Vector3 direction = GetEmissionDirection(); 
                Vector3 rotationAxis = ejeRotacionAleatoria.normalized; 
                Vector3 angularImpulse = rotacionAleatoria
                    ? rotationAxis * Random.Range(vAngularMin, vAngularMax) //Impulso aleatorio.
                    : rotationAxis;

                //Inicializa la part�cula con par�metros de rotaci�n generados aleatoriamente.
                ((RotatingParticle)particle).Initialize(
                    direction, vMin, vMax, aMin, aMax,
                    tiempoDeVidamin, tiempoDeVidaMax, Posicion.position,
                    vAngularMin, vAngularMax, aAngularMin, aAngularMax,
                    masaMin, masaMax, rotationAxis, angularImpulse
                );

                ((RotatingParticle)particle).ActivarP(true);
            }
        }
    }

    //Actualiza la posici�n y la rotaci�n de las part�culas activas en cada frame fixed.
    void FixedUpdate()
    {
        float deltaTime = Time.deltaTime;
        foreach (var particle in particlePool)
        {
            if (particle.IsActive)
            {
                particle.UpdateParticle(deltaTime);
                ((RotatingParticle)particle).Rotacion(deltaTime);
            }
        }
    }
}
