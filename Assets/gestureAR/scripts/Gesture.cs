using System.Collections;
using System.Collections.Generic;

public class Gesture
{
    public string question;
    public string correct;
    public string incorrect;

    public Gesture(string question, string correct, string incorrect)
    {
        this.question = question;
        this.correct = correct;
        this.incorrect = incorrect;
    }
}
