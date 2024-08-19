
using System;

public static class GameTag
{
    public enum Tag
    {
        Null,

        Player,
        Player_1,
        Player_2,
        Player_3,

        Brick_1,   
        Brick_2,  
        Brick_3,
        Brick_4,
    }

    public static string ToString(Tag tag)
    {
        return tag switch
        {
            Tag.Null => "Null",

            Tag.Player => "Player",
            Tag.Player_1 => "Player_1",
            Tag.Player_2 => "Player_2",
            Tag.Player_3 => "Player_3",

            Tag.Brick_1 => "Brick_1",      
            Tag.Brick_2 => "Brick_2",      
            Tag.Brick_3 => "Brick_3",
            Tag.Brick_4 => "Brick_4",
            _ => throw new ArgumentOutOfRangeException(nameof(tag), tag, null),
        };
    }
}
