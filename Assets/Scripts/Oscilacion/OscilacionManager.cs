using UnityEngine;

public class OscilacionManager : MonoBehaviour
{
    [Header("Objeto 1 - Oscilación")]
    [SerializeField] private float amplitud1 = 1.0f;
    [SerializeField] private float frecuencia1 = 1.0f;

    [Header("Objeto 2 - Oscilación")]
    [SerializeField] private float amplitud2 = 1.5f;
    [SerializeField] private float frecuencia2 = 0.8f;

    [Header("Referencia al Interpolador (Solo Objeto 1)")]
    [SerializeField] private Interpolador interpolador;

    [Header("Objeto Base para Oscilación")]
    [SerializeField] private Transform objetoReferencia;

    [Header("Prefabs de los Objetos")]
    [SerializeField] private GameObject prefabObjeto1;
    [SerializeField] private GameObject prefabObjeto2;

    [Header("Duración aleatoria")]
    [SerializeField] private float duracionMinima = 1f;
    [SerializeField] private float duracionMaxima = 3f;

    private GameObject objeto1Instanciado;
    private GameObject objeto2Instanciado;

    void Start()
    {
        objeto1Instanciado = Instantiate(prefabObjeto1, transform.position, Quaternion.identity);
        objeto2Instanciado = Instantiate(prefabObjeto2, transform.position, Quaternion.identity);
        objetoReferencia = objeto1Instanciado.transform;

        ConfigurarNuevoCiclo();
    }
    void FixedUpdate()
    {
        interpolador.AvanzarTiempo();
        Vector3 posicionInterpolada = interpolador.GetPosicionInterpolada();

        AplicarOscilacion(objeto1Instanciado, posicionInterpolada, amplitud1, frecuencia1);
        AplicarOscilacion(objeto2Instanciado, objeto1Instanciado.transform.position, amplitud2, frecuencia2);

        if (interpolador.HaTerminado())
        {
            ConfigurarNuevoCiclo();
            interpolador.Resetear();
        }        
    }    
    void ConfigurarNuevoCiclo()//
    {
        Vector3 nuevoPuntoInicio = ObtenerPosicionAleatoria();
        Vector3 nuevoPuntoFinal = ObtenerPosicionAleatoria();
        float duracionAleatoria = Random.Range(duracionMinima, duracionMaxima);
                
        interpolador.Configurar(nuevoPuntoInicio, nuevoPuntoFinal, duracionAleatoria);
    }        
    void AplicarOscilacion(GameObject objeto, Vector3 posicionBase, float amplitud, float frecuencia)//
    {
        float tOscilacionY = Mathf.Sin(Time.fixedTime * frecuencia);
        float yPos = Mathf.Lerp(-amplitud, amplitud, (tOscilacionY + 1) / 2);

        float tOscilacionX = Mathf.Cos(Time.fixedTime * frecuencia);
        float xPos = Mathf.Lerp(-amplitud, amplitud, (tOscilacionX + 1) / 2);
                
        Vector3 nuevaPosicion = new Vector3(posicionBase.x + xPos, posicionBase.y + yPos, posicionBase.z);
        
        objeto.transform.position = nuevaPosicion;
    }    
    private Vector3 ObtenerPosicionAleatoria()//
    {
        float x = Random.Range(-500f, 500f); 
        float y = Random.Range(-500f, 500f); 
        float z = Random.Range(-500f, 500f); 

        return new Vector3(x, y, z);
    }
}
