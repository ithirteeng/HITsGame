using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject buff;
    public GameObject brokenEggModel;
    private float _deltaTime;
    private GameObject[] brokensEggs = new GameObject[10];
    private int ind;
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject model = Instantiate(brokenEggModel);
        model.transform.SetParent(buff.transform);
        model.transform.position = new Vector3(col.transform.position.x, -3.75f, 0f);
        Destroy(col.gameObject);
        Destroy(brokensEggs[ind]);
        brokensEggs[ind] = model;
        ind++;
        if (ind >= 10) ind = 0;
    }
}
