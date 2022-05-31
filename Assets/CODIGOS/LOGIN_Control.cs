using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LOGIN_Control : MonoBehaviour
{
    private string secretKey = "mySecretKey";
    //public string addScoreURL = 
    //        "http://127.0.0.1/CHILDDAY_2022/Assets/CODIGOS/addscore.php?";
    public string loginURL = 
            "http://127.0.0.1/CHILDDAY_2022/Assets/CODIGOS/login.php?";
    //public string loginURL = 
    //        "https://oxfordcollege.net/2022/login.php?";
    public InputField dniTextInput;
    
    void Start() 
    {
        // Chequeo si hay algo en las PlayerPrefs
        if(PlayerPrefs.HasKey("id") && PlayerPrefs.HasKey("coins") && PlayerPrefs.HasKey("name")) {
            // Voy a HOME
            SceneManager.LoadScene("HOME", LoadSceneMode.Single); // Closes all current loaded Scenes and loads a Scene.
        }
    }

    public void GetLoginBtn()
    {
        //idResultText.text = "ID: \n \n";
        //coinsResultText.text = "Coins: \n \n";
        StartCoroutine(getLoginInfo(dniTextInput.text));
    }

    IEnumerator getLoginInfo(string dni)
    {
        string hash = HashInput(dni + secretKey);
        string get_url = loginURL + "dni=" + dni + "&hash=" + hash;
        UnityWebRequest hs_get = UnityWebRequest.Post(get_url, hash);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null) {
            Debug.Log("There was an error posting the high score: " 
                    + hs_get.error);
        }
        else
        {
            string dataText = hs_get.downloadHandler.text;

            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                if(int.Parse(splitData[1]) != 0) { 
                    // splitData[1] trae el valor del id. 
                    // splitData[2] la cantidad de coins.
                    // splitData[3] nombre completo.
                    // Si id != 0 significa que el php encontró un usuario con el dni ingresado.
                    
                    // Guardo los datos que necesito en las PlayerPrefs.
                    PlayerPrefs.SetInt("id", int.Parse(splitData[1]));
                    PlayerPrefs.SetInt("coins", int.Parse(splitData[2]));
                    PlayerPrefs.SetString("name", splitData[3]);
                    
                    PlayerPrefs.Save();
                    // Voy al home.
                    SceneManager.LoadScene("HOME", LoadSceneMode.Single); // Closes all current loaded Scenes and loads a Scene.
                } else {
                    // si id == 0 entonces no existe un alumno en la bd con el dni ingresado.
                    //idResultText.text = "No se encontró un alumno con el dni ingresado.";
                    //coinsResultText.text = "Intenté de nuevo.";
                }
            }
        } 
    }
/*
    public void SendScoreBtn()
    {
        StartCoroutine(PostScores(Convert.ToInt32(dniTextInput.text), 54));

        dniTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
    }
*/
    
/*
    IEnumerator PostScores(int dni, int coins)
    {
        string hash = HashInput(dni + coins + secretKey);
        string post_url = addScoreURL + "dni=" + dni + "&oxfordcredits=" 
            + coins + "&hash=" + hash;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: " 
                    + hs_post.error);
    }
*/

    public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue = 	
                hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert = 
                BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }
}
