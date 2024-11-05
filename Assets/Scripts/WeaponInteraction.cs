using RootMotion.FinalIK;
using UnityEngine;

public class WeaponInteraction : MonoBehaviour
{
    [Tooltip("The object to interact to")]
    public InteractionObject interactionObject;
    [Tooltip("The effectors to interact with")]
    public FullBodyBipedEffector[] effectors;

    private InteractionSystem interactionSystem;

    void Awake()
    {
        interactionSystem = GetComponent<InteractionSystem>();
    }

    private void Start()
    {
        if (interactionSystem == null) return;
        if (effectors.Length == 0) Debug.Log("Please select the effectors to interact with.");

        foreach (FullBodyBipedEffector e in effectors)
        {
            interactionSystem.StartInteraction(e, interactionObject, true);
        }

        if (effectors.Length == 0) return;
    }

    private void Update()
    {
        
    }
}