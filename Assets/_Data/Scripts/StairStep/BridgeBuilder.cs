using UnityEngine;

public class StairBuilder : GameMonoBehaviour
{
    [SerializeField] private GameObject stepPrefab;
    [SerializeField] private Transform holder;
    [SerializeField] private int numberOfSteps = 17; 
    [SerializeField] private float stepWidth = 0.6f;
    [SerializeField] private float stepHeight = 0.08f;
    [SerializeField] private float stepDepth = 0.4f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStepPrefab();
        this.LoadHolder();
    }

    protected virtual void LoadStepPrefab()
    {
        if (this.stepPrefab != null) return;
        this.stepPrefab = transform.Find("Holder").Find("Ladder (1)").gameObject;
        Debug.LogWarning(transform.name + ": LoadStepPrefab", gameObject);
    } 
    
    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder").transform;
        Debug.LogWarning(transform.name + ": LoadHolder", gameObject);
    }

    protected override void Start()
    {
        BuildStairs();
    }

    private void BuildStairs()
    {
        for (int i = 0; i < numberOfSteps; i++)
        {
            Vector3 position = stepPrefab.transform.position + new Vector3(0, i * stepHeight, i * stepDepth);
            GameObject step = Instantiate(stepPrefab, position, Quaternion.identity);
            step.gameObject.SetActive(true);
            step.transform.localScale = new Vector3(stepWidth, stepHeight, stepDepth);
            step.transform.SetParent(holder);
        }
    }
}
