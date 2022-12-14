using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class vidasContador : MonoBehaviour
{
    private GameObject[] vidas;
    public GameObject prefab;
    public GameObject padreObject;
    public GameObject target;
    public ParticleSystem particles;
    public int numVidas;
    private bool dead;
    //sobrecarga paremetrica csm..... ~~~
    private string vidascont = "vidas";
    
    void Start()
    {
        
        loadVidas();
        if (numVidas <= 0)
        {
            numVidas = 5;
            saveVidas();
        }
        Debug.Log("asdsa" + numVidas);
        vidas = new GameObject[numVidas];
        for (var i = 0; i < numVidas; i++)
        {
            GameObject a = Instantiate(prefab,padreObject.transform);
            a.transform.position = new Vector3(a.transform.position.x + (50*i), a.transform.position.y , 0);
            vidas[i] = a;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("asdsa" + numVidas);
        if (target.GetComponent<BarraVida>().vida <= 0)
        {
            Descontar();
        }
        if (dead == true)
        {
            SceneManager.LoadScene("IntroMenu");
        }
    }

    public void Descontar()
    {
        if (numVidas >= 0)
        {
            numVidas--;
            var cloneParticulas = Instantiate(particles, transform.position,particles.transform.rotation);
            Destroy(vidas[numVidas].gameObject);
            Destroy(cloneParticulas, cloneParticulas.main.duration);
            target.SetActive(false);
            myasync();
            //SceneManager.LoadScene("Scene1");
        }
        if (numVidas == 0)
        {
            dead = true;
        }
    }

    private async void myasync(){
        //numVidas--;
        //Destroy(vidas[numVidas].gameObject);
        await Task.Delay(TimeSpan.FromSeconds(4));
        SceneManager.LoadSceneAsync("Scene1");
        
    }
   
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "caida"){
            Descontar();
        }
    } 

    private void OnDestroy()
    {
        saveVidas();
    }
    private void saveVidas()
    {
        PlayerPrefs.SetInt(vidascont,numVidas);
    }
    private void loadVidas()
    {

        numVidas = PlayerPrefs.GetInt(vidascont,numVidas);
    }
    
}
