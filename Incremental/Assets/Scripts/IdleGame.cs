using UnityEngine;
using UnityEngine.UI;

public class IdleGame : MonoBehaviour
{

    public Text expText;
    public Text expPerSecText;
    public double exp;
    public double expPerSec;
    public double questValue;

    public Text questText;
    public Text questUpgrade1Text;
    public Text repeatMissionUpgrade1Text;

    public Text questUpgrade2Text;
    public Text repeatMissionUpgrade2Text;

    public double questUpgrade1Cost;
    public int questUpgrade1Level;

    public double repeatMissionUpgrade1Cost;
    public int repeatMissionUpgrade1Level;

    public double questUpgrade2Cost;
    public double questUpgrade2Power;
    public int questUpgrade2Level;

    public double repeatMissionUpgrade2Cost;
    public double repeatMissionUpgrade2Power;
    public int repeatMissionUpgrade2Level;

    public Text generationText;
    public Text generationTalentText;
    public Text generationBoostText;
    public Text nextGenerationIncreaseText;

    public double generation;
    public double generationTalents;
    public double generationBoost;
    public double nextGenerationTalentIncrease;


    public void Start()
    {
        exp = 0;
        questValue = 1;
        questUpgrade1Cost = 25;
        repeatMissionUpgrade1Cost = 100;
        questUpgrade2Cost = 120;
        repeatMissionUpgrade2Cost = 550;

        questUpgrade2Power = 5;
        repeatMissionUpgrade2Power = 5;

        Load();

        if (questUpgrade1Cost > 1000)
        {
            var sciNotation = ScientificNotation(questUpgrade1Cost);
            questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + sciNotation + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;
        }
        else
            questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + questUpgrade1Cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;

        if (repeatMissionUpgrade1Cost > 1000)
        {
            var sciNotation = ScientificNotation(repeatMissionUpgrade1Cost);
            repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + sciNotation + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;
        }
        else
            repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + repeatMissionUpgrade1Cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;


        if (questUpgrade2Cost > 1000)
        {
            var sciNotation = ScientificNotation(questUpgrade2Cost);
            questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + sciNotation + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;
        }
        else
            questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + questUpgrade2Cost.ToString("F0") + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;

        if (repeatMissionUpgrade2Cost > 1000)
        {
            var sciNotation = ScientificNotation(repeatMissionUpgrade2Cost);
            repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + sciNotation + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
        }
        else
            repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + repeatMissionUpgrade2Cost.ToString("F0") + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
    }

    public void Load()
    {
        exp = double.Parse(PlayerPrefs.GetString("exp", "0"));
        questValue = double.Parse(PlayerPrefs.GetString("questValue", "1"));
        questUpgrade1Cost = double.Parse(PlayerPrefs.GetString("questUpgrade1Cost", "25"));
        repeatMissionUpgrade1Cost = double.Parse(PlayerPrefs.GetString("repeatMissionUpgrade1Cost", "100"));
        questUpgrade2Cost = double.Parse(PlayerPrefs.GetString("questUpgrade2Cost", "120"));
        repeatMissionUpgrade2Cost = double.Parse(PlayerPrefs.GetString("repeatMissionUpgrade2Cost", "550"));

        questUpgrade1Level = PlayerPrefs.GetInt("questUpgrade1Level", 0);
        questUpgrade2Level = PlayerPrefs.GetInt("questUpgrade2Level", 0);
        repeatMissionUpgrade1Level = PlayerPrefs.GetInt("repeatMissionUpgrade1Level", 0);
        repeatMissionUpgrade2Level = PlayerPrefs.GetInt("repeatMissionUpgrade2Level", 0);

        questUpgrade2Power = double.Parse(PlayerPrefs.GetString("questUpgrade2Power", "5"));
        repeatMissionUpgrade2Power = double.Parse(PlayerPrefs.GetString("repeatMissionUpgrade2Power", "5"));

        generation = double.Parse(PlayerPrefs.GetString("generation", "0"));
        generationTalents = double.Parse(PlayerPrefs.GetString("generationTalents", "0"));
        generationBoost = double.Parse(PlayerPrefs.GetString("generationBoost", "0"));
    }

    public void Save()
    {
        PlayerPrefs.SetString("exp", exp.ToString());
        PlayerPrefs.SetString("questValue", questValue.ToString());
        PlayerPrefs.SetString("questUpgrade1Cost", questUpgrade1Cost.ToString());
        PlayerPrefs.SetString("repeatMissionUpgrade1Cost", repeatMissionUpgrade1Cost.ToString());
        PlayerPrefs.SetString("questUpgrade2Cost", questUpgrade2Cost.ToString());
        PlayerPrefs.SetString("repeatMissionUpgrade2Cost", repeatMissionUpgrade2Cost.ToString());

        PlayerPrefs.SetInt("questUpgrade1Level", questUpgrade1Level);
        PlayerPrefs.SetInt("questUpgrade2Level", questUpgrade2Level);
        PlayerPrefs.SetInt("repeatMissionUpgrade1Level", repeatMissionUpgrade1Level);
        PlayerPrefs.SetInt("repeatMissionUpgrade2Level", repeatMissionUpgrade2Level);

        PlayerPrefs.SetString("questUpgrade2Power", questUpgrade2Power.ToString());
        PlayerPrefs.SetString("repeatMissionUpgrade2Power", repeatMissionUpgrade2Power.ToString());

        PlayerPrefs.SetString("generation", generation.ToString());
        PlayerPrefs.SetString("generationTalents", generationTalents.ToString());
        PlayerPrefs.SetString("generationBoost", generationBoost.ToString());
    }

