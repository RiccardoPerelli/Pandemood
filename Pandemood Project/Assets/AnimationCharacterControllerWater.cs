using System;
using System.Collections;
using System.Collections.Generic;
using DDSystem.Script;
using Doublsb.Dialog;
using UnityEngine;

public class AnimationCharacterControllerWater : MonoBehaviour
{
    private static readonly int IsSwimming = Animator.StringToHash("IsSwimming");
    private Animator _animator;
    private DialogManager[] _dialogManagers;
    private bool _dialogueOpen;

    [SerializeField] private AudioClip _swimmingSound;

    private AudioSource _audioSource;
    private bool _hasAudioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _dialogManagers = Resources.FindObjectsOfTypeAll<DialogManager>();
        
        foreach (var dialog in _dialogManagers)
        {
            dialog.OnActivate += OnActivateDialogue;
            dialog.OnDeactivate += OnDisableDialogue;
        }

        _audioSource = gameObject.GetComponent<AudioSource>();
        _hasAudioSource = _audioSource != null;
    }

    void OnActivateDialogue(object p, EventArgs d)
    {
        _dialogueOpen = true;
    }

    void OnDisableDialogue(object p, EventArgs d)
    {
        _dialogueOpen = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (inGameMenu.GameIsPaused && _audioSource.isPlaying)
        {
            _audioSource.Pause();
        }
        else if ((Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.1f || Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.1f) && !_dialogueOpen)
        {
            _animator.SetBool(IsSwimming, true);
            if (_hasAudioSource)
            {
                if (!_audioSource.isPlaying && !inGameMenu.GameIsPaused)
                {
                    _audioSource.clip = _swimmingSound;
                    _audioSource.Play();
                    _audioSource.volume = 0.35f;
                    _audioSource.loop = true;
                }
            }
        } else
        {
            _animator.SetBool(IsSwimming, false);
            if (_hasAudioSource)
            {
                if (_audioSource.isPlaying && !inGameMenu.GameIsPaused)
                {
                    _audioSource.Pause();
                }
            }
        }
    }
}
