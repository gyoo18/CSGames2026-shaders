// Some random functions that might be useful

// use like "color.r = rand(uv)", returns in [0,1[
float rand(vec2 u) {
  return fract(sin(dot(u, vec2(12.9898, 78.233))) * 43758.5453);
}

float rand(float f) {
  return fract(sin(f * 6342.63175) * 43758.5453);
}

// a smooth kind of rand, use like "color.r = noise(uv * 12)", returns in [0,1[
float noise(vec2 u) {
  vec4 f = vec4(floor(u), floor(u+1));
  vec2 z = fract(u);
  return mix(mix(rand(f.xy), rand(f.zy), z.x), mix(rand(f.xw), rand(f.zw), z.x), z.y);
}

float saturate(float f) {
  return clamp(f, 0., 1.);
}

// useful for compositing colors with alpha
// use like "compose(color, vec3(1,0,0), 0.5)" to modify "color" by adding a red overlay with 50% alpha
void compose(inout vec4 color, vec3 applied, float alpha) {
  color.rgb = mix(applied * alpha, color.rgb, color.a) / mix(alpha, 1, color.a);
  color.a = mix(alpha, 1, color.a);
}
void compose(inout vec4 color, vec4 applied) {
  color.rgb = mix(applied.rgb * applied.a, color.rgb, color.a) / mix(applied.a, 1, color.a);
  color.a = mix(applied.a, 1, color.a);
}