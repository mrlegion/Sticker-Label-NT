using StickerLib.Infrastructure.Helpers;

namespace StickerLib.Infrastructure.Entities
{
    public class PrintPage
    {
        /// <summary>
        /// Gets or sets uniqual identifier on database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Name for print page
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets value for Width on print page object
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Gets or sets value for Height on print page object
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Gets or sets value for type of size metrics for this print page (example, mm on metrics)
        /// </summary>
        public SizeType SizeType { get; set; }

        public PrintPage(string name, float width, float height, SizeType sizeType)
        {
            Name = name;
            Width = width;
            Height = height;
            SizeType = sizeType;
        }

        public PrintPage(SizeType sizeType)
        {
            SizeType = sizeType;
        }

        /// <summary>
        /// Rotation Print Page to 90 deg
        /// </summary>
        /// <returns>New object PrintPage class</returns>
        public PrintPage Rotation()
        {
            return new PrintPage(this.Name, this.Height, this.Width, this.SizeType);
        }

        /// <summary>
        /// Gets orientation print page
        /// </summary>
        /// <returns>Orientation for this print page</returns>
        public OrientationType GetOrientation()
        {
            return Width > Height
                ? OrientationType.Horizontal
                : Width < Height
                    ? OrientationType.Vertical
                    : OrientationType.None;
        }
    }
}