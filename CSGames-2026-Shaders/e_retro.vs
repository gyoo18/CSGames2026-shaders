#version 330 core

#define PI 3.1415926535

uniform vec2 u_resolution;

layout(location=0) in vec4 i_pos;
layout(location=1) in vec3 i_normal;
layout(location=2) in vec2 i_uv;
out vec2 v_uv;
out vec3 v_normal;

mat4 translation(vec3 v) {
  return mat4(
    1, 0, 0, 0,
    0, 1, 0, 0,
    0, 0, 1, 0,
    v.x, v.y, v.z, 1);
}

mat4 perspective(float fov, float near, float far) {
  float scale = 1 / tan(fov * PI / 360);
  float zmap = far/(near-far);
  float aspect = u_resolution.x / u_resolution.y;
  return mat4(
    scale / aspect, 0, 0, 0,
    0, scale, 0, 0,
    0, 0, zmap, -1,
    0, 0, zmap*near, 0);
}

mat4 lookat(vec3 origin, vec3 dir, vec3 up) {
  vec3 ww = normalize(-dir);
  vec3 uu = normalize(cross(ww, up));
  vec3 vv = normalize(cross(uu, ww));
  return
    inverse(mat4(mat3(uu, vv, ww))) *
    translation(-origin)
  ;
}

void main(void) {
  vec3 cameraPosition = vec3(1,.5,0);
  vec3 cameraLookAt = vec3(0,.2,0);
  mat4 MVP = 
    perspective(45, .01, 1000) *
    lookat(cameraPosition, cameraLookAt - cameraPosition, vec3(0, 1, 0));
  gl_Position = MVP * i_pos;
  v_uv = i_uv;
  v_normal = i_normal;
}
