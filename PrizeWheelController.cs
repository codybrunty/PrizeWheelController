using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeWheelController : MonoBehaviour {

    public int minNumberOfSpins = 3;
    public int maxNumberOfSpins = 4;
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
        gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, UnityEngine.Random.Range(0f, 360f));
    }

    public void SpinWheel() {
        int randomNumberOfSpins = UnityEngine.Random.Range(minNumberOfSpins, maxNumberOfSpins + 1);
        float currentAngle = transform.eulerAngles.z;
        int randomPrizeWheelIndex = UnityEngine.Random.Range(0, prizeWheelNumbers.Count);
        float itemNumberAngle = ((float)randomPrizeWheelIndex) * anglePerItem;
        int prizeNumber = prizeWheelNumbers[randomPrizeWheelIndex];
        
        while (currentAngle >= 360) {
            currentAngle -= 360;
        }
        while (currentAngle < 0) {
            currentAngle += 360;
        }

        float targetAngle = -(itemNumberAngle + 360f * ((float) randomNumberOfSpins));
        Debug.Log("PrizeWheel will spin "+ randomNumberOfSpins + " times before ending at " + prizeNumber + " with an angle of " + itemNumberAngle);
        StartCoroutine(SpinTheWheel(currentAngle, targetAngle, totalSpinDuration, prizeNumber));
    }
    
    private IEnumerator SpinTheWheel(float fromAngle, float toAngle, float totalSpinDuration, int result) {
        float time = 0f;
        while (time < totalSpinDuration) {
            float lerpPositionOnCurve = easeOutCurve.Evaluate(time / totalSpinDuration);
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Lerp(fromAngle, toAngle, lerpPositionOnCurve));
            time += Time.deltaTime;
            yield return null;
        }
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, toAngle);
        Debug.Log("Prize: " + result);
    }
}


