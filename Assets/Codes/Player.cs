using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    [SerializeField] private float hiz;
    [SerializeField] private bool yerde;
    [SerializeField] private float ziplamaGucu = 5f;


    private bool sagaBak;
    private bool yerdeMi;
    [SerializeField] private Transform zeminKontrol; 
    [SerializeField] private LayerMask zeminKatmani;

    void Start()
    {
        sagaBak = true;
        myRigidbody = GetComponent<Rigidbody2D> ();   
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        float yatay = Input.GetAxis("Horizontal");
        TemelHareketler(yatay);
        YonCevir(yatay);
        Zipla();  
        YerKontrol();   
          
    }
    
    private void TemelHareketler(float yatay){

        myRigidbody.linearVelocity = new Vector2 (yatay*hiz, myRigidbody.linearVelocity.y);
        myAnimator.SetFloat("karakterHizi", Mathf.Abs(yatay));
    }

    private void YonCevir(float yatay){
        if(yatay>0 && !sagaBak || yatay < 0 && sagaBak){
            sagaBak= !sagaBak;
            Vector3 yon = transform.localScale;
            yon.x *=-1;
            transform.localScale = yon;
        }
    }

    private void Zipla(){
        if (Input.GetKeyDown(KeyCode.Space) && yerdeMi)
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, ziplamaGucu);
            myAnimator.SetTrigger("zipla"); 
            }
        
    }

    private void YerKontrol(){
        bool oncekiYerdeMi = yerdeMi;

        yerdeMi = Physics2D.OverlapCircle(zeminKontrol.position, 0.1f, zeminKatmani);

        if (yerdeMi && !oncekiYerdeMi) {
         Invoke(nameof(ResetZiplaTrigger), 0.05f); 
        }
    
        myAnimator.SetBool("yerde", yerdeMi);
    }

    private void ResetZiplaTrigger(){
        myAnimator.ResetTrigger("zipla");
    }

}



    