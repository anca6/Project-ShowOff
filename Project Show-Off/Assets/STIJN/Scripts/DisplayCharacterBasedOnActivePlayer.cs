using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMovement : MonoBehaviour
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

    private PlayerSwitch playerSwitch;

    void Start()
    {
        playerSwitch = FindObjectOfType<PlayerSwitch>(); // Find the PlayerSwitch script in the scene
    }

    private void Update()
    {
        // Check which character is currently selected
        switch (playerSwitch.currentCharacter)
        {
            case 0: // Furbie
                furbieImage.sprite = furbieSprite;
                jojoImage.sprite = jojoDesaturatedSprite;
                saraImage.sprite = saraDesaturatedSprite;

                furbieUI.DOAnchorPos(position1, UItimeScale);
                jojoUI.DOAnchorPos(position2, UItimeScale);
                saraUI.DOAnchorPos(position3, UItimeScale);

                furbieUI.DOScale(scale1, UItimeScale);
                jojoUI.DOScale(scale2, UItimeScale);
                saraUI.DOScale(scale2, UItimeScale);
                break;
            case 1: // Jojo
                furbieImage.sprite = furbieDesaturatedSprite;
                jojoImage.sprite = jojoSprite;
                saraImage.sprite = saraDesaturatedSprite;

                furbieUI.DOAnchorPos(position3, UItimeScale);
                jojoUI.DOAnchorPos(position1, UItimeScale);
                saraUI.DOAnchorPos(position2, UItimeScale);

                furbieUI.DOScale(scale2, UItimeScale);
                jojoUI.DOScale(scale1, UItimeScale);
                saraUI.DOScale(scale2, UItimeScale);
                break;
            case 2: // Sara
                furbieImage.sprite = furbieDesaturatedSprite;
                jojoImage.sprite = jojoDesaturatedSprite;
                saraImage.sprite = saraSprite;

                furbieUI.DOAnchorPos(position2, UItimeScale);
                jojoUI.DOAnchorPos(position3, UItimeScale);
                saraUI.DOAnchorPos(position1, UItimeScale);

                furbieUI.DOScale(scale2, UItimeScale);
                jojoUI.DOScale(scale2, UItimeScale);
                saraUI.DOScale(scale1, UItimeScale);
                break;
            default:
                break;
        }
    }
}