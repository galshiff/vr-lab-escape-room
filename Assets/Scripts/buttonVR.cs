using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class buttonVR : MonoBehaviour
{
    public GameObject btn;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            Vector3 btn_pos = btn.transform.localPosition;
            btn.transform.localPosition = new Vector3(btn_pos.x, btn_pos.y - 0.01f, btn_pos.z);
            presser = other.gameObject;
            onPress.Invoke();
            // sound.Play();
            sound.PlayOneShot(sound.clip, 0.75f);
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed && other.gameObject == presser) 
        {
            Vector3 btn_pos = btn.transform.localPosition;
            btn.transform.localPosition = new Vector3(btn_pos.x, btn_pos.y + 0.01f, btn_pos.z);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("labEscapeRoomScene", LoadSceneMode.Single);
    }
}
