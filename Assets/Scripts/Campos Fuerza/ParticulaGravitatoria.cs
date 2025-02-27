using UnityEngine;

//Part�cula afectada por la gravedad.
public class ParticulaGravitatoria : MonoBehaviour
{
    public float masa = 1.0f; // Masa de la part�cula.
    public Vector3 velocidad; // Velocidad de la part�cula.

    //Aplica las fuerzas gravitatorias.
    private void FixedUpdate()
    {
        Vector3 sumaAceleraciones = Vector3.zero; //Acumulador de la aceleraci�n total.

        //Recorre todas las fuentes gravitatorias y suma sus efectos.
        foreach (FuenteGravitatoria fuente in CampoGravitatorio.fuentes)
        {
            Vector3 aceleracion = fuente.CalcularAceleracion(transform.position);
            sumaAceleraciones += aceleracion;
        }

        //Aplica la aceleraci�n a la velocidad y actualiza la posici�n.
        velocidad += sumaAceleraciones * Time.fixedDeltaTime;
        transform.position += velocidad * Time.fixedDeltaTime;
    }
}
