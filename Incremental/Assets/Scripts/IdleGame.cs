using UnityEngine;
using UnityEngine.UI;

public class IdleGame : MonoBehaviour
{
    //Variables
    //Basic EXP
    public Text expText;
    public Text expPerSecText;
    public double exp;
    public double expPerSec;
    public int buyState;

    //Quest
    public Text questText;
    public double questValue;

    //Upgrade text
    public Text questUpgrade1Text;
    public Text repeatMissionUpgrade1Text;
    public Text questUpgrade2Text;
    public Text repeatMissionUpgrade2Text;

    public int questUpgrade1Level;

    public int repeatMissionUpgrade1Level;

    public double questUpgrade2Power;
    public int questUpgrade2Level;

    public double repeatMissionUpgrade2Power;
    public int repeatMissionUpgrade2Level;

    //Generation
    public Text generationText;
    public Text generationTalentText;
    public Text generationBoostText;
    public Text nextGenerationIncreaseText;

    public double generation;
    public double generationTalents;
    public double generationBoost;
    public double nextGenerationTalentIncrease;

    //Progress Bars
    public Image questUpgrade1Bar;
    public Image repeatMissionUpgrade1Bar;
    public Image questUpgrade2Bar;
    public Image repeatMissionUpgrade2Bar;
    public Image generationBar;


    //Functions
    //Basic Functions
    public void Start()
    {
        //Load/Create Start Values
        Load();

        //Text Fields
        var cost = 25 * System.Math.Pow(1.07, questUpgrade1Level);
        if (cost > 1000)
        {
            var sciNotation = ScientificNotation(cost);
            questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + sciNotation + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;
        }
        else
            questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;

        cost = 100 * System.Math.Pow(1.15, repeatMissionUpgrade1Level);
        if (cost > 1000)
        {
            var sciNotation = ScientificNotation(cost);
            repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + sciNotation + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;
        }
        else
            repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;

        cost = 120 * System.Math.Pow(1.09, questUpgrade2Level);
        if (cost > 1000)
        {
            var sciNotation = ScientificNotation(cost);
            questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + sciNotation + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;
        }
        else
            questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + cost.ToString("F0") + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;

        cost = 550 * System.Math.Pow(1.17, repeatMissionUpgrade2Level);
        if (cost > 1000)
        {
            var sciNotation = ScientificNotation(cost);
            repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + sciNotation + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
        }
        else
            repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + cost.ToString("F0") + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
    }

    public void Update()
    {
        //Values that change frequently
        nextGenerationTalentIncrease = (150 * System.Math.Sqrt(exp / 1e7));
        nextGenerationIncreaseText.text = "Teach:\n+" + System.Math.Floor(nextGenerationTalentIncrease).ToString("F0") + " Talents";
        generationText.text = "Generation: " + generation;
        generationBoostText.text = generationBoost.ToString("F2") + "X Boost";
        generationTalentText.text = "Talents: " + System.Math.Floor(generationTalents).ToString("F0");

        expPerSec = (repeatMissionUpgrade1Level + (repeatMissionUpgrade2Power * repeatMissionUpgrade2Level)) * generationBoost;

        //Text Fields
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

        //Progress Bars
        ProgressBar(questUpgrade1Bar, exp, Cost(25, 1.07, questUpgrade1Level));
        ProgressBar(repeatMissionUpgrade1Bar, exp, Cost(100, 1.15, repeatMissionUpgrade1Level));
        ProgressBar(questUpgrade2Bar, exp, Cost(120, 1.09, questUpgrade2Level));
        ProgressBar(repeatMissionUpgrade2Bar, exp, Cost(550, 1.17, repeatMissionUpgrade2Level));
        ProgressBar(generationBar, exp, 1000);

        //Save Current Progress
        Save();
    }

