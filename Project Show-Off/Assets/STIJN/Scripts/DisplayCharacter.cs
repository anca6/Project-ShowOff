using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageMovement : MonoBehaviour
{
    public RectTransform furbieUI;
    public RectTransform jojoUI;
    public RectTransform saraUI;

    public Image furbieImage;
    public Image jojoImage;
    public Image saraImage;

    public Sprite furbieDesaturatedSprite;
    public Sprite jojoDesaturatedSprite;
    public Sprite saraDesaturatedSprite;
    public Sprite furbieSprite;
    public Sprite jojoSprite;
    public Sprite saraSprite;

    public Vector2 position1 = new Vector2(95f, -94f);
    public Vector2 position2 = new Vector2(55f, -40f);
    public Vector2 position3 = new Vector2(138f, -40f);

    public Vector2 scale1 = new Vector3(1f, 1f, 1f);
    public Vector2 scale2 = new Vector3(.66f, .66f, 1f);

    public float UItimeScale = .5f;

    void Start()
    {

    }

    private void Update()
    {
        // Furbie Selected
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Example: Move the image to a new position over 1 second
            furbieUI.DOAnchorPos(position1, UItimeScale);
            jojoUI.DOAnchorPos(position2, UItimeScale);
            saraUI.DOAnchorPos(position3, UItimeScale);

            furbieUI.DOScale(scale1, UItimeScale);
            jojoUI.DOScale(scale2, UItimeScale);
            saraUI.DOScale(scale2, UItimeScale);


            furbieImage.sprite = furbieSprite;
            jojoImage.sprite = jojoDesaturatedSprite;
            saraImage.sprite = saraDesaturatedSprite;

        }

        // Jojo Selected
        // Check if the q is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Example: Move the image to a new position over 1 second
            furbieUI.DOAnchorPos(position3, UItimeScale);
            jojoUI.DOAnchorPos(position1, UItimeScale);
            saraUI.DOAnchorPos(position2, UItimeScale);

            furbieUI.DOScale(scale2, UItimeScale);
            jojoUI.DOScale(scale1, UItimeScale);
            saraUI.DOScale(scale2, UItimeScale);
            

            furbieImage.sprite = furbieDesaturatedSprite;
            jojoImage.sprite = jojoSprite;
            saraImage.sprite = saraDesaturatedSprite;

        }
        // Sara Selected
        // Check if the e is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Example: Move the image to a new position over 1 second
            furbieUI.DOAnchorPos(position2, UItimeScale);
            jojoUI.DOAnchorPos(position3, UItimeScale);
            saraUI.DOAnchorPos(position1, UItimeScale);

            furbieUI.DOScale(scale2, UItimeScale);
            jojoUI.DOScale(scale2, UItimeScale);
            saraUI.DOScale(scale1, UItimeScale);


            furbieImage.sprite = furbieDesaturatedSprite;
            jojoImage.sprite = jojoDesaturatedSprite;
            saraImage.sprite = saraSprite;

        }
    }
}