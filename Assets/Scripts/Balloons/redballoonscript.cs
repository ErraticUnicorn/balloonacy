using UnityEngine;
using System.Collections;

 public class redballoonscript : balloon {

    void Start()
    {
        speed = new Vector2(0, 2.5f);
        direction = new Vector2(0, 1);
        deflaterate = .001f;
        accel = 4.5f;
        floatingconst = 4;
    }
}
