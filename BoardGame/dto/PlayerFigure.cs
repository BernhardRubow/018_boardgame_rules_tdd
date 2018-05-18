using newvisionsproject.boardgame.enums;

namespace newvisionsproject.boardgame.dto
{
    public class PlayerFigure
    {

        // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public int Index { get; private set; }
        public PlayerColors Color { get; private set; }
        public int WorldOffset { get; private set; }
        public int LocalPosition { get; set; }
        public int WorldPosition { get; set; }


        // +++ constructor ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public PlayerFigure(PlayerColors playerColor, int index = 0, int offSet = 0)
        {
            Index = index;
            Color = playerColor;
            LocalPosition = -1;
            WorldPosition = -1;
            WorldOffset = offSet;
        }

        public PlayerFigure(PlayerColors playerColor, int index, int localPosition, int worldPosition, int offset = 0)
        {
            Color = playerColor;
            Index = index;
            LocalPosition = localPosition;
            WorldPosition = worldPosition;
            WorldOffset = offset;
        }

        public void Move(int diceValue)
        {
            if (LocalPosition == -1)
            {
                LocalPosition = 0;
                WorldPosition = WorldOffset;
            }
            else
            {
                LocalPosition += diceValue;
                WorldPosition += diceValue;

                // the track on the gameboard is endless with 40 fields.
                WorldPosition %= 41;
            }
        }
    }
}