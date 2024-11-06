using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //싱글턴으로
    public static SoundManager instance; // 싱글톤을 할당할 전역 변수 -> 이 instance 자체는 게임 오브젝트를 얘기하는것 같고 

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            Debug.Log("SoundManager저가 생성됐습니다");
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 SoundManager가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("SoundManager를 삭제합니다");
        }
    }


    //인스펙터에서  할당

    [Header("Background")]
    public AudioClip Day_BGM;
    public AudioClip Fight_BGM;
    public AudioClip Victory_BGM;
    public AudioClip GameOver_Sound;


    [Header("Player")]
    public AudioClip Jump;
    public AudioClip Attak;
    public AudioClip Energy;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(Jump);
    }


    public void PlayGetItemSound()
    {
        audioSource.PlayOneShot(Energy);
    }


    public void PlayDayBGM()
    {
        audioSource.clip = Day_BGM;
        audioSource.Play();
    }
    public void PlayBattleBGM()
    {
        audioSource.Stop();
        audioSource.clip = Fight_BGM;
        audioSource.Play();
    }

    public void PlayVictoryBGM()
    {

        audioSource.clip = Victory_BGM;
        audioSource.Play();
    }

    public void PlayGameOverSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(GameOver_Sound);
    }


}
