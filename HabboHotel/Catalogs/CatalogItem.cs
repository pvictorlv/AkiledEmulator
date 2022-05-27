﻿using Akiled.HabboHotel.Items;
using System.Text;

namespace Akiled.HabboHotel.Catalog
{
    public class CatalogItem
    {
        public int Id;
        public int ItemId;
        public ItemData Data;
        public int Amount;
        public int CostCredits;
        public bool HaveOffer;
        public bool IsLimited;
        public string Name;
        public int PageID;
        public int CostDuckets;
        public int LimitedEditionStack;
        public int LimitedEditionSells;
        public int CostDiamonds;
        public string Badge;

        public CatalogItem(int Id, int ItemId, ItemData Data, string CatalogName, int PageId, int CostCredits, int CostPixels,
            int CostDiamonds, int Amount, int LimitedEditionSells, int LimitedEditionStack, bool HaveOffer, string Badge)
        {
            this.Id = Id;
            this.Name = Encoding.GetEncoding("Windows-1252").GetString(Encoding.GetEncoding("UTF-8").GetBytes(CatalogName));
            this.ItemId = ItemId;
            this.Data = Data;
            this.PageID = PageId;
            this.CostCredits = CostCredits;
            this.CostDuckets = CostPixels;
            this.CostDiamonds = CostDiamonds;
            this.Amount = Amount;
            this.LimitedEditionSells = LimitedEditionSells;
            this.LimitedEditionStack = LimitedEditionStack;
            this.IsLimited = (LimitedEditionStack > 0);
            this.HaveOffer = HaveOffer;
            this.Badge = Badge;
        }
    }
}