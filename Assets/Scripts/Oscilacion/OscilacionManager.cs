using UnityEngine;

public class OscilacionManager : MonoBehaviour
{
    [Header("Objeto 1 - Oscilaci�n")]
    [SerializeField] private float amplitud1 = 1.0f;
    [SerializeField] private float frecuencia1 = 1.0f;

    [Header("Objeto 2 - Oscilaci�n")]
    [SerializeField] private float amplitud2 = 1.5f;
    [SerializeField] private float frecuencia2 = 0.8f;

    [Header("Referencia al Interpolador (Solo Objeto 1)")]
    [SerializeField] private Interpolador interpolador;

    [Header("Objeto Base para Oscilaci�n")]
    [SerializeField] private Transform objetoReferencia;

    [Header("Prefabs de los Objetos")]
    [SerializeField] private GameObject prefabObjeto1;
    [SerializeField] private GameObject prefabObjeto2;

    [Header("Duraci�n aleatoria")]
    [SerializeField] private float duracionMinima = 1f;
    [SerializeField] private float duracionMaxima = 3f;

    private GameObject objeto1Instanciado;
    private GameObject objeto2Instanciado;

    void Start()
    {
        // Instanciar objetos si existen
        if (prefabObjeto1 != null)
        {
            objeto1Instanciado = Instantiate(prefabObjeto1, transform.position, Quaternion.identity);
        }
        if (prefabObjeto2 != null)
        {
            objeto2Instanciado = Instantiate(prefabObjeto2, transform.position, Quaternion.identity);
        }
        if (objetoReferencia == null && objeto1Instanciado != null)
        {
            objetoReferencia = objeto1Instanciado.transform;
        }

        // Configurar el interpolador inicialmente
        ConfigurarNuevoCiclo();
    }

    void FixedUpdate()
    {
        if (interpolador != null && objeto1Instanciado != null)
        {
            interpolador.AvanzarTiempo();
            Vector3 posicionInterpolada = interpolador.GetPosicionInterpolada();

            // Asegurarse de que la posici�n no sea NaN antes de usarla
            if (float.IsNaN(posicionInterpolada.x) || float.IsNaN(posicionInterpolada.y) || float.IsNaN(posicionInterpolada.z))
            {
                Debug.LogError("Posici�n interpolada es NaN, se usar� Vector3.zero");
                posicionInterpolada = Vector3.zero;
            }

            AplicarOscilacion(objeto1Instanciado, posicionInterpolada, amplitud1, frecuencia1);

            if (objeto2Instanciado != null)
            {
                AplicarOscilacion(objeto2Instanciado, objeto1Instanciado.transform.position, amplitud2, frecuencia2);
            }

            // Si ha terminado el ciclo de interpolaci�n, configurar uno nuevo
            if (interpolador.HaTerminado())
            {
                ConfigurarNuevoCiclo();
                interpolador.Resetear();
            }
        }
    }

    // Configura el interpolador con nuevas posiciones aleatorias y duraci�n aleatoria
    void ConfigurarNuevoCiclo()
    {
        Vector3 nuevoPuntoInicio = ObtenerPosicionAleatoria();
        Vector3 nuevoPuntoFinal = ObtenerPosicionAleatoria();
        float duracionAleatoria = Random.Range(duracionMinima, duracionMaxima);

        // Configurar el interpolador con las nuevas posiciones y duraci�n
        interpolador.Configurar(nuevoPuntoInicio, nuevoPuntoFinal, duracionAleatoria);
    }

    // Funci�n para aplicar la oscilaci�n a un objeto
    void AplicarOscilacion(GameObject objeto, Vector3 posicionBase, float amplitud, float frecuencia)
    {
        float tOscilacionY = Mathf.Sin(Time.fixedTime * frecuencia);
        float yPos = Mathf.Lerp(-amplitud, amplitud, (tOscilacionY + 1) / 2);

        float tOscilacionX = Mathf.Cos(Time.fixedTime * frecuencia);
        float xPos = Mathf.Lerp(-amplitud, amplitud, (tOscilacionX + 1) / 2);

        // Asegurarse de que no se asignen valores NaN a la posici�n
        Vector3 nuevaPosicion = new Vector3(posicionBase.x + xPos, posicionBase.y + yPos, posicionBase.z);
        if (float.IsNaN(nuevaPosicion.x) || float.IsNaN(nuevaPosicion.y) || float.IsNaN(nuevaPosicion.z))
        {
            Debug.LogError("Posici�n de oscilaci�n es NaN, se usar� Vector3.zero");
            nuevaPosicion = Vector3.zero;
        }

        objeto.transform.position = nuevaPosicion;
    }

    // Funci�n para obtener una posici�n aleatoria dentro de un rango
    private Vector3 ObtenerPosicionAleatoria()
    {
        float x = Random.Range(-500f, 500f); // Margen ajustable
        float y = Random.Range(-500f, 500f); // Margen ajustable
        float z = Random.Range(-500f, 500f); // Margen ajustable

        // Comprobar si alg�n valor es NaN
        if (float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z))
        {
            Debug.LogError("Posici�n aleatoria generada con NaN: " + new Vector3(x, y, z));
            return Vector3.zero; // Retornar una posici�n v�lida
        }

        return new Vector3(x, y, z);
    }
}
