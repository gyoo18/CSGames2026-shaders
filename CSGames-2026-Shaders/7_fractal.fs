#version 430 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform float u_time;

uniform int u_iterations = 6;

layout(location=0) out vec4 color;

float sierpinsky(vec2 n) {
  // start with white
  float w = 1;
  for(int i = 0; i < u_iterations; i++) {
    // at each iteration, remove the middle triangle by multiplying with 0
    w *= (
      step(0, n.x) *
      step(0, n.y) *
      step(0, .5*(1-n.x-n.y))
    );
    // and "zoom in" by a factor 2, this is the big idea to make any self-similar fractal
    n = fract(2*n);
  }
  return w;
}

void main(void) {
  vec2 uv = (gl_FragCoord.xy-.5*u_resolution.xy)/u_resolution.y;

  // this centers the triangle on the screen
  uv = vec2(uv.x+.5, sqrt(3)*.25+uv.y);
  // this matrix makes it easier to do math on the triangle, you probably won't need it
  // to draw a square carpet
  vec2 n = mat2(1, 0, -1/sqrt(3.), 2./sqrt(3.)) * uv;
  
  color = vec4(vec3(sierpinsky(n)), 1);
}
