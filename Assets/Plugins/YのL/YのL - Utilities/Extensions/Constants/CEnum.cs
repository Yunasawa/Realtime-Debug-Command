namespace YNL.Extension.Constant
{
    public enum EButtonTransition
    {
        None = 0, 
        Color = 1, 
        SpriteSwap = 1 << 2, 
        Animation = 1 << 3,
        ColorAndTransform = 1 << 4
    }
}