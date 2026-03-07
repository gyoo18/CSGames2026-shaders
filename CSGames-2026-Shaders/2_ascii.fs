#version 330 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform sampler2D u_texture; // textures/monalisa.jpg
uniform sampler2D u_bitmap; // textures/asciibitmap.png
const int g_bimapWidth = 18;
const int g_bimapHeight = 7;

uniform float u_time;
uniform bool u_showTexture = false;

layout(location=0) out vec4 color;

float getCharacter(vec2 uv, int n) {
  return texture(u_bitmap, (uv + ivec2(n%g_bimapWidth, n/g_bimapWidth + g_bimapHeight-1))/vec2(g_bimapWidth, g_bimapHeight)).r;
}

void main(void) {
  vec2 uv = gl_FragCoord.xy/u_resolution.xy;

  if (u_showTexture) {
    color = texture(u_texture, uv);
    return;
  }

  vec3 col = vec3(getCharacter(uv, int(u_time)%10));
  color = vec4(col, 1.);
}
