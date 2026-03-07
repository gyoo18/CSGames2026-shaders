#version 430 core

uniform sampler2D u_texture;

layout(location=0) out vec4 color;

void main(void) {
  color = texelFetch(u_texture, ivec2(gl_FragCoord.xy), 0);
}
