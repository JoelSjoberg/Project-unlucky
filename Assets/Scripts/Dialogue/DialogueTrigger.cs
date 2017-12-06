using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;

    public float lifeTime = 3f, timer = 0, publicAlpha = 0.95f;
    private CanvasGroup cg;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        timer = lifeTime;
        cg.alpha = 0;
    }

    private void Update()
    {
        if (timer < lifeTime && cg.alpha < publicAlpha) cg.alpha += Time.deltaTime;
        else if(timer < lifeTime) timer += Time.deltaTime;
        else if(cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime;
        }
    }

    public void WriteText(string name, string sentence)
    {
        nameText.text = name;
        dialogueText.text = sentence;

        Vector3 pos = new Vector3(0, 0, 0) ;

        float x = Random.Range(200, Screen.width - 200);
        float y = Random.Range(200, Screen.height - 200);
        pos.x = x;
        pos.y = y;
        pos.z = transform.position.z;
        transform.position = pos;

        timer = 0;
    }

    public bool ready() { return (cg.alpha == 0); }
}
