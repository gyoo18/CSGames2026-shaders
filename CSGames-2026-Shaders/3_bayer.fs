#version 330 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform sampler2D u_texture; // textures/machinarium-og-image.jpg

uniform float u_scale = 128.0;

layout(location=0) out vec4 color;

void main(void) {
  vec2 uv = gl_FragCoord.xy/u_resolution.xy;

  uv = floor(uv * u_scale) / u_scale;
  color = texture(u_texture, uv);
  color.a = 1;
}
