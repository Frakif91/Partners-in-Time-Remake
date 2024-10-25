#include <Packages/com.blendernodesgraph.core/Editor/Includes/Importers.hlsl>

void New_Node_Graph_1_float(float3 _POS, float3 _PVS, float3 _PWS, float3 _NOS, float3 _NVS, float3 _NWS, float3 _NTS, float3 _TWS, float3 _BTWS, float3 _UV, float3 _SP, float3 _VVS, float3 _VWS, Texture2D gradient_14614, Texture2D gradient_39846, out float name)
{
	
	float _SimpleNoiseTexture_14170_fac; float4 _SimpleNoiseTexture_14170_col; node_simple_noise_texture_full(_POS, 0, 10,5, 11,6, 0,5, 2, 1, _SimpleNoiseTexture_14170_fac, _SimpleNoiseTexture_14170_col);
	float4 _ColorRamp_39846 = color_ramp(gradient_39846, _SimpleNoiseTexture_14170_col);
	float4 _ColorRamp_14614 = color_ramp(gradient_14614, _SimpleNoiseTexture_14170_col);
	float4 _MixRGB_22050 = mix_blend(_ColorRamp_39846, _ColorRamp_39846, _ColorRamp_14614);

	name = _MixRGB_22050;
}