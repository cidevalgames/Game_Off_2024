Shader "Custom/DeskMask" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry-1" }

        Pass {
            Stencil {
                Ref 1           // DeskMask marque la zone avec la valeur 1
                Comp Always     // Toujours passer le test de stencil
                Pass Replace    // Remplace la valeur du stencil avec Ref 1
            }
            ColorMask 0         // Ne rend pas de couleur, juste le masque
            SetTexture[_MainTex] {
                combine primary
            }
        }
    }
}