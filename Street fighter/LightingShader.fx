// LightingShader.fx

// Maximum number of point lights
#define MAX_POINT_LIGHTS 10

// Uniform variables for lighting properties
float4 AmbientColor;
float AmbientIntensity;
bool LightingEnabled;

// Structure to hold point light properties
struct PointLight
{
    float2 position;
    float4 Color;
    float Intensity;
    float Radius;
};

// Array to store point lights
PointLight PointLights[MAX_POINT_LIGHTS];

float4 PixelShaderFunction(float4 position : SV_position, float4 color : COLOR0) : COLOR0
{
    float4 finalColor = color;

    if (LightingEnabled)
    {
        // Apply ambient light
        finalColor += AmbientColor * AmbientIntensity;

        // Apply point lights
        for (int i = 0; i < MAX_POINT_LIGHTS; i++)
        {
            PointLight light = PointLights[i];

            // Calculate distance to light
            float2 toLight = light.position - position.xy;
            float distance = length(toLight);

            // Calculate light intensity based on distance and radius
            float lightIntensity = light.Intensity * max(0.0, 1.0 - distance / light.Radius);

            // Apply light color and intensity
            finalColor += light.Color * lightIntensity;
        }
    }

    // Apply the final color to the pixel
    return finalColor;
}
