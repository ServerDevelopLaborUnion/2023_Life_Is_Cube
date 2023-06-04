public struct CubeRotateInfo
{
    public DirectionFlags axis;
    public bool clockWise;

    public CubeRotateInfo(DirectionFlags axis, bool clockWise)
    {
        this.axis = axis;
        this.clockWise = clockWise;
    }
}