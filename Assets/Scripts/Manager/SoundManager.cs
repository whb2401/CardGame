using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip hitBrick;
    public AudioClip hitPickable;

    [Header("BGM")]
    public AudioClip acMain;
    public AudioClip acTown;
}
