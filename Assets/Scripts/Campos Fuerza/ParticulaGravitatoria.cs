using UnityEngine;

//Partícula afectada por la gravedad.
public class ParticulaGravitatoria : MonoBehaviour
{
    public float masa = 1.0f; // Masa de la partícula.
    public Vector3 velocidad; // Velocidad de la partícula.

    //Aplica las fuerzas gravitatorias.
    private void FixedUpdate()
    {
        Vector3 sumaAceleraciones = Vector3.zero; //Acumulador de la aceleración total.

        //Recorre todas las fuentes gravitatorias y suma sus efectos.
        foreach (FuenteGravitatoria fuente in CampoGravitatorio.fuentes)
        {
            Vector3 aceleracion = fuente.CalcularAceleracion(transform.position);
            sumaAceleraciones += aceleracion;
        }

        //Aplica la aceleración a la velocidad y actualiza la posición.
        velocidad += sumaAceleraciones * Time.fixedDeltaTime;
        transform.position += velocidad * Time.fixedDeltaTime;
    }
}
