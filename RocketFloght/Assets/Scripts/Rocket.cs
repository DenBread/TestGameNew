using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MainClass
{
    private int idLvl;
    private Rigidbody rb;
    private Vector3 rot; // угол поворота
    private Vector3 direction; // напрвление передвижения
    private Quaternion deltaRotation;
    private AudioSource audioSource; // звук для полета

    [SerializeField]
    private float speed; // скорость передвижения
    [SerializeField]
    private float speedRot; // скорость поворота

    [SerializeField] private GameObject creshRocket; // модель разрушения

    [Space]
    [Header("Audio Clip")]
    [SerializeField] private AudioClip flyingClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip finishClip;

    [Space]
    [Header("Partical System")]
    [SerializeField] private ParticleSystem RocketJetParticles; // эффект двигателей
    [SerializeField] private ParticleSystem ExplosionParticles; // эффект взрыва ракеты
    [SerializeField] private ParticleSystem SuccessParticles; // эффекст финиша

    enum State // состояния 
    {
        Playing, LoadingLvl, Dead
    }
    State state = State.Playing;

    protected override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rot = new Vector3(0, 0, 90f);
        direction = Vector3.up;
        deltaRotation = Quaternion.identity;
        state = State.Playing;
    }

    protected override void Update()
    {
        if (state == State.Playing)
        {
            Launch();
            RotaionRocket();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state == State.Dead || state == State.LoadingLvl)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
            case "Finish":
                Finish();
                break;
            case "Battery":
                print("PlusEnergy");
                break;
            default:
                Dead();
                break;
        }
    }

    private void Dead()
    {
        print("RocketBoom!");
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        
        // Кастыль
        creshRocket.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        rb.isKinematic = true;
        //

        RocketJetParticles.Stop();
        ExplosionParticles.Play();
        Invoke("LeadLvl", 2f);
    }

    private void Finish()
    {
        print("Finish");
        state = State.LoadingLvl;
        audioSource.Stop();
        audioSource.PlayOneShot(finishClip);

        RocketJetParticles.Stop();
        SuccessParticles.Play();
        Invoke("NextLvl", 2f);
    }

    private void NextLvl()
    {
        idLvl = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idLvl + 1);
    }

    private void LeadLvl()
    {
        idLvl = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idLvl);
    }

    private void Launch()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(direction * speed * Time.deltaTime, ForceMode.Force);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(flyingClip);
            RocketJetParticles.Play();
        }
        else
        {
            audioSource.Pause();
            RocketJetParticles.Stop();
        }
    }

    private void RotaionRocket()
    {
        rb.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            deltaRotation = Quaternion.Euler(rot * Time.deltaTime * speedRot);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            deltaRotation = Quaternion.Euler(-rot * Time.deltaTime * speedRot);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        rb.freezeRotation = false;
    }
}
