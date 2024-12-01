Shader "Custom/FusainMask" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
        Pass {
            Stencil {
                Ref 2           // Fusain Mask writes Ref 2 in the stencil buffer
                Comp Always     // Always pass the stencil test
                Pass Replace    // Replace the stencil buffer value with Ref 2
            }
            ColorMask 0         // Don't render color, just update the stencil
            SetTexture[_MainTex] {
                combine primary
            }
        }
    }
}