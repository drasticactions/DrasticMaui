// <copyright file="DrasticMenuCollectionView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using CoreGraphics;
using DrasticMaui.Models;
using Foundation;
using UIKit;

namespace DrasticMaui.iOS
{
    public class DrasticMenuCollectionView : UIViewController
    {
        private UICollectionView collectionView;
        private UICollectionViewCellRegistration? headerRegistration;
        private UICollectionViewCellRegistration? rowRegistration;
        private UICollectionViewCellRegistration? expandedRowRegistration;
        private UICollectionViewDiffableDataSource<NativeSidebarMenuItemType, DrasticMenuItem> dataSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticMenuCollectionView"/> class.
        /// </summary>
        public DrasticMenuCollectionView()
        {
            if (this.View is null)
            {
                throw new NullReferenceException(nameof(this.View));
            }

            this.collectionView = new UICollectionView(this.View.Frame, this.CreateLayout())
            {
                AutoresizingMask = UIViewAutoresizing.All,
            };

            this.dataSource = new UICollectionViewDiffableDataSource<NativeSidebarMenuItemType, DrasticMenuItem>(this.collectionView, (collectionView, indexPath, item) => {
                if (item is DrasticMenuItem menuItem)
                {
                    switch (menuItem.Type)
                    {
                        case SidebarMenuItemType.Header:
                            return this.collectionView.DequeueConfiguredReusableCell(this.headerRegistration, indexPath, item);
                        case SidebarMenuItemType.Row:
                            return this.collectionView.DequeueConfiguredReusableCell(this.rowRegistration, indexPath, item);
                        case SidebarMenuItemType.ExpandableRow:
                            return this.collectionView.DequeueConfiguredReusableCell(this.expandedRowRegistration, indexPath, item);
                    }
                }

                return new UICollectionViewCell();
            });

            this.View.AddSubview(this.collectionView);

            this.headerRegistration = UICollectionViewCellRegistration.GetRegistration(typeof(UICollectionViewListCell), (cell, indexPath, item) =>
            {
                if (item is not DrasticMenuItem nativeItem)
                {
                    return;
                }

                var contentConfigruation = UIListContentConfiguration.SidebarHeaderConfiguration;
                contentConfigruation.Text = nativeItem.Title;
                contentConfigruation.TextProperties.Font = UIFont.GetPreferredFontForTextStyle(UIFontTextStyle.Subheadline);
                contentConfigruation.TextProperties.Color = UIColor.SecondaryLabelColor;
                cell.ContentConfiguration = contentConfigruation;
            });

            this.rowRegistration = UICollectionViewCellRegistration.GetRegistration(typeof(UICollectionViewListCell), (cell, indexPath, item) =>
            {
                if (item is not DrasticMenuItem nativeItem)
                {
                    return;
                }

                var contentConfigruation = UIListContentConfiguration.SidebarSubtitleCellConfiguration;
                contentConfigruation.Text = nativeItem.Title;
                cell.ContentConfiguration = contentConfigruation;
            });

            this.expandedRowRegistration = UICollectionViewCellRegistration.GetRegistration(typeof(UICollectionViewListCell), (cell, indexPath, item) =>
            {
                if (item is not DrasticMenuItem nativeItem)
                {
                    return;
                }

                var contentConfigruation = UIListContentConfiguration.SidebarSubtitleCellConfiguration;
                contentConfigruation.Text = nativeItem.Title;
                cell.ContentConfiguration = contentConfigruation;
            });

            this.collectionView.DataSource = this.dataSource;
            this.AddItems(new List<DrasticMenuItem>() { new DrasticMenuItem("Test"), new DrasticMenuItem("Test2") }, new NativeSidebarMenuItemType());
        }

        public void AddItems(List<DrasticMenuItem> items, NativeSidebarMenuItemType type)
        {
            var snapshot = new NSDiffableDataSourceSectionSnapshot<DrasticMenuItem>();
            snapshot.AppendItems(items.ToArray());
            this.dataSource.ApplySnapshot(snapshot, type, false);
        }

        private UICollectionViewCompositionalLayout CreateLayout()
        {
            var config = new UICollectionLayoutListConfiguration(UICollectionLayoutListAppearance.Sidebar);
            return UICollectionViewCompositionalLayout.GetLayout(config);
        }
    }

    public class NativeSidebarMenuItemType : NSEnumerator
    {
    }
}