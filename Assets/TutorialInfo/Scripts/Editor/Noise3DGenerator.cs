using UnityEngine;
using UnityEditor;
using System.IO;

public class Noise3DGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate 3D Noise Texture")]
    public static void Generate3DNoise()
    {
        const int size = 64;   // Можеш змінити на 32, 128, 256
        Texture3D tex = new Texture3D(size, size, size, TextureFormat.RFloat, false);

        Color[] colors = new Color[size * size * size];

        int i = 0;
        for (int z = 0; z < size; z++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float nx = (float)x / size;
                    float ny = (float)y / size;
                    float nz = (float)z / size;

                    // Тут ти викликаєш свій пакет NoiseS3D
                    float n = (float)NoiseS3D.NoiseCombinedOctaves(nx * 10f, ny * 10f, nz * 10f);

                    colors[i++] = new Color(n, n, n, 1);
                }
            }
        }

        tex.SetPixels(colors);
        tex.Apply();

        // Зберігаємо Asset
        string path = "Assets/GeneratedNoise3D.asset";
        Directory.CreateDirectory("Assets");

        AssetDatabase.CreateAsset(tex, path);
        AssetDatabase.SaveAssets();

        Debug.Log("✔ 3D Noise Texture Generated at: " + path);
    }
}
