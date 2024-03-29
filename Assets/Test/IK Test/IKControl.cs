using UnityEngine;

namespace Test
{
    [RequireComponent(typeof(Animator))]
    public class IKControl : MonoBehaviour
    {
        protected Animator animator;

        public bool ikActive = false;
        public Transform rightHandAnchor = null;
        public Transform leftHandAnchor = null;
        public Transform lookObj = null;

        void Start () {
            animator = GetComponent<Animator>();
        }

        //a callback for calculating IK
        void OnAnimatorIK()
        {
            if(animator)
            {
                //if the IK is active, set the position and rotation directly to the goal.
                if(ikActive)
                {
                    // Set the look target position, if one has been assigned
                    if(lookObj != null) {
                        animator.SetLookAtWeight(1);
                        animator.SetLookAtPosition(lookObj.position);
                    }

                    // Set the right hand target position and rotation, if one has been assigned
                    if(rightHandAnchor != null) {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);
                        animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandAnchor.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandAnchor.rotation);
                    }

                    // Set the right hand target position and rotation, if one has been assigned
                    if(leftHandAnchor != null) {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);
                        animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandAnchor.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandAnchor.rotation);
                    }

                }
                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0);
                    animator.SetLookAtWeight(0);
                }
            }
        }
    }
}
