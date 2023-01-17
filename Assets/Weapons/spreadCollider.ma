//Maya ASCII 2022 scene
//Name: spreadCollider.ma
//Last modified: Tue, Jan 17, 2023 02:21:15 PM
//Codeset: 1252
requires maya "2022";
requires "mtoa" "5.0.0.4";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2022";
fileInfo "version" "2022";
fileInfo "cutIdentifier" "202205171752-c25c06f306";
fileInfo "osv" "Windows 10 Pro v2009 (Build: 19044)";
fileInfo "UUID" "14F151F0-4831-3A65-4C8D-D3977C0B83AF";
fileInfo "license" "education";
createNode transform -n "shotgunCollider";
	rename -uid "25375D51-4465-FE29-15DF-BBBE31973518";
	setAttr ".t" -type "double3" 0 0 -1 ;
	setAttr ".r" -type "double3" 90 0 0 ;
	setAttr ".rp" -type "double3" 0 1 0 ;
	setAttr ".rpt" -type "double3" 0 -1 1 ;
	setAttr ".sp" -type "double3" 0 1 0 ;
createNode mesh -n "shotgunColliderShape" -p "shotgunCollider";
	rename -uid "BF49FBDA-4F91-6B49-70A0-FA9A9CC779F7";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.21684190630912781 0.31314486265182495 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode polyTweakUV -n "polyTweakUV1";
	rename -uid "7139A0DF-4596-0A59-19FC-A6B20F49AE9B";
	setAttr ".uopa" yes;
	setAttr -s 48 ".uvtk[0:47]" -type "float2" 0.41451728 0.0071037156 0.4300794
		 0.014292214 0.45935342 0.12296373 0.45766288 0.029259894 0.40605387 0.0080342926
		 0.39831597 0.017281685 0.38491789 0.034903023 0.36088565 0.060647789 -0.48037049
		 0.022333149 -0.46579519 0.0092614926 -0.45118123 0.12169921 -0.45751923 0.0044823331
		 -0.50619847 0.043406341 -0.44915706 0.0080512203 -0.43432444 0.019882295 -0.4080742
		 0.039673898 -0.15385354 -0.080610424 -0.14645982 -0.082046449 -0.13906613 -0.080610394
		 -0.13239616 -0.076442748 -0.12710285 -0.069951713 -0.12370431 -0.061772287 -0.12253332
		 -0.052705407 -0.12370431 -0.043638587 -0.12710285 -0.035459161 -0.13239616 -0.028967977
		 -0.1390661 -0.02480042 -0.14645976 -0.023364365 -0.15385351 -0.02480042 -0.16052347
		 -0.028968036 -0.16581678 -0.035459101 -0.16921532 -0.043638468 -0.17038634 -0.052705348
		 -0.16921532 -0.061772287 -0.16581681 -0.069951713 -0.1605235 -0.076442897 0.36729747
		 0.19286513 0.35477769 0.20424533 0.32019982 0.10781337 0.34536275 0.21007729 0.33570898
		 0.2102288 0.32247427 0.20460618 0.0057449937 -0.42845696 0.018264711 -0.43983713
		 0.052842677 -0.34340525 0.027679622 -0.44566911 0.037333488 -0.44582066 0.050568283
		 -0.440198;
createNode polyAutoProj -n "polyAutoProj1";
	rename -uid "56CB0C23-4949-E2EB-1C27-DA82ECAB64D5";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:20]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 0 1 0 0 -1 0 0 0 0 -1 1;
	setAttr ".s" -type "double3" 2 2 2 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyTweak -n "polyTweak1";
	rename -uid "694CB15C-40E0-9CCC-3FB1-CBB19DE076C8";
	setAttr ".uopa" yes;
	setAttr -s 21 ".tk[0:20]" -type "float3"  -0.18664937 0 0.10647994 -0.15877347
		 0 0.20253694 -0.11535565 0 0.27876827 -0.060646042 0 0.32771158 0 0 0.34457651 0.060646042
		 0 0.32771152 0.11535566 0 0.27876809 0.15877341 0 0.20253684 0.18664931 0 0.10647991
		 0.19625464 0 0 0.18664931 0 -0.10647991 0.15877339 0 -0.20253682 0.11535558 0 -0.27876803
		 0.060646035 0 -0.32771122 5.8488436e-09 0 -0.34457645 -0.06064602 0 -0.32771116 -0.11535557
		 0 -0.278768 -0.1587733 0 -0.20253682 -0.18664928 0 -0.10647989 -0.19625463 0 0 0
		 0 0;
createNode polyCone -n "polyCone1";
	rename -uid "D6A23E29-4316-F7F4-B40A-6283BCA90E07";
	setAttr ".cuv" 3;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 2 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 5 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :initialShadingGroup;
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultRenderGlobals;
	addAttr -ci true -h true -sn "dss" -ln "defaultSurfaceShader" -dt "string";
	setAttr ".ren" -type "string" "arnold";
	setAttr ".dss" -type "string" "lambert1";
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :defaultColorMgtGlobals;
	setAttr ".cfe" yes;
	setAttr ".cfp" -type "string" "<MAYA_RESOURCES>/OCIO-configs/Maya2022-default/config.ocio";
	setAttr ".vtn" -type "string" "ACES 1.0 SDR-video (sRGB)";
	setAttr ".vn" -type "string" "ACES 1.0 SDR-video";
	setAttr ".dn" -type "string" "sRGB";
	setAttr ".wsn" -type "string" "ACEScg";
	setAttr ".otn" -type "string" "ACES 1.0 SDR-video (sRGB)";
	setAttr ".potn" -type "string" "ACES 1.0 SDR-video (sRGB)";
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
connectAttr "polyTweakUV1.out" "shotgunColliderShape.i";
connectAttr "polyTweakUV1.uvtk[0]" "shotgunColliderShape.uvst[0].uvtw";
connectAttr "polyAutoProj1.out" "polyTweakUV1.ip";
connectAttr "polyTweak1.out" "polyAutoProj1.ip";
connectAttr "shotgunColliderShape.wm" "polyAutoProj1.mp";
connectAttr "polyCone1.out" "polyTweak1.ip";
connectAttr "shotgunColliderShape.iog" ":initialShadingGroup.dsm" -na;
// End of spreadCollider.ma
