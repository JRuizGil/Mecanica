using UnityEngine;
using System.Collections.Generic;

// Esta clase maneja la emisión de partículas con rotación.
public class RotatingParticleEmitter : ParticleManager
{
    [Header("Rotación")]
    public float vAngularMin = 10f; 
    public float vAngularMax = 50f; 
    public float aAngularMin = -5f; 
    public float aAngularMax = 5f; 
    public float masaMin = 0.5f; 
    public float masaMax = 2f; 
    public bool rotacionAleatoria; //Rotacion aleatria activable.
    public Vector3 ejeRotacionAleatoria; //Ejes de rotacion aleatorios.

    [Header("Prefab de Partícula receptora")]
    public GameObject particulaPrefab; //Prefab de la partícula.

    //Crea y almacena un conjunto de partículas que rotan.
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

            //Intenta obtener el componente RotatingParticle, si no lo tiene, lo añade.
            RotatingParticle mover = particula.GetComponent<RotatingParticle>();
            if (mover == null)
            {
                mover = particula.AddComponent<RotatingParticle>();
            }
            mover.gameObject.SetActive(false);
            particlePool.Add(mover);
        }
    }

    //Activa y configura las partículas inactivas de la pool con los parámetros de rotación.
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

                //Inicializa la partícula con parámetros de rotación generados aleatoriamente.
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

    //Actualiza la posición y la rotación de las partículas activas en cada frame fixed.
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
