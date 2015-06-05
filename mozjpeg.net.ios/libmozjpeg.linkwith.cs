using System;
using ObjCRuntime;

[assembly: LinkWith ("libmozjpeg.a", LinkTarget.Simulator | LinkTarget.ArmV7s | LinkTarget.ArmV7 | LinkTarget.Simulator64 | LinkTarget.Arm64, SmartLink = true, ForceLoad = true)]