    //Save/Load Functions
    public void Load()
    {
        exp = double.Parse(PlayerPrefs.GetString("exp", "0"));
        questValue = double.Parse(PlayerPrefs.GetString("questValue", "1"));

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

    //Buttons
    public void GainEXP()
    {
        exp += questValue;
    }

    public void BuyQuestUpgrade1()
    {
        BuyState(25, 1.07, ref questUpgrade1Level, ref questValue);

        var cost = 25 * System.Math.Pow(1.07, questUpgrade1Level);
        if (cost > 1000)
        {
            var sciNotation = ScientificNotation(cost);
            questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + sciNotation + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;
        }
        else
            questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;
    }

    public void BuyRepeatMissionUpgrade1()
    {
        BuyState(100, 1.15, ref repeatMissionUpgrade1Level);

        var cost = 100 * System.Math.Pow(1.15, repeatMissionUpgrade1Level);
        if (cost > 1000)
        {
            var sciNotation = ScientificNotation(cost);
            repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + sciNotation + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;
        }
        else
            repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + cost.ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;
    }

    public void BuyQuestUpgrade2()
    {
            BuyState(120, 1.09, ref questUpgrade2Level, ref questValue, questUpgrade2Power);

            var cost = 120 * System.Math.Pow(1.09, questUpgrade2Level);
            if (cost > 1000)
            {
                var sciNotation = ScientificNotation(cost);
                questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + sciNotation + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;
            }
            else
                questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + cost.ToString("F0") + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;
    }

    public void BuyRepeatMissionUpgrade2()
    {
            BuyState(550, 1.17, ref repeatMissionUpgrade2Level);

            var cost = 550 * System.Math.Pow(1.17, repeatMissionUpgrade2Level);
            if (cost > 1000)
            {
                var sciNotation = ScientificNotation(cost);
                repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + sciNotation + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
            }
            else
                repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + cost.ToString("F0") + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
    }

    public void Teach()
    {
        if(exp > 1000)
        {
            exp = 0;
            questValue = 1;

            questUpgrade1Level = 0;
            questUpgrade2Level = 0;
            repeatMissionUpgrade1Level = 0;
            repeatMissionUpgrade2Level = 0;

            questUpgrade2Power = 5;
            repeatMissionUpgrade2Power = 5;

            generation++;
            generationTalents += nextGenerationTalentIncrease;
            generationBoost = ((generation + generationTalents) * 0.05) + 1;

            questUpgrade1Text.text = "Quest Upgrade 1\nCost: " + Cost(25, 1.07, questUpgrade1Level).ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade1Level;
            repeatMissionUpgrade1Text.text = "Repeat Mission 1\nCost: " + Cost(100, 1.15, repeatMissionUpgrade1Level).ToString("F0") + " EXP\n+" + generationBoost.ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade1Level;
            questUpgrade2Text.text = "Quest Upgrade 2\nCost: " + Cost(120, 1.09, questUpgrade2Level).ToString("F0") + " EXP\n+" + (questUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Quest\nLevel: " + questUpgrade2Level;
            repeatMissionUpgrade2Text.text = "Repeat Mission 2\nCost: " + Cost(550, 1.17, repeatMissionUpgrade2Level).ToString("F0") + " EXP\n+" + (repeatMissionUpgrade2Power * generationBoost).ToString("F2") + " Exp Per Second\nLevel: " + repeatMissionUpgrade2Level;
        }
    }

    public void BuyMaxButton()
    {
        buyState = 2;
    }

    public void BuyTenButton()
    {
        buyState = 1;
    }

    public void BuyOneButton()
    {
        buyState = 0;
    }

    //Universal Functions
    public string ScientificNotation(double Value)
    {
        var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(Value))));
        var mantissa = (Value / System.Math.Pow(10, exponent));
        return mantissa.ToString("F2") + "e" + exponent;
    }

    public void ProgressBar(Image bar, double current, double max)
    {
        if (current / max < 0.01)
        {
            bar.fillAmount = 0;
        }
        else if (current / max > 1)
        {
            bar.fillAmount = 1;
        }
        else
        {
            bar.fillAmount = (float)(current / max);
        }
    }

    public void BuyState(double baseCost, double rate, ref int level)
    {
        double value = 0;
        BuyState(baseCost, rate, ref level, ref value);
    }

    public void BuyState(double baseCost, double rate, ref int level, ref double value)
    {
        double power = 0;
        BuyState(baseCost, rate, ref level, ref value, power);
    }

    public void BuyState(double baseCost, double rate, ref int level, ref double value, double power)
    {
        switch(buyState)
        {
            case 1:
                BuyOne(baseCost, rate, ref level, ref value, power);
                break;
            case 2:
                BuyMax(baseCost, rate, ref level, ref value, power);
                break;
            default:
                BuyOne(baseCost, rate, ref level, ref value, power);
                break;
        }
    }

    public void BuyOne(double baseCost, double rate, ref int level, ref double value, double power)
    {
        var cost = baseCost * System.Math.Pow(rate, level);
        if (exp >= cost)
        {
            level++;
            exp -= cost;
            if (power != 0)
                value += (power * generationBoost);
            else
                value += generationBoost;
        }
    }

    public void BuyMax(double baseCost, double rate, ref int level, ref double value, double power)
    {
        var e = exp;
        var k = level;
        var m = System.Math.Floor(System.Math.Log((e * (rate - 1)) / (baseCost * System.Math.Pow(rate, k)) + 1, rate));

        var cost = baseCost * ((System.Math.Pow(rate, k) * (System.Math.Pow(rate, m) - 1)) / (rate - 1));

        if (exp >= cost)
        {
            level += (int)m;
            exp -= cost;
            if (power != 0)
                value += (m * (power * generationBoost));
            else
                value += (m * generationBoost);
        }
    }

    public double Cost(double initial, double rate, int level)
    {
        return initial * System.Math.Pow(rate, level);
    }
}
