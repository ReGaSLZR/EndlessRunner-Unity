namespace ReGaSLZR.EndlessRunner.Utils
{

    using UnityEngine;

    public static class PhysicsUtil
    {

        /// <summary>
        /// Hasten the movement of a physics-influenced object by multiplying the gravity.
        /// reference: "Better Jumping in Unity >> https://www.youtube.com/watch?v=7KiK0Aqtmzc
        /// </summary>
        /// <param name="multiplier">Has to be greater than 1 (the default gravity).</param>
        /// <returns></returns>
        public static Vector3 GetGravityMultiplier(
            Vector3 vector, float multiplier)
        {
            return (vector * Physics.gravity.y *
                multiplier * Time.fixedDeltaTime);
        }

    }


}