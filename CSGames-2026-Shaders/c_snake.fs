#version 430 core

uniform vec2 u_resolution;
uniform float u_time;

layout(std430, binding = 0) buffer buffer_0
{
  int snake_length;
  ivec2 snake[];
};

layout(location=0) out vec4 color;

void main(void) {
  vec2 uv = (gl_FragCoord.xy-.5*u_resolution.xy)/u_resolution.y;
  
  // add a background
  vec3 col = (.1+.1*cos(u_time+uv.xyx+vec3(2, 4, 0))) * step(.5, max(abs(uv.x), abs(uv.y)));
  // and a border
  col += step(abs(max(abs(uv.x), abs(uv.y)) - .5), .01) * .5;

  // then you can do anything in the center
  // don't forget that the grid size is defined by the GRID_SIZE macro

  color = vec4(col, 1);
}
