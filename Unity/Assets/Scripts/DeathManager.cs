using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    public static bool deathPlayer;
    protected float startTimerDeath;
    [SerializeField]
    protected float temps;
    protected Color couleurCanvesDeath;
    protected Color startCanvasColor;
    protected float transparency;
    protected Image image;
    protected bool startCheck;

    private void Start()
    {
        startCheck = false;
        if(temps <= 0)
        {
            temps = 1;
        }
        deathPlayer = false;
        couleurCanvesDeath = new Color(0, 0, 0, 0);
        image = gameObject.GetComponent<Image>();
        startCanvasColor = new Color(0, 0, 0, 1);
        transparency = 1;
    }

    void Update ()
    {
        if (!startCheck)
        {
            if (transparency >= 0)
            {
                image.color = startCanvasColor;
                Debug.Log(image.color + "Start");
                transparency -= Time.deltaTime;
                startCanvasColor = new Color(0, 0, 0, transparency);
                image.color = startCanvasColor;
                startTimerDeath = Time.time;
            }
            if (transparency <= 0)
            {
                transparency = 0;
                startCheck = true;
            }
        }

		if(deathPlayer == true)
        {
            if(transparency <= 1)
            {
                image.color = couleurCanvesDeath;
                Debug.Log(image.color + "Death");
                transparency += Time.deltaTime;
                couleurCanvesDeath = new Color(0, 0, 0, transparency);
                image.color = couleurCanvesDeath;
                startTimerDeath = Time.time;
            }
            if (transparency >= 1)
            {
                if(Time.time > startTimerDeath + temps)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
	}
}
