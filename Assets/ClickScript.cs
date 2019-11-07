using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickScript : MonoBehaviour
{
    private Image image;
    private DateTime begin;
    private DateTime ftbegin;
    private DateTime fpsBegin;
    private int wait;
    private System.Random rand = new System.Random();
    private Text statistics;
    private Text fps;
    private Text frameTime;
    private int fpsCount;
    private List<double> results = new List<double>();

    void Start()
    {
        fpsBegin = DateTime.Now;
        image = GameObject.Find("Button").GetComponent<Image>();
        statistics = GameObject.Find("Statistics").GetComponent<Text>();
        fps = GameObject.Find("FPS").GetComponent<Text>();
        frameTime= GameObject.Find("FrameTime").GetComponent<Text>();
    }

    void Update()
    {
       
        if (ftbegin != null)
        {
            var time = (DateTime.Now - ftbegin).TotalMilliseconds;
            frameTime.text = $"Frame Time: {time.ToString()}";
        }

        if((DateTime.Now-fpsBegin).TotalSeconds>1)
        {
            fps.text = $"FPS: {fpsCount}";
            fpsBegin = DateTime.Now;
            fpsCount = 0;
        }
           
        if (image.color == Color.yellow && (DateTime.Now - begin).TotalMilliseconds >= wait)
        {
            image.color = Color.green;
            begin = DateTime.Now;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (image.color == Color.green)
            {
                var result = Math.Round((DateTime.Now - begin).TotalMilliseconds);
                results.Add(result);
                statistics.text = $"Last result: {result.ToString()}ms\n";
                PrintResults(3, 5, 10, 20);
                image.color = Color.white;
            }   
            else if (image.color == Color.white || image.color == Color.red)
            {
                image.color = Color.yellow;
                wait = rand.Next(400, 3000);
                begin = DateTime.Now;
            }
            else if (image.color == Color.yellow)
                image.color = Color.red;
        }

        ftbegin = DateTime.Now;
        fpsCount++;
    }

    void PrintResults(params int[] counts)
    {
        foreach (int count in counts)
            PrintCertainAmountOfLastResults(count);
    }

    void PrintCertainAmountOfLastResults(int count)
    {
        if (results.Count >= count)
        {
            double averageLastTimes = 0;
            for (int i = 1; i <= count; i++)
                averageLastTimes += results[results.Count - i];

            averageLastTimes = Math.Round(averageLastTimes / count);
            statistics.text += $"Last {count} times avg result: {averageLastTimes}\n";
        }
    }
}
