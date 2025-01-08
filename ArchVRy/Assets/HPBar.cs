using UnityEngine;
using TMPro;
public class HPBar : MonoBehaviour
{
    public TMP_Text text;
    NPC npc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
        npc = transform.GetComponentInParent<NPC>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "HP: " + npc.hp;
    }
}
