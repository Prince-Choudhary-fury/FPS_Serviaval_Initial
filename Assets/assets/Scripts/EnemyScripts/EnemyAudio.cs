using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip screemClip;

    [SerializeField]
    private AudioClip dieClip;

    [SerializeField]
    private AudioClip[] attackClips;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayScreamSound()
    {
        audioSource.clip = screemClip;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }
    public void PlayDeadSound()
    {
        audioSource.clip = dieClip;
        audioSource.Play();
    }

}
