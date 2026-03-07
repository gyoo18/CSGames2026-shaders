#version 430 core

#include "utils.glsl"

uniform vec2 u_resolution;
uniform float u_frame;

uniform sampler2D u_texture;

layout(location=0) out vec4 color;

const ivec2 c_glider[5] = ivec2[](ivec2(1,0),ivec2(2,1),ivec2(0,2),ivec2(1,2),ivec2(2,2));

void main(void) {
  ivec2 uv = ivec2(gl_FragCoord.xy);

  // You can uncomment the last line to get a random grid,
  // but appart from that please don't change the initial state
#ifdef INIT
  color = vec4(0,0,0,1);
  for(int i=0; i<5; i++)
    color += (uv == c_glider[i]+ivec2(50,50))?1:0;
  // color = rand(uv) < .1 ? vec4(1,1,1,1) : vec4(0,0,0,1);
  return;
#endif
  
  bool cell = texelFetch(u_texture, uv, 0).r > 0.5;

  color = vec4(vec3(cell?1:0), 1);
}
