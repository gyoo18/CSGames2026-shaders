#version 430 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform float u_time;
uniform vec2 u_cursor; // in (0..u_resolution)

layout(location=0) out vec4 color;

// Go read utils.glsl first, you might find some useful functions there

void main(void) {
  vec2 uv = (gl_FragCoord.xy-.5*u_resolution.xy)/u_resolution.y;
  
  vec3 col = vec3(0);

  vec2 p = fract(uv * 10.);

  // When doing things with p, it's as if it were applied to all cells in the grid, try those out!
  // if (length(p - .5) < .3) col.rgb += 1;
  // col.rg = p;
  // if (abs(p.r - p.g) < .1) col.rgb += 1;

  // smoothstep might be useful for anti-aliasing
  col.rgb += smoothstep(.1, 0., abs(p.x-.5));

  color = vec4(col, 1);
}
