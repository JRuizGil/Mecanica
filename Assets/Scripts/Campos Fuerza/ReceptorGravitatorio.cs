using UnityEngine;

//objeto que es afectado por la gravedad de las fuentes gravitatorias.
public class ReceptorGravitatorio : MonoBehaviour
{
    public float coeficienteGravitatorio = 1.0f; //intensidad del efecto gravitatorio en este objeto.
    private Rigidbody rb; //Rigidbody del objeto.

    
    private void Start()
    {
        //obtiene el componente Rigidbody.
        rb = GetComponent<Rigidbody>();
    }

    //aplica la gravedad de todas las fuentes gravitatorias, cada fixed frame.
    private void FixedUpdate()
    {
        Vector3 sumaAceleraciones = Vector3.zero; //guerda la suma de todas las aceleraciones gravitatorias.

        // Recorre todas las fuentes de gravedad y calcula su efecto en el objeto.
        foreach (FuenteGravitatoria fuente in CampoGravitatorio.fuentes)
        {
            Vector3 aceleracion = fuente.CalcularAceleracion(transform.position);
            sumaAceleraciones += aceleracion;
        }

        //Aplica la fuerza gravitatoria
        rb.AddForce(sumaAceleraciones * rb.mass * coeficienteGravitatorio);
    }
}


