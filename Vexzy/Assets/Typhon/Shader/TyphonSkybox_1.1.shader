Shader "Typhon/SkyBox1.1"
{
    Properties {

        _Exposure("Exposure", Range(0, 8)) = 1
        _SunSize ("Sun Size", Range(0,1)) = 0.04
        _SunExpozure ("Sun Exposure", Range(0,8)) = 1
        [Toggle] _TimeWithColor ("Using not only Light's Direction but also Light's Color", Float) = 1

        [Header(Sky Config)]
        _GroundAngle ("Ground Angle", Range(-1.0, -0.001)) = -0.1
        _HorizonAngle ("Horizon Angle", Range(-1.0, 1.0)) = 0.0
        _SkyAngle ("Sky Angle", Range(0.001, 1.0)) = 0.75
        _TransisionCoefficient ("Transision Coefficient", Range(0.001, 10.0)) = 3
        [Header(Daytime Color)]
        _DaySkyColor ("Daytime Sky Color", Color) = (.25, .6, 1, 1)
        _DayHorizonColor ("Daytime Horizon Color", Color) = (1, 1, 1, 1)
        _DayGroundColor ("Daytime Ground Color", Color) = (.369, .349, .341, 1)
        _DayCloudColor ("Daytime Cloud Color (Outer)", Color) = (1,1,1, 1)
        _DayCloudColor2 ("Daytime Cloud Color (Inner)", Color) = (0.8,0.8,0.8, 1)
        [Header(Sunset Color)]
        _EveSkyColor ("Sunset Sky Color", Color) = (.1, .6, 25, 1)
        _EveHorizonColor ("Sunset Horizon Color", Color) = (1, 1, 1, 1)
        _EveGroundColor ("Sunset Ground Color", Color) = (.369, .349, .341, 1)
        _EveCloudColor ("Sunset Cloud Color (Outer)", Color) = (1, 0.7, 0.8, 1)
        _EveCloudColor2 ("Sunset Cloud Color (Inner)", Color) = (.1, .6, 25, 1)
        [Header(Midnight Color)]
        _NightSkyColor ("Midnight Sky Color", Color) = (.0, .0, .0, 1)
        _NightHorizonColor ("Midnight Horizon Color", Color) = (0, 0, 0.2, 1)
        _NightGroundColor ("Midnight Ground", Color) = (.0, .0, .0, 1)
        _NightCloudColor ("Midnight Cloud Color (Outer)", Color) = (0.2,0.2,0.2, 1)
        _NightCloudColor2 ("Midnight Cloud Color (Inner)", Color) = (0,0,0, 1)

        [Header(Starry Config)]
        _StarryThreshold ("Starry Threshold", Color) = (0.95,0.95,0.93, 1)
        _StarryFineness ("Fineness", Range(0, 1000.0)) = 200
        _StarryAngleFrom ("Border Angle Min", Range(-1.0, 1.0)) = 0
        _StarryAngleTo ("Border Angle Max", Range(-1.0, 1.0)) = 0.2
        _StarrySunAngleFrom ("Minimum Luminance Sun Angle", Range(-1.0, 1.0)) = 0.3
        _StarrySunAngleTo ("Maximum Luminance Sun Angle", Range(-1.0, 1.0)) = 0

        [Header(Cloud Config)]
        _CloudScale ("Scale", Float) = 100
        _CloudDensity ("Density", Range(0.0, 1.0)) = 0.4
        _CloudSoftness ("Softness", Range (0.0001, 0.9999)) = 0.15
        _GrainSize ("GrainSize", Range(0.0, 1.0)) = 0.4
        [Header(Cloud Sun Blend)]
        [Toggle] _scattering ("Sun affects clouds", Float) = 1
		_scatteringForce ("Effectiveness", Range (0, 3)) = 1
		_scatteringRange ("Effective Range", Range (0, 1)) = 0.1
		_scatteringNarrow ("Fineness", Float) = 1
        [Header(Cloud Moving)]
        _CloudTransform ("Transform", Float) = 0.1
        _CloudDirection ("Move Direction", Range(0.0, 360.0)) = 0
		_CloudSpeed ("Speed", Range(-100.0, 100.0)) = 0.1
        _SpeedError("Speed Difference", Float) = 0.2

        [HideInInspector][Header(Cloud Border)]
        [HideInInspector]_CloudThreshold ("Threshold", Range (-1, 1)) = -0.4
        [HideInInspector]_CloudFade ("Fade", Range (0.0001, 0.9999)) = 0.25
    }

    SubShader {
        Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
        Cull Off ZWrite Off

        Pass {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "SimplexNoise3D.hlsl" 
			#include "Quaternion.hlsl"
			#include "Easing.hlsl"

            half _Exposure;
            half _SunSize;
            half _SunExpozure;

            //////////////////////////////////// for sky
            half _GroundAngle;
            half _HorizonAngle;
            half _SkyAngle;
            half3 _DayGroundColor;
            half4 _DayHorizonColor;
            half3 _DaySkyColor;
            half3 _DayCloudColor;
            half3 _DayCloudColor2;
            half3 _EveGroundColor;
            half4 _EveHorizonColor;
            half3 _EveSkyColor;
            half3 _EveCloudColor;
            half3 _EveCloudColor2;
            half3 _NightGroundColor;
            half4 _NightHorizonColor;
            half3 _NightSkyColor;
            half3 _NightCloudColor;
            half3 _NightCloudColor2;
            float _TimeWithColor;
            half _TransisionCoefficient;

            static const half SUN_ALTITUDE = _TimeWithColor?clamp(clamp(_WorldSpaceLightPos0.y*_TransisionCoefficient,-1,1)-saturate(1-_LightColor0.rgb),-1,1):clamp(_WorldSpaceLightPos0.y*_TransisionCoefficient,-1,1);
            static const half3 GROUND_COLOR = SUN_ALTITUDE>0?lerp(_EveGroundColor,_DayGroundColor,SUN_ALTITUDE):lerp(_EveGroundColor,_NightGroundColor,-SUN_ALTITUDE) * _Exposure;
            static const half3 SKY_COLOR = SUN_ALTITUDE>0?lerp(_EveSkyColor,_DaySkyColor,SUN_ALTITUDE):lerp(_EveSkyColor,_NightSkyColor,-SUN_ALTITUDE) * _Exposure;
            static const half3 HORIZON_COLOR = SUN_ALTITUDE>0?lerp(_EveHorizonColor,_DayHorizonColor,SUN_ALTITUDE):lerp(_EveHorizonColor,_NightHorizonColor,-SUN_ALTITUDE) * _Exposure;
            static const half3 CLOUD_COLOR1 = SUN_ALTITUDE>0?lerp(_EveCloudColor,_DayCloudColor,SUN_ALTITUDE):lerp(_EveCloudColor,_NightCloudColor,-SUN_ALTITUDE) * _Exposure;
            static const half3 CLOUD_COLOR2 = SUN_ALTITUDE>0?lerp(_EveCloudColor2,_DayCloudColor2,SUN_ALTITUDE):lerp(_EveCloudColor2,_NightCloudColor2,-SUN_ALTITUDE) * _Exposure;
            static const half3 SUN_COLOR = _LightColor0.xyz / clamp(length(_LightColor0.xyz), 0.25, 1) ;//* _Exposure?

            //////////////////////////////////// for starry
            half3 _StarryThreshold;
            half _StarryFadeFrom;
            half _StarryFadeTo;
            half _StarryFineness;
            half _StarrySunAngleFrom;
            half _StarrySunAngleTo;

            static const half STARRY_INTENSITY = _StarrySunAngleFrom<=_StarrySunAngleTo?1:smoothstep(_StarrySunAngleFrom,_StarrySunAngleTo,SUN_ALTITUDE);

            //////////////////////////////////// for cloud
            fixed4 _CloudColor;
            fixed _CloudScale;
			fixed _CloudDensity;
            fixed _CloudSoftness;
            fixed _GrainSize;
            fixed _CloudTransform;
            fixed _CloudDirection;
            fixed _CloudSpeed;
            fixed _SpeedError;
            fixed _CloudThreshold;
            fixed _CloudFade;

            float _scattering;
            float _scatteringForce;
			float _scatteringPassRate;
			float _scatteringRange;
			float _scatteringNarrow;

            static const fixed _SpeedSlide = _SpeedError / 2;
            #define RIM_POWER 1.0;
            #define RIM_NARROW 2.5;

            #define planetRadius 6000000.0
			#define cloudHeight 20000.0
			#define adjustRate 15000.0
		    #define adjustOffset 1.0
			#define adjustMax 5
			
            struct appdata_t
            {
                float4 vertex   : POSITION;
                fixed2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4  pos             : SV_POSITION;
                half3   rayDir          : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

		    /////////////////////////////////////////////////// functions
            #define NOISE_LOOPING 6

			inline float remap(float value, float minOld, float maxOld, float minNew, float maxNew)
			{
				float rangeOld = (maxOld - minOld);
				return rangeOld==0 ? minNew : minNew + (value - minOld) * (maxNew - minNew) / rangeOld;
			}

            half calcSunAttenuation(half3 lightPos, half3 ray)
            {
                half3 delta = lightPos - ray;
                half dist = length(delta);
                half spot = 1.0 - smoothstep(_SunSize/1.5, _SunSize, dist);
                return spot*spot;
            }

            fixed2 random2(fixed2 st){
                st = fixed2( dot(st,fixed2(127.1,311.7)),dot(st,fixed2(269.5,183.3)) );
                return -1.0 + 2.0*frac(sin(st)*43758.5453123);
            }

            float perlinNoise(fixed2 st) 
            {
                fixed2 p = floor(st);
                fixed2 f = frac(st);
                fixed2 u = f*f*(3.0-2.0*f);

                float v00 = random2(p+fixed2(0,0));
                float v10 = random2(p+fixed2(1,0));
                float v01 = random2(p+fixed2(0,1));
                float v11 = random2(p+fixed2(1,1));

                return lerp( lerp( dot( v00, f - fixed2(0,0) ), dot( v10, f - fixed2(1,0) ), u.x ),
                             lerp( dot( v01, f - fixed2(0,1) ), dot( v11, f - fixed2(1,1) ), u.x ), 
                             u.y)+0.5f;
            }

            inline float3 createReViewDir (float3 worldPos)
			{
				float3 worldViewDir = UnityWorldSpaceViewDir(worldPos.xyz);
				worldViewDir = normalize(worldViewDir);
				return worldViewDir;
			}

            float4 createFbm (float3 coord, float totalScale, float3 totalOffset, float3 fbmOffset, float3 speed, float fbmAdjust)
			{
				float3 offset;
				float swingRate = 2;
				float swing = 1;
				float nowSwing;
				float smallness = 1;
				float totalSwing = 0;
				float smallnessRate = _GrainSize;
				float totalSmallness = 1 / totalScale * pow(smallnessRate, -NOISE_LOOPING);
				if (totalScale == 0)
				{
					totalSmallness = 0;
				}
				float4 noise = float4(0, 0, 0, 0);
				float4 nowNoise;
				float baseOffset = totalScale * 10;
				coord.xz += totalOffset;
				fbmOffset /= NOISE_LOOPING;
				float adjustBase = floor(fbmAdjust);
				float adjustSmallRate = frac(fbmAdjust);
				float adjustLargeRate = 1 - adjustSmallRate;
				smallness = pow(smallnessRate, adjustBase);
				swing /= swingRate;
				smallness /= smallnessRate;
				for (int loopIndex = 0; loopIndex < NOISE_LOOPING; loopIndex++)
				{
					float adjustIndex = adjustBase + loopIndex;
					swing *= swingRate;
					nowSwing = swing;
					if (loopIndex == 0)
					{
						nowSwing *= adjustLargeRate;
					}
					if (loopIndex == NOISE_LOOPING - 1)
					{
						nowSwing *= adjustSmallRate;
					}
					smallness *= smallnessRate;
					offset = -fbmOffset * length(speed.xz) * (adjustIndex - NOISE_LOOPING) + speed + baseOffset;
					nowNoise = snoise3d_grad((coord + offset) * smallness * totalSmallness);
					nowNoise.y = quadInOut(nowNoise.w);
					noise += nowNoise * nowSwing;
					totalSwing += nowSwing;
				}
				noise /= totalSwing;
				// 
				return noise;
			}

            //////////////////////////////////////////// vert

            v2f vert (appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.pos = UnityObjectToClipPos(v.vertex);

                float3 eyeRay = normalize(mul((float3x3)unity_ObjectToWorld, v.vertex.xyz));
                OUT.rayDir          = half3(-eyeRay);

                return OUT;
            }

            //////////////////////////////////////////// frag

            half4 frag (v2f IN) : SV_Target
            {
                half3 col = half3(0.0, 0.0, 0.0);
                half3 ray = IN.rayDir.xyz;
                half3 rRay = -ray;
                half rRayY = rRay.y;
                ///////////////SkyColor and Sun
                col = GROUND_COLOR;
                col= lerp(col,HORIZON_COLOR,smoothstep(_GroundAngle,_HorizonAngle,rRayY));
                col= lerp(col,SKY_COLOR,smoothstep(_HorizonAngle,_SkyAngle,rRayY));
                col += SUN_COLOR * calcSunAttenuation(_WorldSpaceLightPos0.xyz,rRay)*_SunExpozure;

                ///////////////Starry
                half3 starry = half3(0.0, 0.0, 0.0);
                half perlin = snoise3d(ray*_StarryFineness);
                starry.r = smoothstep(_StarryThreshold.r,1,perlin);
                starry.g = smoothstep(_StarryThreshold.g,1,perlin);
                starry.b = smoothstep(_StarryThreshold.b,1,perlin);
                starry.rgb *= STARRY_INTENSITY;
                starry.rgb *= _Exposure * smoothstep(_StarryFadeFrom,_StarryFadeTo,rRayY);
                col = max(col,starry);
                
                ///////////////Clouds
                half4 cloud = 1;
                cloud.rgb = CLOUD_COLOR2;
				float totalR = cloudHeight + planetRadius;
				float viewDistance = sqrt(totalR * totalR - (1 - rRayY * rRayY) * (planetRadius * planetRadius)) - rRayY * planetRadius;
				float3 ovalCoord = rRay * viewDistance;
				float adjustBase = pow((length(ovalCoord)) / adjustRate, 0.6);
				adjustBase = clamp(adjustBase - adjustOffset, 0, adjustMax);
				float4 moveQuaternion =  rotate_angle_axis(_CloudDirection * PI / 180, float3(0, 1, 0));
				float3 fbmOffset = float3(_SpeedError, 0, _SpeedSlide);
				float3 speed = _Time.y * float3(_CloudSpeed, _CloudTransform, 0) * 1000;
				speed = rotate_vector(speed, moveQuaternion);
				fbmOffset = rotate_vector(fbmOffset, moveQuaternion);
				float4 noise = createFbm(ovalCoord, _CloudScale * 1000, 1000, fbmOffset, speed, adjustBase);
				float cloudNoisePower = clamp(noise.w,-1, 1) * 0.5 + 0.5;
				cloudNoisePower *= clamp(remap(asin(clamp(rRay.y, -1, 1)), _CloudThreshold, (1 - _CloudThreshold) * _CloudFade + _CloudThreshold, 0, 1), 0, 1);
				float soft = tan((1 - _CloudSoftness) * PI / 2);
				float3 topVector = float3(0, 1, 0);
				float softDecay = clamp(dot(rRay, topVector), 0, 1);
				softDecay = remap(softDecay, 0, 1, 0, 1);
				softDecay = 1 - clamp(softDecay, 0, 1);
				soft /= softDecay;
				float cloudPower = soft * cloudNoisePower + 0.5 - (1 - _CloudDensity) * soft;
				cloudPower = cubicInOut(clamp(cloudPower, 0, 1));
				float3 cloudWorldNormal = normalize(-noise.xyz);
				float cloudAreaRate = (cloudNoisePower + _CloudDensity - 1) / _CloudDensity;
				if (_CloudDensity == 0){
					cloudAreaRate = 0;
				}
				float3 underVector = float3(0, -1, 0);
				float4 viewQuaternion = from_to_rotation(underVector, ray);
				cloudWorldNormal = rotate_vector(cloudWorldNormal, viewQuaternion);
				float3 lightVector = _WorldSpaceLightPos0.xyz;
				float3 skyVector = float3(1, 0, 0);
				float4 quaternionY =  rotate_angle_axis(0, float3(0, 1, 0));
				float4 quaternionZ =  rotate_angle_axis(0, float3(0, 0, 1));
				skyVector = rotate_vector(skyVector, quaternionY);
				float normalDot = dot(cloudWorldNormal, lightVector);
				float normalDotForUv = normalDot * 0.5 + 0.5;
				float viewDotToLightForUv = dot(ray, lightVector) * 0.5 + 0.5;
				float viewDotToSkyForUv = dot(ray, skyVector) * 0.5 + 0.5;
				float2 cloudUv = float2(viewDotToLightForUv, normalDotForUv);
			    //cloud outside
				float rimPowerR = cloudAreaRate * RIM_NARROW;
				rimPowerR = quadOut(clamp(rimPowerR, 0, 1));
				float rimPower = (1 - rimPowerR) * RIM_POWER;
				rimPower = clamp(rimPower, 0, 1);
				float2 rimUv = float2(viewDotToLightForUv, normalDotForUv);
				float3 rimColor = CLOUD_COLOR1;
                cloud.rgb = lerp(cloud.rgb,rimColor,rimPower);
				float scatteringPower = 0;
				if (_scattering){
					float scatteringPowerR = cloudAreaRate * _scatteringNarrow;
					scatteringPowerR = quadOut(clamp(scatteringPowerR, 0, 1));
					float scatteringPower = (1 - scatteringPowerR) * _scatteringForce;
					float scatteringPowerRateRaw = clamp((_scatteringRange - viewDotToLightForUv) / _scatteringRange, 0, 1);
					if (_scatteringRange == 0)
					{
						scatteringPowerRateRaw = 0;
					}
					scatteringPower *= quadIn(scatteringPowerRateRaw);
					scatteringPower = clamp(scatteringPower, 0, 1);
					cloud.rgb += scatteringPower * _LightColor0.xyz * _scatteringForce;
				}
				cloud.a *= cloudPower;
                cloud.rgb = lerp(col,cloud.rgb,cloud.a);

                col=lerp(col,cloud.rgb,cloud.a);
                col.rgb *= _Exposure;
                ///////////////
                return half4(col,1.0);
            }
            ENDCG
        }
    }
}