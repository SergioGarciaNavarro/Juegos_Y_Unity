using UnityEngine;

public class Generador : MonoBehaviour
{
    public GameObject cube;
    public int width, large; // Eliminamos height de aquí porque se calcula dinámicamente
    public float detail;
    private int seed;
    public bool crearHueco = true;
    public int huecoWidth = 10;
    public int huecoLarge = 10;

    void Start()
    {
        seed = Random.Range(100000, 900000);
        GenerateMap();
    }

    public void GenerateMap()
    {
        // Calculamos el inicio del hueco para que quede centrado (opcional)
        int startX = (width - huecoWidth) / 2;
        int startZ = (large - huecoLarge) / 2;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < large; z++)
            {
                // CONDICIÓN DEL HUECO:
                // Si la posición actual está dentro del rango del hueco, saltamos este ciclo
                if (crearHueco && 
                    x >= startX && x < startX + huecoWidth && 
                    z >= startZ && z < startZ + huecoLarge)
                {
                    continue; // "Salta" a la siguiente iteración de Z sin instanciar cubos
                }

                // Cálculo de altura usando Perlin Noise
                int currentHeight = (int)(Mathf.PerlinNoise((x / 2.0f + seed) / detail, (z / 2.0f + seed) / detail) * detail);

                for (int y = 0; y < currentHeight; y++)
                {
                    Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }
    }
}