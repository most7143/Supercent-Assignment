using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FollowUI : MonoBehaviour
{
    public Guest Guest;

    public Vector3 UIOffset;

    public CanvasGroup UICanvasGroup;

    public RectTransform UIRect;

    public Transform UITakeBread;

    public Image UIPayIcon;

    public Image UiTableIcon;

    public TextMeshProUGUI UINeedCountText;

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        UIRect.transform.position = Camera.main.WorldToScreenPoint(transform.position + UIOffset);

        if (Guest.IsArrive && Guest.CurrentMovePoint == GuestMovePoints.DisplayTable)
        {
            UIPayIcon.gameObject.SetActive(false);
            UiTableIcon.gameObject.SetActive(false);

            UICanvasGroup.alpha = 1;

            RefreshText();
        }
        else if (Guest.Breads.Count > 0)
        {
            UITakeBread.gameObject.SetActive(false);

            if (false == Guest.IsEating)
            {
                UIPayIcon.gameObject.SetActive(true);
            }
            else
            {
                UiTableIcon.gameObject.SetActive(true);
            }
        }
        else
        {
            UICanvasGroup.alpha = 0;
        }
    }

    public void RefreshText()
    {
        UITakeBread.gameObject.SetActive(true);
        UINeedCountText.SetText((Guest.MaxTakeBreadCount - Guest.CurrentTakeCount).ToString());
    }
}