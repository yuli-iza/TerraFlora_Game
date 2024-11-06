using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardBall : MonoBehaviour
{
    [Header("Inscribed")]
    
    public GameObject evolutionEffectPrefab;

    public GameObject deathEffectPrefab;

    public Transform evolutionEffectPosition;
    public Transform deathEffectPosition;

    public Vector3 effectScale = new Vector3(5.0f, 5.0f, 1.0f);
    public float speed = 35.0f;

    public float leftAndRightEdge = 20.0f;

    public float chanceToChangeDirections = 0.1f;

    private bool isBallStopped = false;

    //private bool inTargetZone = false;
    private int targetZoneCount = 0;

    private GameObject evolutionEffect;

    public GameObject plant;

    private GameObject targetGroup;

    // Start is called before the first frame update
    void Start()
    {
        //plant = GameObject.Find("Coleus02");
        if (plant != null) plant.SetActive(false);

        targetGroup = GameObject.Find("TargetGroup");

        if (targetGroup != null) {
            Debug.Log("TargetGroup found");
        }
        else {
            Debug.Log("TargetGroup not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBallStopped)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isBallStopped = true;
            if (targetZoneCount > 0)
            {
                TriggerEvolutionEffect();
                Debug.Log("Hit the target zone!, Loading Next Level...");
            }
            else
            {
                
                TriggerDeathEffect();

                // Notify TerraFlora to decrement health
                TerraFlora terraFlora = Camera.main.GetComponent<TerraFlora>();
                terraFlora.TargetMissed();

                if (terraFlora.health > 0)
                {
                    Debug.Log("Missed the target zone!, Health: " + terraFlora.health);
                    //If health is greater than 0, reset the ball movement
                    ResetMovement();
                    // ResetMovement();
                }

                //Debug.Log("Missed the target zone!, Game Over...");
                //ResetMovement();
            }
        }
        // Basic Movement
        Vector3 pos = transform.position;
// b
        pos.x += speed * Time.deltaTime;
// c
        transform.position = pos;
// d
        // Changing Direction
        if(pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if(pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move left
        }
    }

    void FixedUpdate()
    {
        // Changing Direction Randomly
        if(Random.value < chanceToChangeDirections)
        {
            speed *= -1; // Change direction
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TargetBox"))
        {
            //Debug.Log("Hit the target zone!, Loading Next Level...");
            
            //inTargetZone = true;
            targetZoneCount++; // Increment the count when entering a target box
            Debug.Log("Entered a target box. Current count: " + targetZoneCount);
       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("TargetBox"))
        {   
            targetZoneCount--;
            if (targetZoneCount < 0) targetZoneCount = 0;
            //inTargetZone = false;
        }
    }

        private void TriggerEvolutionEffect()
    {
        if (evolutionEffectPrefab != null && evolutionEffectPosition != null)
        {
            //yield return new WaitForSeconds(0.7f); // Adjust delay as needed
            //Make the ball and bar/target game objects invisible
                    // Make the ball and bar/target game objects invisible
            gameObject.SetActive(false);  // Hide the ball itself
            // Assuming you have references to the bar and target objects, deactivate them as well
            GameObject bar = GameObject.Find("Bar");  // Replace with your actual bar GameObject reference
            //GameObject target = GameObject.Find("TargetGroup");  // Replace with your actual target GameObject reference
            if (bar != null) bar.SetActive(false);
            if (targetGroup != null) targetGroup.SetActive(false);

            //yield return new WaitForSeconds(0.7f); // Adjust delay as needed

            evolutionEffect = Instantiate(evolutionEffectPrefab, evolutionEffectPosition.position, Quaternion.identity);
            evolutionEffect.transform.localScale = effectScale;

            //StartCoroutine(FadeOutEffect(evolutionEffect));
            InvokeRepeating("FadeOutEffectInvoke", 1.0f, 0.2f);

            //Make Plant Grow/visible
            Invoke("PlantAppear", 1.3f);

            Invoke("Next", 2.0f);

        }
    }

    private void PlantAppear()
    {
        // Make the plant game object visible
        //GameObject plant = GameObject.Find("Coleus02");  // Replace with your actual plant GameObject reference
        if (plant != null) {
            plant.SetActive(true);
            Debug.Log("Plant is now active");
        }
        
        
    }

    private void Next(){
        SceneManager.LoadScene("Scene_Win");
    }


    private void FadeOutEffectInvoke()
    {
        if (evolutionEffect == null) return;

        // Gradually scale down the object to simulate fading out
        evolutionEffect.transform.localScale *= 0.5f; // Adjust scaling factor as needed

        // Stop scaling and destroy the object once it’s nearly invisible
        if (evolutionEffect.transform.localScale.x <= 0.1f)
        {
            CancelInvoke("FadeOutEffectInvoke");
            Destroy(evolutionEffect);
        }
    }


    private void TriggerDeathEffect()
    {
        if (deathEffectPrefab != null && deathEffectPosition != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, deathEffectPosition.position, Quaternion.identity);
            deathEffect.transform.localScale = effectScale;
        }
    }

    public void ResetMovement()
    {
        isBallStopped = false;
        //inTargetZone = false;
        targetZoneCount = 0;
    }
}