    public void Update()
    {
        nextGenerationTalentIncrease = (150 * System.Math.Sqrt(exp / 1e7));
        nextGenerationIncreaseText.text = "Teach:\n+" + System.Math.Floor(nextGenerationTalentIncrease).ToString("F0") + " Talents";
        generationText.text = "Generation: " + generation;
        generationBoostText.text = generationBoost.ToString("F2") + "X Boost";
        generationTalentText.text = "Talents: " + System.Math.Floor(generationTalents).ToString("F0");

        expPerSec = (repeatMissionUpgrade1Level + (repeatMissionUpgrade2Power * repeatMissionUpgrade2Level)) * generationBoost;


        if (questValue > 1000)
        {
            var sciNotation = ScientificNotation(questValue);
            questText.text = "Quest: +" + sciNotation + " EXP";
        }
        else
            questText.text = "Quest: +" + questValue.ToString("F2") + " EXP";

        if (exp > 1000)
        {
            var sciNotation = ScientificNotation(exp);
            expText.text = "EXP: " + sciNotation;
        }
        else
            expText.text = "EXP: " + exp.ToString("F0");

        if (expPerSec > 1000)
        {
            var sciNotation = ScientificNotation(expPerSec);
            expPerSecText.text = sciNotation + " EXP/s";
        }
        else
            expPerSecText.text = expPerSec.ToString("F2") + " EXP/s";

        exp += expPerSec * Time.deltaTime;

        Save();
    }

    //Buttons
    public void GainEXP()
    {
        exp += questValue;
    }

    public void BuyQuestUpgrade1()
    {
        if (exp >= questUpgrade1Cost)
        {
            questUpgrade1Level++;
            exp -= questUpgrade1Cost;
            questUpgrade1Cost *= 1.07;
            questValue += generationBoost;

            if (questUpgrade1Cost > 1000)
            {
                var sciNotation = ScientificNotation(questUpgrade1Cost);
                questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + sciNotation + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;
            }
            else
                questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + questUpgrade1Cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;
        }
    }

    public void BuyRepeatMissionUpgrade1()
    {
        if (exp >= repeatMissionUpgrade1Cost)
        {
            repeatMissionUpgrade1Level++;
            exp -= repeatMissionUpgrade1Cost;
            repeatMissionUpgrade1Cost *= 1.15;

            if (repeatMissionUpgrade1Cost > 1000)
            {
                var sciNotation = ScientificNotation(repeatMissionUpgrade1Cost);
                repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + sciNotation + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;
            }
            else
                repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + repeatMissionUpgrade1Cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;
        }
    }

    public void BuyQuestUpgrade2()
    {
        if (exp >= questUpgrade2Cost)
        {
            questUpgrade2Level++;
            exp -= questUpgrade2Cost;
            questUpgrade2Cost *= 1.09;
            questValue += (questUpgrade2Power * generationBoost);

            if (questUpgrade2Cost > 1000)
            {
                var sciNotation = ScientificNotation(questUpgrade2Cost);
                questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + sciNotation + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;
            }
            else
                questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + questUpgrade2Cost.ToString("F0") + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;
        }
    }

    public void BuyRepeatMissionUpgrade2()
    {
        if (exp >= repeatMissionUpgrade2Cost)
        {
            repeatMissionUpgrade2Level++;
            exp -= repeatMissionUpgrade2Cost;
            repeatMissionUpgrade2Cost *= 1.17;

            if (repeatMissionUpgrade2Cost > 1000)
            {
                var sciNotation = ScientificNotation(repeatMissionUpgrade2Cost);
                repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + sciNotation + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
            }
            else
                repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + repeatMissionUpgrade2Cost.ToString("F0") + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
        }
    }

    public string ScientificNotation(double Value)
    {
        var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(Value))));
        var mantissa = (Value / System.Math.Pow(10, exponent));
        return mantissa.ToString("F2") + "e" + exponent;
    }

    public void Teach()
    {
        if(exp > 1000)
        {
            exp = 0;
            questValue = 1;
            questUpgrade1Cost = 25;
            repeatMissionUpgrade1Cost = 100;
            questUpgrade2Cost = 120;
            repeatMissionUpgrade2Cost = 550;

            questUpgrade1Level = 0;
            questUpgrade2Level = 0;
            repeatMissionUpgrade1Level = 0;
            repeatMissionUpgrade2Level = 0;

            questUpgrade2Power = 5;
            repeatMissionUpgrade2Power = 5;

            generation++;
            generationTalents += nextGenerationTalentIncrease;
            generationBoost = ((generation + generationTalents) * 0.05) + 1;

            questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + questUpgrade1Cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;
            repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + repeatMissionUpgrade1Cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;
            questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + questUpgrade2Cost.ToString("F0") + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;
            repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + repeatMissionUpgrade2Cost.ToString("F0") + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
        }
    }
}