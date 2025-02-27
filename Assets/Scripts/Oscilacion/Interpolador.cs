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
        this.duracion = duracion;
    }
    public Vector3 GetPosicionInterpolada()
    {
        float t = Mathf.Clamp01(tiempo / duracion);
        Vector3 interpolada = Vector3.Lerp(puntoInicio, puntoFinal, t);

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
