using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeWheelController : MonoBehaviour {

    public int minNumberOfSpins = 3;
    public int maxNumberOfSpins = 5;
    public float totalSpinDuration = 5f;
    public AnimationCurve easeOutCurve = default;
    private float anglePerItem;
    private List<int> prizeWheelNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    
    private void Start() {
        anglePerItem = 360f / prizeWheelNumbers.Count;
        RandomizePrizeWheelStartingPosition();
        SpinWheel();
    }

    private int RandomizePrizeWheelStartingPosition() {
        //Gives the wheel a new starting position everytime for a more authentic feel
        gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, UnityEngine.Random.Range(0f, 360f));
    }

    public void SpinWheel() {
        //Randomize the number of times the wheel spins around before coming to a stop
        int randomNumberOfSpins = UnityEngine.Random.Range(minNumberOfSpins, maxNumberOfSpins + 1);
        float currentAngle = gameObject.transform.eulerAngles.z;
        //Randomly select a number on the prize wheel
        int randomPrizeWheelIndex = UnityEngine.Random.Range(0, prizeWheelNumbers.Count);
        //Get the angle at which the PrizeWheel needs to be rotated for the randomPrizeWheelIndex to be achhieved
        float itemNumberAngle = ((float)randomPrizeWheelIndex) * anglePerItem;
        //Just get the prize number name in the wheel
        int prizeNumber = prizeWheelNumbers[randomPrizeWheelIndex];
        
        //Make sure our currentAngle is between 0-360
        while (currentAngle >= 360) {
            currentAngle -= 360;
        }
        while (currentAngle < 0) {
            currentAngle += 360;
        }
        
        //Calculate the targetAngle after factoring in the times we spin around the PrizeWheel
        float targetAngle = -(itemNumberAngle + 360f * ((float) randomNumberOfSpins));
        
        Debug.Log("PrizeWheel will spin "+ randomNumberOfSpins + " times before ending at " + prizeNumber + " with an angle of " + itemNumberAngle);
        StartCoroutine(SpinTheWheel(currentAngle, targetAngle, totalSpinDuration, prizeNumber));
    }
    
    private IEnumerator SpinTheWheel(float fromAngle, float toAngle, float totalSpinDuration, int prizeNumber) {
        //Start at 0 seconds
        float time = 0f;
        //While we are under then desired amount of time for the entire spin
        while (time < totalSpinDuration) {
            //Use an animation curve to get a nice ease out when the PrizeWheel comes to a stop 
            float lerpPositionOnCurve = easeOutCurve.Evaluate(time / totalSpinDuration);
            //Rotate the PrizeWheel in Z to its new desired angle using the curve position
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Lerp(fromAngle, toAngle, lerpPositionOnCurve));
            //Add elapsed time to our time variable and repeat the process
            time += Time.deltaTime;
            yield return null;
        }
        //After the desired time make sure we avoid a floating point number and end on the exact desired position.
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, toAngle);
        Debug.Log("Prize: " + prizeNumber);
    }
}


