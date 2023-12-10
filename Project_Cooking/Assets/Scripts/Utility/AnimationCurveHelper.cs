using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveHelper : MonoBehaviour
{
   

    public static AnimationCurve GetAnimationCurve(EasingFunction easingFunction)
    {
        switch (easingFunction)
        {
            case EasingFunction.EaseIn:
                return AnimationCurve.EaseInOut(0, 0, 1, 1);
            case EasingFunction.EaseOut:
                return AnimationCurve.EaseInOut(0, 1, 1, 0);
            case EasingFunction.Linear:
                return AnimationCurve.Linear(0, 0, 1, 1);
            default:
                Debug.Log("Wrong Easing Function assigned ");
                return null;
                
        }
    }
  
  

}
public enum EasingFunction
{
    EaseIn,
    EaseOut,
    Linear,
}