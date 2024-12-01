Shader "Custom/BougieMask" {
 Properties {
        _MainTex ("Texture", 2D) = "white" {} // Required by Sprite Renderer
    }
    SubShader {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry-1" }
        Pass {
            Stencil {
                Ref 2           // This is the stencil reference value
                Comp Always     // Always write to the stencil buffer
                Pass Replace    // Replace stencil buffer value with Ref
            }
            ColorMask 0         // Don't render any visible color (mask only)
        }
    }
}
