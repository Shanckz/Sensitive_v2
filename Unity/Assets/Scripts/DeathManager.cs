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
    protected float transparency;
    protected Image image;

    private void Start()
    {
        if(temps <= 0)
        {
            temps = 1;
        }
        deathPlayer = false;
        couleurCanvesDeath = new Color(0, 0, 0, 0);
        image = gameObject.GetComponent<Image>();
        image.color = couleurCanvesDeath;
    }

    void Update ()
    {
		if(deathPlayer == true)
        {
            if(transparency <= 1)
            {
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
