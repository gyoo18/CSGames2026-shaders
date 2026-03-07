#version 430 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform float u_time;
uniform vec2 u_cursor; // in (0..u_resolution)

const int nb_cases = 10;
const float PI = 3.1416;

layout(location=0) out vec4 color;

// Go read utils.glsl first, you might find some useful functions there

vec2 rotation(vec2 uv, float r){
  mat2 rot;
  rot[0][0] = cos(r);
  rot[0][1] = sin(r);
  rot[1][0] = -sin(r);
  rot[1][1] = cos(r);

  return rot*uv;
}

void main(void) {
  vec2 uv = (gl_FragCoord.xy-.5*u_resolution.xy)/u_resolution.y;
  vec2 uv_case_a = mod(uv*vec2(nb_cases),1.0) * 2.0 - 1.0;
  vec2 uv_case_b = floor(uv*vec2(nb_cases))/vec2(nb_cases);

  float r = rand(uv_case_b);
  if (r < 0.25){
    uv_case_a = rotation(uv_case_a, PI/4.0);
  }else if(r < 0.5){
    uv_case_a = rotation(uv_case_a, 3.0*PI/4.0);
  }else if(r < 0.75){
    uv_case_a = rotation(uv_case_a, 5.0*PI/4.0);
  }else{
    uv_case_a = rotation(uv_case_a, 7.0*PI/4.0);
  }
  float ligne = float( distance(abs(uv_case_a.y), sin(PI/4.0)) < 0.2);
  color = vec4(mix(vec3(0.1,0.3,0.5),vec3(0.1,0.6,0.8),ligne), 1.0);
}
