using UnityEngine;
using System.Collections;

 public class redballoonscript : balloon {

    void Start()
    {
        speed = new Vector2(0, 2.5f);
        direction = new Vector2(0, 1);
        deflateRate = .001f;
        accel = 4.5f;
        floatingConst = 4;
    }
}
