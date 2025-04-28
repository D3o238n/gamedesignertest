using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Animation_event : MonoBehaviour
{
    [SerializeField]
    UnityEvent<AnimationEvent> OnFootstepEvent = new UnityEvent<AnimationEvent>();

    [SerializeField]
    UnityEvent<AnimationEvent> OnLandEvent = new UnityEvent<AnimationEvent>();

    private void OnFootstep(AnimationEvent animationEvent)
    {
        OnFootstepEvent.Invoke(animationEvent);
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        OnLandEvent.Invoke(animationEvent);
    }
}
