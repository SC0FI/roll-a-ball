using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool forward = true;
    float rotation = 0;

    [SerializeField]
    RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //-10, 10

        if (forward)
        {
            rotation += 0.1f;
            rectTransform.Rotate(new Vector3(0, 0, 0.1f));
        } else
        {
            rotation -= 0.1f;
            rectTransform.Rotate(new Vector3(0, 0, -0.1f));
        }


        if (rotation > 9.9)
        {
            forward = false;
        } else if (rotation < -9.9)
        {
            forward = true;
        }
        
    }

    public void StartGame()
    {
        print("Loading");
        SceneManager.LoadScene("Level1");
        
    }
}
