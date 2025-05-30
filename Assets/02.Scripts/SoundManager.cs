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
    public AudioClip doorCloseSound;
    public AudioClip footstepWalk;
    public AudioClip footstepRun;
    public AudioClip jumpSound;
    public AudioClip stageSuccessBGM;

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
        bgmSource.volume = 0.05f;
        bgmSource.Play();
        currentBGM = "Stage";
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

    public void PlayStageSuccessBGM()
    {
        bgmSource.clip = stageSuccessBGM;
        bgmSource.Play();

        currentBGM = "StageSuccess";
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
        AudioClip clip = isRunning ? footstepRun : footstepWalk;
        sfxSource.PlayOneShot(clip);
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
    public void PlayDoorCloseSound()
    {
        sfxSource.PlayOneShot(doorCloseSound);
    }

    public void PlayJumpSound()
    {
        sfxSource.PlayOneShot(jumpSound);
    }

    public void StopFootstep()
    {
        if (footstepSource.isPlaying)
        {
            footstepSource.Stop();
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp01(volume); // 0.0 ~ 1.0 ���̷� ����
    }
}
