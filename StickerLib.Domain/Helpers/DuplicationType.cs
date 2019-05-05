using System.ComponentModel;

namespace StickerLib.Domain.Helpers
{
    public enum DuplicationType
    {
        [Description("None")]
        None,
        [Description("Each page")]
        EachPage,
        [Description("Group page")]
        GroupPage
    }
}