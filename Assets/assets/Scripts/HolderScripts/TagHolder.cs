using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
}

public class MouseAxis
{
    public const string mouseX = "Mouse X";
    public const string mouseY = "Mouse Y";
}

public class AnimationTag
{
    public const string ZoomInAnim = "ZoomIn";
    public const string ZoomOutAnim = "ZoomOut";

    public const string ShootTrigger = "Shoot";
    public const string AimParameter = "Aim";

    public const string WalkParameter = "Walk";
    public const string RunParameter = "Run";
    public const string AttackTrigger = "Attack";
    public const string DeadTrigger = "Dead";
}

public class Tags
{
    public const string LookRoot = "LookRoot";
    public const string ZoomCamera = "FPCamera";
    public const string Crosshair = "Crosshair";
    public const string ArrowTag = "Arrow";

    public const string AxeTag = "Axe";

    public const string PlayerTag = "Player";
    public const string EnemyTag = "Enemy";
}