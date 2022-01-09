// <copyright file="SidebarViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Events;
using DrasticMaui.Models;
using Foundation;
using UIKit;

namespace DrasticMaui.iOS
{
    public class SidebarViewController : UIViewController, IUICollectionViewDelegate
    {
        private UICollectionView? collectionView;
        private UICollectionViewDiffableDataSource<NSString, NavigationSidebarItem>? dataSource;
        private UISearchController? searchController;
        private SidebarMenuOptions options;
        private MacSidebarMenuOptions macOptions;

        public SidebarViewController(SidebarMenuOptions options, MacSidebarMenuOptions macOptions)
        {
            this.options = options;
            this.macOptions = macOptions;
        }

        public event EventHandler? OnSettingsSelect;

        public event EventHandler<DrasticSidebarMenuItemSelectedEventArgs>? OnMenuItemSelect;

        /// <summary>
        /// Item Selected.
        /// </summary>
        /// <param name="collectionView">Collection View.</param>
        /// <param name="indexPath">Index Path.</param>
        [Export("collectionView:didSelectItemAtIndexPath:")]
        protected void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var sidebarItem = this.dataSource?.GetItemIdentifier(indexPath);
            if (sidebarItem is not null)
            {
                this.OnMenuItemSelect?.Invoke(this, new DrasticSidebarMenuItemSelectedEventArgs(sidebarItem));
            }
        }

        /// <inheritdoc/>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = this.options.Header;

            this.NavigationItem.LargeTitleDisplayMode = this.macOptions.LargeTitleDisplayMode;

            if (this.options.ShowSettingsItem)
            {
                var settingsButton = new UIBarButtonItem(UIImage.GetSystemImage("gear"), UIBarButtonItemStyle.Plain, this.OpenSettings);
                settingsButton.AccessibilityLabel = this.options.SettingsTitle;
                this.NavigationItem.RightBarButtonItem = settingsButton;
            }

            if (this.View is null)
            {
                throw new NullReferenceException(nameof(this.View));
            }

            this.collectionView = new UICollectionView(this.View.Bounds, this.CreateLayout());
            this.collectionView.Delegate = this;
            this.View.AddSubview(this.collectionView);

            // Anchor collectionView
            this.collectionView.TranslatesAutoresizingMaskIntoConstraints = false;

            var constraints = new List<NSLayoutConstraint>();
            constraints.Add(this.collectionView.BottomAnchor.ConstraintEqualTo(this.View.BottomAnchor));
            constraints.Add(this.collectionView.LeftAnchor.ConstraintEqualTo(this.View.LeftAnchor));
            constraints.Add(this.collectionView.RightAnchor.ConstraintEqualTo(this.View.RightAnchor));
            constraints.Add(this.collectionView.HeightAnchor.ConstraintEqualTo(this.View.HeightAnchor));

            NSLayoutConstraint.ActivateConstraints(constraints.ToArray());

            this.ConfigureDataSource();

            this.SetupNavigationItems(this.GetNavigationSnapshot(this.options.DefaultSidebarItems));

            if (this.options.ShowSearchBar)
            {

            }
        }

        private void SetupNavigationItems(NSDiffableDataSourceSectionSnapshot<NavigationSidebarItem> snapshot)
        {
            if (this.dataSource is null)
            {
                return;
            }

            // Add base sidebar items
            var sectionIdentifier = new NSString("base");
            this.dataSource.ApplySnapshot(snapshot, sectionIdentifier, false);
        }

        private NSDiffableDataSourceSectionSnapshot<NavigationSidebarItem> GetNavigationSnapshot(IEnumerable<NavigationSidebarItem> items)
        {
            var snapshot = new NSDiffableDataSourceSectionSnapshot<NavigationSidebarItem>();

            var headers = items.Where(n => n.Type == SidebarItemType.Header);
            snapshot.AppendItems(headers.ToArray());
            snapshot.ExpandItems(headers.ToArray());

            var rows = items.Where(n => n.Type == SidebarItemType.Row || n.Type == SidebarItemType.ExpandableRow);
            snapshot.AppendItems(rows.ToArray());

            return snapshot;
        }

        private void ConfigureDataSource()
        {
            var headerRegistration = UICollectionViewCellRegistration.GetRegistration(typeof(UICollectionViewListCell),
                new UICollectionViewCellRegistrationConfigurationHandler((cell, indexpath, item) =>
                {
                    var sidebarItem = item as NavigationSidebarItem;
                    if (sidebarItem is null)
                    {
                        return;
                    }

                    var cfg = UIListContentConfiguration.SidebarHeaderConfiguration;
                    cfg.Text = sidebarItem.Title;
                    cell.ContentConfiguration = cfg;
                })
             );

            var expandableRegistration = UICollectionViewCellRegistration.GetRegistration(typeof(UICollectionViewListCell),
                new UICollectionViewCellRegistrationConfigurationHandler((cell, indexpath, item) =>
                {
                    var sidebarItem = item as NavigationSidebarItem;
                    if (sidebarItem is null)
                    {
                        return;
                    }

                    var cfg = UIListContentConfiguration.SidebarHeaderConfiguration;
                    cfg.Text = sidebarItem.Title;
                    cfg.SecondaryText = sidebarItem.Subtitle;
                    cfg.Image = sidebarItem.Image;

                    cell.ContentConfiguration = cfg;
                    ((UICollectionViewListCell)cell).Accessories = new[] { new UICellAccessoryOutlineDisclosure() };
                })
             );

            var rowRegistration = UICollectionViewCellRegistration.GetRegistration(typeof(UICollectionViewListCell),
                new UICollectionViewCellRegistrationConfigurationHandler((cell, indexpath, item) =>
                {
                    var sidebarItem = item as NavigationSidebarItem;
                    if (sidebarItem is null)
                    {
                        return;
                    }

                    var cfg = UIListContentConfiguration.SidebarCellConfiguration;
                    cfg.Text = sidebarItem.Title;
                    cfg.SecondaryText = sidebarItem.Subtitle;
                    cfg.Image = sidebarItem.Image;

                    cell.ContentConfiguration = cfg;
                })
             );

            if (this.collectionView is null)
            {
                throw new NullReferenceException(nameof(this.collectionView));
            }

            this.dataSource = new UICollectionViewDiffableDataSource<NSString, NavigationSidebarItem>(this.collectionView,
                new UICollectionViewDiffableDataSourceCellProvider((collectionView, indexPath, item) =>
                {
                    var sidebarItem = item as NavigationSidebarItem;
                    if (sidebarItem is null || this.collectionView is null)
                    {
                        throw new Exception();
                    }

                    switch (sidebarItem.Type)
                    {
                        case SidebarItemType.Header:
                            return this.collectionView.DequeueConfiguredReusableCell(headerRegistration, indexPath, item);
                        case SidebarItemType.ExpandableRow:
                            return this.collectionView.DequeueConfiguredReusableCell(expandableRegistration, indexPath, item);
                        default:
                            return this.collectionView.DequeueConfiguredReusableCell(rowRegistration, indexPath, item);
                    }
                })
            );
        }

        private UICollectionViewLayout CreateLayout()
        {
            var config = new UICollectionLayoutListConfiguration(UICollectionLayoutListAppearance.Sidebar);
            config.HeaderMode = UICollectionLayoutListHeaderMode.FirstItemInSection;
            config.ShowsSeparators = false;

            return UICollectionViewCompositionalLayout.GetLayout(config);
        }

        private void OpenSettings(object? sender, EventArgs e)
        {
            this.OnSettingsSelect?.Invoke(this, EventArgs.Empty);
        }
    }
}