using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundOnClick : MonoBehaviour
{
    void Awake()
    {
       this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClickSound);
    }
}