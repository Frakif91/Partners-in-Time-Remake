#include <Packages/com.blendernodesgraph.core/Editor/Includes/Importers.hlsl>

void New_Node_Graph_float(float3 _POS, float3 _PVS, float3 _PWS, float3 _NOS, float3 _NVS, float3 _NWS, float3 _NTS, float3 _TWS, float3 _BTWS, float3 _UV, float3 _SP, float3 _VVS, float3 _VWS, float name, Texture2D gradient_130672, out float name)
{
	
	float4 _ColorRamp_130672 = color_ramp(gradient_130672, name);

	name = _ColorRamp_130672;
}