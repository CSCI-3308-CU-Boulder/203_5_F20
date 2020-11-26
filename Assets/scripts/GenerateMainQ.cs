using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
//using System.Text;
//using System.Diagnostics;
//using System;

public class GenerateMainQ : MonoBehaviour {

	public Text TextBox;
	public Text Option1;
	public Text Option2;
	public Text Option3;
	public Text Option4;
	public int globalIndex = 0;
	public GameObject a;
	public GameObject serverConnection;
	public int QuestionNum = 1;
	public string QuestionText = "";


	public void Start() {
		serverConnection = GameObject.Find("ServerCode");
		serverConnection.GetComponent<ServerConnect>().startGame();
		nextQuestion();
	}
	void Update()
	{
		//Debug.Log(a.GetComponent<TimerCount>().timeRemaining);
		if (a.GetComponent<TimerCount>().timeRemaining <= 0)
		{
			//Debug.Log(a.GetComponent<TimerCount>().timeRemaining);
			nextQuestion();
		}
	}
	public void readTextFileQ(){
		System.Random rnd = new System.Random();
		StreamReader reader = new StreamReader("./Assets//Resources/QuoteListQuestions.txt");
		string itemStrings = reader.ReadLine();
		// char[] delimiter = { '\n' };

		IList<string> arlist = new List<string>();
		while (itemStrings != null) {
			// string[] fields = itemStrings.Split (delimiter);
			// arlist.Add(itemStrings.Split (delimiter));
			arlist.Add(itemStrings);
			// for (int i = 0; i < arlist.Count; i++) {
			//	Debug.Log (arlist [i]);
			//	Debug.Log (i);
			// }
			itemStrings = reader.ReadLine ();

			int randIndex = rnd.Next (arlist.Count); // arlist.Count
			globalIndex = randIndex;
			TextBox.GetComponent<Text> ().text = arlist [randIndex]; // fields [randIndex];
			QuestionText = arlist[randIndex];
		}
	}

		
	public void readTextFileA(){
		System.Random rndAnswer = new System.Random();
		int answerToChange = rndAnswer.Next(1, 5);  // creates a number between 1 and 4
		Debug.Log(answerToChange);

		StreamReader readerA = new StreamReader("./Assets//Resources/QuoteListAnswers.txt");
		string itemStringsA = readerA.ReadLine();
		// char[] delimiter = { '\n' };

		IList<string> arlistA = new List<string>();
		while (itemStringsA != null) {
			// string[] fieldsA = itemStrings.Split (delimiter);
			arlistA.Add (itemStringsA);
			// for (int i = 0; i < arlistA.Count; i++) {
			//	 Debug.Log (arlistA [i]);
			// }
			itemStringsA = readerA.ReadLine ();
		}
			Debug.Log ("Count: " + arlistA.Count);
			System.Random rndRef = new System.Random();
			int randIndex1 = rndRef.Next (0, arlistA.Count);
			int randIndex2 = rndRef.Next (0, arlistA.Count);
			int randIndex3 = rndRef.Next (0, arlistA.Count);
			int randIndex4 = rndRef.Next (0, arlistA.Count);
			if (randIndex1 == randIndex2)
			{
				randIndex1 = rndRef.Next(0, arlistA.Count);
			}
			if (randIndex2 == randIndex3)
			{
				randIndex2 = rndRef.Next(0, arlistA.Count);
			}
			if (randIndex3 == randIndex4)
			{
				randIndex3 = rndRef.Next(0, arlistA.Count);
			}
			if (randIndex4 == randIndex1)
			{
				randIndex4 = rndRef.Next(0, arlistA.Count);
			}
			if (randIndex4 == randIndex2)
			{
				randIndex4 = rndRef.Next(0, arlistA.Count);
			}
			if (randIndex3 == randIndex1)
			{
				randIndex3 = rndRef.Next(0, arlistA.Count);
			}
			//Edge cases for if the answers are the same
			while (arlistA[randIndex1] == arlistA[randIndex2] || arlistA[randIndex2] == arlistA[randIndex3] || arlistA[randIndex3] == arlistA[randIndex4] || arlistA[randIndex4] == arlistA[randIndex1] || arlistA[randIndex4] == arlistA[randIndex2] || arlistA[randIndex3] == arlistA[randIndex1]) {
				if (arlistA[randIndex1] == arlistA[randIndex2])
				{
					randIndex1 = rndRef.Next(0, arlistA.Count);
				}
				if (arlistA[randIndex2] == arlistA[randIndex3])
				{
					randIndex2 = rndRef.Next(0, arlistA.Count);
				}
				if (arlistA[randIndex3] == arlistA[randIndex4])
				{
					randIndex3 = rndRef.Next(0, arlistA.Count);
				}
				if (arlistA[randIndex4] == arlistA[randIndex1])
				{
					randIndex4 = rndRef.Next(0, arlistA.Count);
				}
				if (arlistA[randIndex4] == arlistA[randIndex2])
				{
					randIndex4 = rndRef.Next(0, arlistA.Count);
				}
				if (arlistA[randIndex3] == arlistA[randIndex1])
				{
					randIndex3 = rndRef.Next(0, arlistA.Count);
				}
			}
		if (answerToChange == 1) {
				randIndex1 = globalIndex;
			}
			if (answerToChange == 2) {
				randIndex2 = globalIndex;
			}
			if (answerToChange == 3) {
				randIndex3 = globalIndex;
			}
			if (answerToChange == 4) {
				randIndex4 = globalIndex;
			}
		serverConnection.GetComponent<ServerConnect>().correctAnswers(answerToChange);
		Debug.Log ("Index1: " + randIndex1);
		Debug.Log ("Index2: " +randIndex2);
		Debug.Log ("Index3: " +randIndex3);
		Debug.Log ("Index4: " +randIndex4);
		Debug.Log ("List size: " + arlistA.Count);
		//Debug.Log (fieldsA.Length);
		//Debug.Log (itemStrings.Length);

			Option1.GetComponent<Text> ().text = arlistA [randIndex1];
			Option2.GetComponent<Text> ().text = arlistA [randIndex2];
			Option3.GetComponent<Text> ().text = arlistA [randIndex3];
			Option4.GetComponent<Text> ().text = arlistA [randIndex4];
	}
	private void nextQuestion()
	{
		serverConnection.GetComponent<ServerConnect>().printScore();
		readTextFileQ();
		readTextFileA();
		serverConnection.GetComponent<ServerConnect>().newQuestion(QuestionNum, QuestionText);
		QuestionNum++;
	}
}