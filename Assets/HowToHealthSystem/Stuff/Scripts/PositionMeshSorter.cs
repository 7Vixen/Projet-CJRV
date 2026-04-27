using UnityEngine;
using System.Collections;

public class PositionMeshSorter : MonoBehaviour {

    public int sortingOrderBase = 5000;
    public int offset = 0;
    private MeshRenderer meshRenderer;
    public bool runOnlyOnce = false;
    private float timer;
    private const float timerMax = .1f;

    void Awake() {
        meshRenderer = transform.GetComponent<MeshRenderer>();
    }
	void LateUpdate () {
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            timer = timerMax;
            meshRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y + offset);
            if (runOnlyOnce) Destroy(this);
        }
	}
}
