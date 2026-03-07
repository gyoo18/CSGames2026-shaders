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

float[] bit_lum = float[g_bimapWidth*g_bimapHeight](0.0);

float getCharacter(vec2 uv, int n) {
  return texture(u_bitmap, (uv + ivec2(n%g_bimapWidth, n/g_bimapWidth + g_bimapHeight-1))/vec2(g_bimapWidth, g_bimapHeight)).r;
}

// Obtient la luminosité moyenne de chaque caractère et la stocke dans bit_lum
void generer_lum(){
  float lum = 0.0;
  for (int i = 0; i < g_bimapWidth*g_bimapHeight; i++){
    lum = 0.0;
    for (int x = 1; x < char_larg-1; x++){
      for (int y = 1; y < char_haut-1; y++){
        lum += getCharacter( vec2( float(x)/float(char_larg), float(y)/float(char_haut) ), i);
      }
    }
    lum /= float((g_bimapWidth-2)*(g_bimapHeight-2));
    bit_lum[i] = lum;
  }
}

int obtenir_meilleur_caractere(vec3 coul){
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
  return meilleur_i;
}

void main(void) {
  // Init
  generer_lum();

  vec2 uv = gl_FragCoord.xy/u_resolution.xy;

  // Programme
  ivec2 taille_texture = textureSize(u_texture,0)/4;
  vec2 uv_carac = mod(vec2(uv*taille_texture),vec2(1.0,1.0));
  vec2 uv_case = floor(uv*taille_texture)/taille_texture;

  // color = vec4(uv_carac.xy,0.0,1.0);
  color = texture(u_texture, uv_case);
  color *= getCharacter(uv_carac, obtenir_meilleur_caractere(color.rgb));
  // color = vec4(vec3(getCharacter(uv_carac, obtenir_meilleur_caractere(color.rgb))),1.0);
  color.a = 1.0;
  // color = vec4(vec3(getCharacter(uv,int(mod(u_time,10.0))))*bit_lum[int(mod(u_time,10.0))],1.0);
}
