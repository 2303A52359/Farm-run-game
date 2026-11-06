using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class WinterThemeSetup
{
    private const string MainScene = "Assets/Scenes/mainGameScene.unity";
    private const string LockerScene = "Assets/Scenes/LockerMenu.unity";
    private const string SnowMaterialPath = "Assets/Materials/WinterSnow.mat";

    [MenuItem("Winter Theme/Apply to Main Game + Locker Menu")]
    public static void ApplyToProjectScenes()
    {
        ApplyToScene(MainScene, true);
        ApplyToScene(LockerScene, false);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog(
            "Winter Theme Applied",
            "Winter lighting and atmosphere have been added to the main game and locker scenes. Open the main game scene and press Play to test.",
            "OK");
    }

    [MenuItem("Winter Theme/Apply to Current Scene")]
    public static void ApplyToCurrentScene()
    {
        ApplyWinterLook(SceneManager.GetActiveScene(), true);
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        AssetDatabase.SaveAssets();
    }

    private static void ApplyToScene(string scenePath, bool addSnow)
    {
        if (!File.Exists(scenePath))
        {
            Debug.LogWarning("Winter Theme: scene not found: " + scenePath);
            return;
        }

        Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        ApplyWinterLook(scene, addSnow);
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
    }

    private static void ApplyWinterLook(Scene scene, bool addSnow)
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogColor = new Color(0.70f, 0.82f, 0.94f, 1f);
        RenderSettings.fogDensity = 0.006f;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.62f, 0.72f, 0.86f, 1f);
        RenderSettings.reflectionIntensity = 0.75f;

        foreach (GameObject go in scene.GetRootGameObjects())
        {
            Light[] lights = go.GetComponentsInChildren<Light>(true);
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    light.color = new Color(0.78f, 0.86f, 1f, 1f);
                    light.intensity = Mathf.Max(light.intensity, 0.85f);
                }
            }
        }

        if (addSnow)
            CreateOrUpdateSnow(scene);
    }

    private static void CreateOrUpdateSnow(Scene scene)
    {
        GameObject existing = GameObject.Find("Winter Snowfall");
        if (existing != null)
            Object.DestroyImmediate(existing);

        Camera cam = Camera.main;
        GameObject snowObject = new GameObject("Winter Snowfall");
        SceneManager.MoveGameObjectToScene(snowObject, scene);
        if (cam != null)
            snowObject.transform.SetParent(cam.transform, false);

        ParticleSystem ps = snowObject.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.loop = true;
        main.playOnAwake = true;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.startLifetime = new ParticleSystem.MinMaxCurve(8f, 13f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.4f, 1.4f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.035f, 0.11f);
        main.startColor = new Color(0.94f, 0.98f, 1f, 0.9f);
        main.maxParticles = 3500;
        main.gravityModifier = 0.12f;

        var emission = ps.emission;
        emission.rateOverTime = 260f;

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(34f, 18f, 30f);

        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.x = new ParticleSystem.MinMaxCurve(-0.25f, 0.25f);
        velocity.y = new ParticleSystem.MinMaxCurve(-1.2f, -0.55f);
        velocity.z = new ParticleSystem.MinMaxCurve(-0.15f, 0.15f);

        var renderer = snowObject.GetComponent<ParticleSystemRenderer>();
        Material snowMat = GetSnowMaterial();
        if (snowMat != null)
            renderer.sharedMaterial = snowMat;
        renderer.renderMode = ParticleSystemRenderMode.Billboard;
        renderer.sortingFudge = 2f;

        if (cam != null)
            snowObject.transform.localPosition = new Vector3(0f, 5f, 9f);
        else
            snowObject.transform.position = Vector3.zero;

        ps.Play();
    }

    private static Material GetSnowMaterial()
    {
        Material material = AssetDatabase.LoadAssetAtPath<Material>(SnowMaterialPath);
        if (material != null)
            return material;

        Shader shader = Shader.Find("Universal Render Pipeline/Particles/Unlit");
        if (shader == null)
            shader = Shader.Find("Particles/Standard Unlit");
        if (shader == null)
            shader = Shader.Find("Sprites/Default");
        if (shader == null)
            return null;

        material = new Material(shader);
        material.name = "Winter Snow";
        AssetDatabase.CreateAsset(material, SnowMaterialPath);
        AssetDatabase.SaveAssets();
        return material;
    }
}
