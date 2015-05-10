﻿using System;

namespace FilmsInventory.Utils
{
    public class DefaultTimeProvider : TimeProvider
    {
        private readonly static DefaultTimeProvider instance = new DefaultTimeProvider();

        private DefaultTimeProvider() { }

        public override DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }

        public static DefaultTimeProvider Instance
        {
            get { return DefaultTimeProvider.instance; }
        }
    }
}
