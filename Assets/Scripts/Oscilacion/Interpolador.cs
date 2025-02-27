using UnityEngine;

public class Interpolador : MonoBehaviour
{
    [SerializeField] private Vector3 puntoInicio;
    [SerializeField] private Vector3 puntoFinal;
    [SerializeField] private float duracion;
    private float tiempo = 0.0f;

    public void Configurar(Vector3 inicio, Vector3 final, float duracion)
    {
        this.puntoInicio = inicio;
        this.puntoFinal = final;

        // Comprobar si la duraci�n es v�lida
        if (float.IsNaN(duracion) || duracion <= 0)
        {
            Debug.LogError("Duraci�n inv�lida para la interpolaci�n: " + duracion);
            this.duracion = 1f; // Asignar un valor por defecto
        }
        else
        {
            this.duracion = duracion;
        }
    }

    public Vector3 GetPosicionInterpolada()
    {
        float t = Mathf.Clamp01(tiempo / duracion);
        Vector3 interpolada = Vector3.Lerp(puntoInicio, puntoFinal, t);

        // Comprobar si la posici�n interpolada tiene NaN en alguna de las componentes
        if (float.IsNaN(interpolada.x) || float.IsNaN(interpolada.y) || float.IsNaN(interpolada.z))
        {
            Debug.LogError("Interpolaci�n ha dado NaN en la posici�n: " + interpolada);
            return Vector3.zero;  // Devolver una posici�n v�lida
        }

        return interpolada;
    }

    public void AvanzarTiempo()
    {
        tiempo += Time.deltaTime;
    }

    public bool HaTerminado()
    {
        return tiempo >= duracion;
    }

    public void Resetear()
    {
        tiempo = 0f;
    }
}
