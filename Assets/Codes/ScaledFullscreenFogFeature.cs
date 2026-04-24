using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class ScaledFullscreenFogFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material fogMaterial;
        [Range(0.3f, 1f)] public float renderScale = 0.5f;
        public RenderPassEvent injectionPoint = RenderPassEvent.AfterRenderingPostProcessing;
    }

    class FogPass : ScriptableRenderPass
    {
        Settings settings;

        public FogPass(Settings settings)
        {
            this.settings = settings;
            requiresIntermediateTexture = true;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (settings.fogMaterial == null) return;

            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();

            if (resourceData.isActiveTargetBackBuffer) return;

            TextureHandle source = resourceData.activeColorTexture;

            UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
            RenderTextureDescriptor desc = cameraData.cameraTargetDescriptor;
            desc.width = Mathf.RoundToInt(desc.width * settings.renderScale);
            desc.height = Mathf.RoundToInt(desc.height * settings.renderScale);
            desc.msaaSamples = 1;

            TextureHandle fogRT = UniversalRenderer.CreateRenderGraphTexture(
                renderGraph, desc, "_FogRT", false, FilterMode.Bilinear);

            using (var builder = renderGraph.AddRasterRenderPass<PassData>("Scaled Fog", out var passData))
            {
                passData.source = source;
                passData.fogRT = fogRT;
                passData.material = settings.fogMaterial;

                builder.UseTexture(source);
                builder.SetRenderAttachment(fogRT, 0);
                builder.SetRenderFunc((PassData data, RasterGraphContext context) =>
                {
                    Blitter.BlitTexture(context.cmd, data.source, new Vector4(1, 1, 0, 0), data.material, 0);
                });
            }

            using (var builder = renderGraph.AddRasterRenderPass<PassData>("Scaled Fog Upscale", out var passData))
            {
                passData.source = fogRT;
                passData.material = null;

                builder.UseTexture(fogRT);
                builder.SetRenderAttachment(source, 0);
                builder.SetRenderFunc((PassData data, RasterGraphContext context) =>
                {
                    Blitter.BlitTexture(context.cmd, data.source, new Vector4(1, 1, 0, 0), 0, false);
                });
            }
        }

        class PassData
        {
            public TextureHandle source;
            public TextureHandle fogRT;
            public Material material;
        }
    }

    public Settings settings = new Settings();
    FogPass pass;

    public override void Create()
    {
        pass = new FogPass(settings);
        pass.renderPassEvent = settings.injectionPoint;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData data)
    {
        if (settings.fogMaterial != null)
            renderer.EnqueuePass(pass);
    }
}