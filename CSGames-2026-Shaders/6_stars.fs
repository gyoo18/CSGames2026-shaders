#version 430 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform float u_time;

layout(location=0) out vec4 color;

void main(void) {
  vec2 uv = (gl_FragCoord.xy-.5*u_resolution.xy)/u_resolution.y;
  
  vec3 col = vec3(0);

  vec2 cell = floor(uv*10.);
  vec2 f = fract(uv*10.);
  vec2 starPos = vec2(rand(cell), rand(cell*1.3+7.));

  // This makes circle stars, but hard edges, not very nice
  col += step(length(f - starPos), .05);
  
  color = vec4(col, 1);
}
