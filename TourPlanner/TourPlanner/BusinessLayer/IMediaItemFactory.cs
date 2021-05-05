﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface IMediaItemFactory
    {
        IEnumerable<MediaItem> GetItems();
        IEnumerable<MediaItem> Search(string itemName, bool caseSensitive = false);
    }
}
