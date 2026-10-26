using UnityEngine;

public class CarCustomizer : MonoBehaviour
{
    // Assign the MeshRenderer or SkinnedMeshRenderer of your PlayerCar
    public Renderer carRenderer;

    void Start()
    {
        // Load saved color choice when the game starts
        LoadCarColor();
    }

    public void ChangeCarColor(Color newColor)
    {
        if (carRenderer != null)
        {
            carRenderer.material.color = newColor;

            // Save color values using PlayerPrefs
            PlayerPrefs.SetFloat("CarR", newColor.r);
            PlayerPrefs.SetFloat("CarG", newColor.g);
            PlayerPrefs.SetFloat("CarB", newColor.b);
        }
    }

    void LoadCarColor()
    {
        if (carRenderer != null && PlayerPrefs.HasKey("CarR"))
        {
            float r = PlayerPrefs.GetFloat("CarR");
            float g = PlayerPrefs.GetFloat("CarG");
            float b = PlayerPrefs.GetFloat("CarB");
            carRenderer.material.color = new Color(r, g, b);
        }
    }

    // --- UI Button Helper Methods ---
    public void SetRed() { ChangeCarColor(Color.red); }
    public void SetBlue() { ChangeCarColor(Color.blue); }
    public void SetWhite() { ChangeCarColor(Color.white); }
    public void SetBlack() { ChangeCarColor(Color.black); }
}