Shader "Custom/DeskMask" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry-1" }

        Pass {
            Stencil {
                Ref 1          // DeskMask: Mark the zone with the value 2
                Comp Always    // Always pass the stencil test
                Pass IncrSat   // Replace the stencil buffer with Ref 2
            }
            ColorMask 0         // Don't render color, just the mask
            SetTexture[_MainTex] {
                combine primary
            }
        }
    }
}