namespace HexGridSystem
{
    public interface IHexTile
    {
        public void SetGridPosition(int x, int y);
        public void Select();
        public void Deselect();
    }
}
