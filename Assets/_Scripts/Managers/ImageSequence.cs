using UnityEngine;

public class ImageSequence : MonoBehaviour
{
    public GameObject[] images;

    public GameObject toGameButton;

    private int currentIndex = 0;

    private void Start()
    {
        // Disable all images initially
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }

        // Show the first image
        if (images.Length > 0)
        {
            images[currentIndex].SetActive(true);
        }

        toGameButton.SetActive(false);
    }

    public void Next()
    {
        if (images.Length == 0)
            return;

        if (currentIndex >= images.Length - 1)
            return;

        // Disable the current image
        images[currentIndex].SetActive(false);

        // Move to the next image
        currentIndex++;

        // Enable the next image
        images[currentIndex].SetActive(true);

        // Show the game button on the final image
        if (currentIndex == images.Length - 1)
        {
            toGameButton.SetActive(true);
        }
    }
}