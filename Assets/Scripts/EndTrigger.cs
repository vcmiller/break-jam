using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {
    public Animator canvasAnim;

    private void OnTriggerEnter2D(Collider2D collision) {
        var p = collision.GetComponent<Player>();
        if (p) {
            if (p.isBlue) {
                canvasAnim.SetBool("BlueWins", true);
            } else {
                canvasAnim.SetBool("RedWins", true);
            }

            foreach (CharacterMotor2D m in FindObjectsOfType<CharacterMotor2D>()) {
                m.enabled = false;
            }

            Invoke("End", 2.0f);
        }
        
    }

    void End() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
