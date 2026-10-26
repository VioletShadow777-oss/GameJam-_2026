using UnityEngine;
using UnityEngine.UI;

public class DropButtonImageSwap : MonoBehaviour
{
    // Components
    public Image image;
    public BombSystemManager bombSystemManager;

    [Header("Images")]
    public Sprite coloredImage;
    public Sprite grayImage;


    private void Awake()
    {
        image = GetComponent<Image>();
    }


    private void Update()
    {
        ImageSwap();
    }


    // Swaps the Image
    private void ImageSwap()
    {
        if(bombSystemManager.bombAmmo == 0)
        {
            image.sprite = grayImage;
        }
        else
        {
            image.sprite = coloredImage;
        }
    }
    
}
