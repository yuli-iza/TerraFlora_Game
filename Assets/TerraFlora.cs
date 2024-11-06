using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerraFlora : MonoBehaviour
{
    public static TerraFlora Instance;

    [Header("Inscribed")]
    
    //public int plantInventory = 3;
    //public GameObject plantPotPrefab;

    public int health = 3;

    public GameObject healthHeartPrefab;

    private List<GameObject> healthHeartsList;

    public float heartSideX = -52f;

    public float heartSpacingX = 70f;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make persistent across scenes
        
            InitializeHearts();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }
    
    // Start is called before the first frame update
    /*
    void Start()
    {
        healthHeartsList = new List<GameObject>();

        for (int i = 0; i < health; i++)
        {
            GameObject healthHeartInstance = Instantiate<GameObject>(healthHeart);
            Vector3 position = Vector3.zero;
            position.x = heartSideX + (i * heartSpacingX);
            healthHeartInstance.transform.position = position;
            healthHeartsList.Add(healthHeartInstance);
        }
    }

    // Called when the target is missed, decrement health hearts
    public void TargetMissed()
    {
        Debug.Log("TargetMissed");
        if (healthHeartsList.Count > 0){
            int healthIndex = healthHeartsList.Count - 1;
            

            GameObject healthHeartInstance = healthHeartsList[healthIndex];

            healthHeartsList.RemoveAt(healthIndex);
            Destroy(healthHeartInstance);

            health--;

            if(health <= 0)
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene("Scene_GameOver");
            }
        }
    }
    */


    private void InitializeHearts()
    {
        healthHeartsList = new List<GameObject>();

        // Locate the Canvas in the scene to set as the parent for hearts
        Canvas canvas = FindObjectOfType<Canvas>();

         if (canvas != null)
        {
            DontDestroyOnLoad(canvas.gameObject);
        }
        else
        {
            Debug.LogWarning("No Canvas found! Ensure a Canvas exists in Scene_0.");
            return;
        }

        // Ensure the Canvas is marked as DontDestroyOnLoad
        DontDestroyOnLoad(canvas.gameObject);

        for (int i = 0; i < health; i++)
        {
            // Instantiate heart and set parent to Canvas
            GameObject healthHeartInstance = Instantiate(healthHeartPrefab, canvas.transform);
            //Vector3 position = Vector3.zero;
            
            RectTransform heartRect = healthHeartInstance.GetComponent<RectTransform>();
            if (heartRect != null)
            {
                heartRect.anchoredPosition = new Vector2(heartSideX + (i * heartSpacingX), -150); // Adjust as needed for positioning
            }
            //position.x = heartSideX + (i * heartSpacingX);
            //healthHeartInstance.transform.localPosition = position; // Set position relative to Canvas
            healthHeartsList.Add(healthHeartInstance);
        }
    }

    // Called when the target is missed to decrement health hearts
    public void TargetMissed()
    {
        Debug.Log("TargetMissed");

        if (health > 0)
        {
            health--;
            healthHeartsList[health].SetActive(false); // Hide a heart instead of destroying it

            if (health <= 0)
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene("Scene_GameOver");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
