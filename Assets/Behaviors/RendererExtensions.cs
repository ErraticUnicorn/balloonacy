using UnityEngine;
using System.Collections;

public static class RendererExtensions {

    //Exention class that is accessible by any of the objects in the game

    public static bool IsVisibleFrom(this Renderer renderer, Camera camera) {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
