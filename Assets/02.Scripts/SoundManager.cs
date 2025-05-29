using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource footstepSource;

    [Header("Audio Clips")]
    public AudioClip lobbyBGM;
    public AudioClip stageBGM_Normal;
    public AudioClip stageBGM_Emergency;
    public AudioClip damageSound;
    public AudioClip buttonPressSound;
    public AudioClip doorOpenSound;
    public AudioClip footstepWalk;
    public AudioClip footstepRun;
    public AudioClip jumpSound;

    private string currentBGM = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �ٸ� ������ �Ѿ�� �� ������Ʈ�� �ı����� �ʰ� ������. 
        }
        else
        {
            Destroy(gameObject);  // ���� �ٸ� ������ �̹� �����Ѵٸ� �ߺ� ������ ���� �ڱ� �ڽ��� �ı���.
        }
    }

    public void PlayStageBGM(string stageName, bool isEmergency)
    {
        bgmSource.clip = isEmergency ? stageBGM_Emergency : stageBGM_Normal;
        bgmSource.Play();
    }

    public void PlayLobbyBGM()
    {
        if (currentBGM == "Lobby") return;

        bgmSource.clip = lobbyBGM;
        bgmSource.loop = true;
        bgmSource.volume = 1f;
        bgmSource.Play();

        currentBGM = "Lobby";
    }

    public void PlaySFX(string sfxName)  
    {
        AudioClip clip = Resources.Load<AudioClip>("SFX/" + sfxName); // Resources.Load�� ȿ������ �ε�
        if (clip != null)                                             // Resources�����ȿ� SFX �����ȿ� ���������
        {                                                            // ������ ȿ������ �ְ������ ���� �ڵ�ȿ� �ؿ� �ڵ带 �־��ָ� ȿ������ ����
            sfxSource.PlayOneShot(clip);                             // SoundManager.Instance.PlaySFX("jumpSound");
        }
        else
        {
            Debug.LogWarning("SFX not found: " + sfxName);           // ������ ������ �ֿܼ� ��� ���
        }
    }

    public void PlayPlayerFootstep(bool isRunning)
    {
        AudioClip clip = isRunning ? footstepRun : footstepWalk;        // �ȴ� ���̸� footstepWalk, �ٴ� ���̸� footstepRun ���
        footstepSource.clip = clip;
        if (!footstepSource.isPlaying)                                  // �ߺ� ��� ������ ���� isPlaying üũ
        {
            footstepSource.Play();
        }
    }

    public void PlayDamageSound()
    {
        sfxSource.PlayOneShot(damageSound);
    }

    public void PlayButtonPressSound()
    {
        sfxSource.PlayOneShot(buttonPressSound);
    }

    public void PlayDoorOpenSound()
    {
        sfxSource.PlayOneShot(doorOpenSound);
    }

    public void PlayJumpSound()
    {
        sfxSource.PlayOneShot(jumpSound);
    }
}
