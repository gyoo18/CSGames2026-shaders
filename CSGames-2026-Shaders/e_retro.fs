#version 430 core

uniform vec2 u_resolution;
uniform float u_time;

in vec3 v_normal;
in vec2 v_uv;

layout(location=0) out vec4 color;

void main(void) {
  vec3 col;

#ifdef COMPUTER
  float light = dot(v_normal, normalize(vec3(1,1,1)))*.5+.5;
  col = vec3(mix(.3, 1., light));
#endif

#ifdef SCREEN
  col = vec3(v_uv.xy, 0);
#endif
  
  color = vec4(col, 1);
}
