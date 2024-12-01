Shader "Custom/TacheMask" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
        Pass {
            Stencil {
                Ref 3          // FusainSprite is visible only where stencil value is 3 (both masks overlap)
                Comp Always    // Always pass the stencil test
                Pass IncrSat   // Increment by 1 (second increment)
            }
            ColorMask 0         // Don't render color, just the mask
            SetTexture[_MainTex] {
                combine primary
            }
        }
    }
}