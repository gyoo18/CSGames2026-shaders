#version 430 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform float u_time;
uniform vec3 c_colors[5] = vec3[](
  vec3(0.408, 0.780, 0.757),
  vec3(0.980, 0.792, 0.471),
  vec3(0.961, 0.498, 0.345),
  vec3(0.867, 0.325, 0.255),
  vec3(0.475, 0.290, 0.227)
);

layout(location=0) out vec4 color;

void main(void) {
  vec2 uv = (gl_FragCoord.xy-.5*u_resolution.xy)/u_resolution.y;
  
  vec3 col = c_colors[0];

  // pro-tip: atan works well for all things circle
  
  color = vec4(col, 1);
}
