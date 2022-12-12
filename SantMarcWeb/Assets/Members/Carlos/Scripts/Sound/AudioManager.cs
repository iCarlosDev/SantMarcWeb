using System;
using System.Collections;
using System.Collections.Generic;
using Members.Carlos.Scripts;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;
    
    public int ModelajeStarsAmount = 0;
    public int TexturizadoStarsAmount = 0;
    public int ProgramacionStarsAmount = 0;
    
    ///////////////////////////////////////////
    [Header("--- COCHE ---")]
    [Space(20)]
    public GameObject cocheTochoM;
    public GameObject cocheNormalM;
    public GameObject cochePochoM;
    public GameObject currentCar;

    [Header("--- TEXTURAS COCHE ---")] 
    public List<Material> TexturasCocheTocho;
    public List<Material> TexturasCocheNormal;
    public List<Material> TexturasCochePocho;
    
    [Header("--- TEXTURAS RUEDAS ---")] 
    public List<Material> TexturasRuedaTocha;
    public List<Material> TexturasRuedaNormal;
    public List<Material> TexturasRuedaPocha;

    [Header("--- AVION ---")]
    [Space(20)]
    public GameObject avionTochoM;
    public GameObject avionNormalM;
    public GameObject avionPochoM;
    public GameObject currentAvion;

    [Header("--- TEXTURAS AVION ---")] 
    public List<Material> TexturasAvionTocho;
    public List<Material> TexturasAvionNormal;
    public List<Material> TexturasAvionPocho;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
           s.source.loop = s.loop;

        }
    }

    private void Start()
    {
        Play("Background");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
