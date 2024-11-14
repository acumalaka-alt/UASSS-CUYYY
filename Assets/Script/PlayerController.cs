using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    bool isJump = false;
    bool isDead = false;
    int idMove = 0;
    Animator anim;
    Rigidbody2D rb;

    public GameObject Projectile;
    public Vector2 projectileVelocity;
    public Vector2 projectOffset;
    public float Cooldown = 0.5f;
    bool isCanShoot = true;
    public bool pause;
    public GameObject pauseUI;
    public GameObject Gameover;
    //public int TextD = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isCanShoot = true;  // Inisialisasi ke true agar bisa menembak di awal
        EnemyController.EnemyKilled = 0;
        pauseUI.SetActive(false);
        Time.timeScale=1f;
        Gameover.SetActive(false);
        //TextD = 0;
        ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (idMove == 1 || idMove == 2)
            {
                Idle();
            }
            idMove = 0;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Fire();
        }
        Move();
        Dead();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if(!pause)
        {
            Time.timeScale=0f;
            pauseUI.SetActive(true);
            pause=true;
            Debug.Log("paused");
        }
        else
        {
            Time.timeScale=1f;
            pauseUI.SetActive(false);
            pause=false;
            Debug.Log("unpaused");
        }

    }
    void Fire()
    {
        if (isCanShoot && !isDead)  // Tambahkan pengecekan isDead
        {
            GameObject bullet = Instantiate(Projectile, (Vector2)transform.position - projectOffset * transform.localScale.x, Quaternion.identity);

            Vector2 velocity = new Vector2(projectileVelocity.x * transform.localScale.x, projectileVelocity.y);
            bullet.GetComponent<Rigidbody2D>().velocity = velocity * -1;

            bullet.transform.localScale = transform.localScale * -1;  // Set scale agar arah peluru sesuai arah player

            StartCoroutine(CanShoot());
            anim.SetTrigger("shoot");
        }
    }

    IEnumerator CanShoot()
    {
        isCanShoot = false;
        yield return new WaitForSeconds(Cooldown);
        isCanShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Peluru"))
        {
            isCanShoot = true;
        }
        if (collision.transform.tag.Equals("Enemy"))
        {
            Gameover.SetActive(true);
            isDead = true;
            Debug.Log("dead");

        }
        if (collision.transform.tag.Equals("Gay"))
        {
            Debug.Log("DED");
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isJump)
        {
            anim.ResetTrigger("jump");
            if (idMove == 0) anim.SetTrigger("idle");
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isJump = true;
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Coin"))
        {
            Data.score += 15;
            Destroy(collision.gameObject);
        }
    }

    public void MoveRight()
    {
        idMove = 1;
    }

    public void MoveLeft()
    {
        idMove = 2;
    }

    private void Move()
    {
        if (idMove == 1 && !isDead)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (idMove == 2 && !isDead)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(-1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void Jump()
    {
        if (!isJump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJump = true;
            anim.SetTrigger("jump");
        }
    }

    public void Idle()
    {
        if (!isJump)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("run");
            anim.SetTrigger("idle");
        }
    }

    private void Dead()
    {
        if (!isDead)
        {
            if (transform.position.y < -10f)
            {
                isDead = true;
            }
        }
    }
    public void ResetScore()
    {
        Data.score = 0;
    }



}
