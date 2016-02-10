using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {

    AudioSource fxSound; // Emitir sons
    public AudioClip backMusic; // Som de fundo

    // Use this for initialization
    void Start () {
        // Audio Source responsavel por emitir os sons
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
	}
}
