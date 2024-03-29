﻿using StickerLib.Infrastructure.Entities;
using StickerLib.Infrastructure.Helpers;

namespace StickerLib.Infrastructure.Common
{
    public class Properties
    {
        private static readonly Properties Instance = new Properties();
        private bool _autoGenerateColsAndRows;
        private bool _autoShufflePattern;
        private PrintPage _stickerPageSize;
        private PrintPage _printPageSize;
        private OrientationType _orientation;
        private int _column;
        private int _row;

        private Properties() {}

        public FileExistRuleType FileExistRule { get; set; }

        public string DirectoryForSaving { get; set; }

        public bool AutoShufflePattern
        {
            get { return _autoShufflePattern; }
            set
            {
                _autoShufflePattern = value;
                if (_autoShufflePattern)
                    GenerateShufflePattern();
            }
        }

        public string ShufflePattern { get; set; }

        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                Groups = Column * Row;
            }
        }

        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                Groups = Column * Row;
            }
        }

        public bool AutoGenerateColsAndRows
        {
            get { return _autoGenerateColsAndRows; }
            set
            {
                _autoGenerateColsAndRows = value;
                if (value)
                    GenerateColsAndRows();
            }
        }

        public int Groups { get; private set; }

        public PrintPage StickerPageSize
        {
            get { return _stickerPageSize; }
            set
            {
                _stickerPageSize = value;
                if (AutoGenerateColsAndRows)
                    GenerateColsAndRows();
            }
        }

        public PrintPage PrintPageSize
        {
            get { return _printPageSize; }
            set
            {
                _printPageSize = value;
                if (AutoGenerateColsAndRows)
                    GenerateColsAndRows();
            }
        }

        public AnchorType Anchor { get; set; }

        public OrientationType Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                if (AutoGenerateColsAndRows)
                    GenerateColsAndRows();
            }
        }

        public static Properties GetInstance() { return Instance; }

        private void GenerateColsAndRows()
        {
            if (StickerPageSize == null) return;
            if (PrintPageSize == null) return;

            // check orientation for size
            if (!Orientation.Equals(PrintPageSize.GetOrientation()))
                PrintPageSize = PrintPageSize.Rotation();

            if (StickerPageSize.Width < PrintPageSize.Height)
                if (StickerPageSize.GetOrientation() == Helpers.OrientationType.Horizontal)
                    PrintPageSize = PrintPageSize.Rotation();

            // calculated columns
            if (PrintPageSize.Width <= StickerPageSize.Width) Column = 1;
            else Column = (int) (PrintPageSize.Width / StickerPageSize.Width);

            // calculated rows
            if (PrintPageSize.Height <= StickerPageSize.Height) Row = 1;
            else Row = (int) (PrintPageSize.Height / StickerPageSize.Height);
        }

        private void GenerateShufflePattern()
        {
            ShufflePattern = "1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16";
            // TODO: create module cut and stack and use it
        }
    }
}