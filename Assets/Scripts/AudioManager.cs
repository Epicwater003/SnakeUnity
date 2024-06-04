using UnityEngine;
public partial class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get{
            if (_instance == null){
                _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    private void Awake(){
        if (_instance == null){
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public void PlaySound(AudioSource sound)
    {
        sound.Play();
    }
}