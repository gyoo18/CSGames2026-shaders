#version 330 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform sampler2D u_texture; // textures/monalisa.jpg
uniform sampler2D u_bitmap; // textures/asciibitmap.png
const int g_bimapWidth = 18;
const int g_bimapHeight = 7;
const int char_larg = 7;
const int char_haut = 9;

uniform float u_time;
uniform bool u_showTexture = false;

layout(location=0) out vec4 color;

float[] bit_lum = float[g_bimapWidth*g_bimapHeight];

// Obtient la luminosité moyenne de chaque caractère et la stocke dans bit_lum
void generer_lum(){
  float lum = 0.0;
  for (int i = 0; i < g_bimapWidth*g_bimapHeight; i++){
    lum = 0.0;
    for (int x = 0; x < char_larg; x++){
      for (int y = 0; y < char_haut; y++){
        lum = getCharacter( vec2( float(x)/float(char_larg), float(y)/float(char_haut) ), i).r;
      }
    }
    lum /= float(g_bimapWidth*g_bimapHeight);
    bit_lum[i] = lum;
  }
}

int obtenir_meilleur_caractère(vec3 coul){
  float lum = max(coul.r, max(coul.g, coul.b));
  // Tester le meilleur match parmis tous les caractères en trouvant la distance minimale de luminosité
  int meilleur_i = 0;
  float meilleure_dist = 99999.0;
  for (int i = 0; i < g_bimapWidth*g_bimapHeight; i++){
    if (meilleure_dist > distance(lum,bit_lum[i])){
      meilleure_dist = distance(lum,bit_lum[i]);
      meilleur_i = i;
    }
  }
  return i;
}

float getCharacter(vec2 uv, int n) {
  return texture(u_bitmap, (uv + ivec2(n%g_bimapWidth, n/g_bimapWidth + g_bimapHeight-1))/vec2(g_bimapWidth, g_bimapHeight)).r;
}

void main(void) {
  // Init
  generer_lum();

  vec2 uv = gl_FragCoord.xy/u_resolution.xy;

  // Programme

  if (u_showTexture) {
    color = texture(u_texture, uv);
    return;
  }

  vec3 col = vec3(getCharacter(uv, int(u_time)%10));
  color = vec4(col, 1.);
}
