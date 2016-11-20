using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LeaderBoardController : MonoBehaviour {
    public MenuController MenuController;
    public Text HiScore;

    public void Show() {
        this.gameObject.SetActive(true);
        this.HiScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString(String.Format("#,##", CultureInfo.CreateSpecificCulture("el-GR")));

        MenuController.gameObject.SetActive(false);
    }

    public void Hide() {
        this.gameObject.SetActive(false);
        MenuController.gameObject.SetActive(true);
    }
}