#version 430 core

#include "utils.glsl"

uniform vec2 u_resolution;

uniform int u_iterations = 10;
uniform vec2 u_zoomTarget = vec2(-1.4, 0.001);
uniform float u_zoom = 1.0; // when applying zoom use exp(u_zoom), otherwise controls are way too sensitive

layout(location=0) out vec4 color;

void main(void) {
  vec2 uv = (gl_FragCoord.xy-.5*u_resolution.xy)/u_resolution.y;
  vec2 c = uv * 5.0;
  vec3 col = vec3(0);

  vec2 z = vec2(0);
  // This is the general formula, I'll let you figure out how to color it nicely
  vec2 z1 = vec2(z.x*z.x - z.y*z.y, 2.0*z.x*z.y) + c;
  vec2 z2 = vec2(z1.x*z1.x - z1.y*z1.y, 2.0*z1.x*z1.y) + c;
  vec2 z3 = vec2(z2.x*z2.x - z2.y*z2.y, 2.0*z2.x*z2.y) + c;

  color.rgb = vec3(step(length(z3), 2.0));
  color.a = 1;
}
