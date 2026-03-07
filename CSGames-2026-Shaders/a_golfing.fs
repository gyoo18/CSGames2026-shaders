#version 330 core

#define PI 3.141592

uniform vec2 u_resolution;
uniform float u_time;
uniform float u_sides = 21.28;
uniform int u_layers = 19;
uniform float u_rotationSpeed = 0.7;
uniform float u_movementSpeed = 1.0;
uniform vec2 u_winding = vec2(1.07,0.23);

uniform vec3 c_palette[] = vec3[](
  vec3(0.428, 0.518, 0.718),
  vec3(0.228, 0.438, 0.328),
  vec3(0.388, 0.388, 0.296),
  vec3(1.490, 2.478, 0.538)
);

layout(location=0) out vec4 color;

float rand(float f) {
  return fract(sin(f * 6342.63175) * 43758.5453);
}

float rand(vec2 u) {
  return fract(sin(dot(u, vec2(12.9898, 78.233))) * 43758.5453);
}

float randrange(vec2 u, float min, float max) {
  return rand(u) * (max-min) + min;
}

vec3 palette(in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d) {
  return a + b*cos(6.28318*(c*t+d));
}

vec3 palette(float t) {
  return t*palette(t, c_palette[0], c_palette[1], c_palette[2], c_palette[3]);
}

mat2 rot2D(float rad) {
  return mat2(
     cos(rad), sin(rad),
    -sin(rad), cos(rad)
  );
}

void main(void) {
  vec2 uv = (gl_FragCoord.xy-.5*u_resolution.xy)/u_resolution.y;
  
  vec3 col = vec3(0);

  float alpha = 0;
  float rt = -u_time*u_rotationSpeed;
  float mt = -u_time*u_movementSpeed;

  for(int layer = 0; layer < u_layers; layer++) {
    int layerId = layer - int(mt);
    float layerDepth = (layer+fract(mt))/(u_layers+1);
    float globalRotation = dot(vec2(layerDepth*PI*2, rt), u_winding);
    vec2 uuv =
      rot2D(rt * mix(randrange(vec2(layerId), -.1,+.1), .2, .4))
      * (uv
        + vec2(randrange(vec2(layerId), -.01, .01))
        + vec2(cos(globalRotation), sin(globalRotation))*.05);
    float theta = atan(uuv.y, uuv.x);
    int sector = int((theta/PI/2+.5) * u_sides);
    float layerAlpha = 0;
    for(int i = -1; i <= 1; i++) {
      int s = sector + i;
      float thetaS = PI*((1.+2*s)/u_sides + 1) + randrange(vec2(s, 0), -.1, +.1);
      vec2 ruv =
        mat2(cos(thetaS), sin(thetaS), sin(thetaS), -cos(thetaS))
        * uuv / exp(2-layerDepth*4.5);
      layerAlpha = max(layerAlpha,
        max(
          smoothstep(.04, .035, abs(ruv.y) - randrange(vec2(s, 2), -.01, .01)),
          smoothstep(.3, .34, ruv.x))
        * smoothstep(.2, .22, ruv.x - randrange(vec2(s, 1), -.01, .05))
      );
      if(layerAlpha == 1) break;
    }
    layerAlpha *= min(1, u_layers-layer-fract(mt));
    float dt = 1/(1+130*pow(sin(u_time/PI*1.4-layerDepth), 2));
    col += (1-alpha) * layerAlpha * palette(layerDepth+dt*.1);
    alpha = min(alpha + layerAlpha, 1);
  }

  col += (1-alpha) * palette(1);

  color = vec4(col, 1);


}
