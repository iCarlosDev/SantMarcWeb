using UnityEngine;

namespace Members.Carlos.Scripts
{
    public class EndGame : MonoBehaviour
    {
        //Variables
        private static readonly int IsEnded = Animator.StringToHash("IsEnded");

        [SerializeField] private Animator endGameAnimator;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                endGameAnimator.SetBool(IsEnded, true);
            }
        }
    }
}
