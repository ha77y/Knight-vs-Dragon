
using UnityEngine;


public class AttackSwooshScript : MonoBehaviour
{

    private Material _shaderMat;
    private float _t;
    public bool attacking;
    public float attackingTime = 0.5f;

    private static readonly int Progress = Shader.PropertyToID("_progress");

    // Start is called before the first frame update
    void Start()
    {
        _shaderMat = gameObject.GetComponent<MeshRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            if (_t <= attackingTime)
            {
                _t += Time.deltaTime;
                _shaderMat.SetFloat(Progress,  Mathf.Lerp(1,0,_t/attackingTime));
            }
            else
            {
                attacking = false;
            }
           
        }
        else
        {
            _t = 0;
            _shaderMat.SetFloat(Progress,1);
        }
    }
}
