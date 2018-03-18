using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryObject {
    public string countryName;
    public string continent;
    public int id;

    public string food;
    public string flag;
    public string landmark;
    public string celebrity;
    public string language;
    public int weather;
    public float rotx;
    public float roty;
    public bool chosen;

	public CountryObject(int cid, string name, string continenth = "", string language = "", int weather = 0,  float rotx = 0f, float roty = 0f)
    {
        id = cid;
        countryName = name;
        continent = continenth;
        this.language = language;
        this.weather = weather;
        this.rotx = rotx;
        this.roty = roty;
        chosen = false;
    }

    public bool Equals(CountryObject c)
    {
        return c.id == this.id;
    }

    public float score()
    {
        float x = Camera.main.transform.localEulerAngles.x > 180 ? Camera.main.transform.localEulerAngles.x - 360 : Camera.main.transform.localEulerAngles.x;
        x = Mathf.Max(-30, Mathf.Min(x, 30));
        float y = Camera.main.transform.localEulerAngles.y;
        y = Mathf.Abs(y - roty);
        if (y > 180)
            y = 360 - y;
        return Mathf.Abs(x - rotx) + y;
    }

}
