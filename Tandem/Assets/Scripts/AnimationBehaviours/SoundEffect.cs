using UnityEngine;
using System.Collections;

public class SoundEffect : StateMachineBehaviour {

    public AudioClip sound;
    public float volume = 1f;
    private AudioSource source;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        source = animator.gameObject.GetComponent<AudioSource>();
        if (source && sound) source.PlayOneShot(sound, volume);
    }
}
