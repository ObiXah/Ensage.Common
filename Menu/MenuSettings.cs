﻿namespace Ensage.Common.Menu
{
    using System;
    using System.IO;

    using SharpDX;

    using Color = System.Drawing.Color;

    /// <summary>
    ///     The menu settings.
    /// </summary>
    internal static class MenuSettings
    {
        #region Static Fields

        /// <summary>
        ///     The base position.
        /// </summary>
        public static Vector2 BasePosition = new Vector2(10, (float)(HUDInfo.ScreenSizeY() * 0.06));

        /// <summary>
        ///     The _draw the menu.
        /// </summary>
        private static bool drawTheMenu;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="MenuSettings" /> class.
        /// </summary>
        static MenuSettings()
        {
            Game.OnWndProc += Game_OnWndProc;
            drawTheMenu = MenuGlobals.DrawMenu;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the active background color.
        /// </summary>
        public static Color ActiveBackgroundColor
        {
            get
            {
                return Color.FromArgb(210, 48, 48, 48);
            }
        }

        /// <summary>
        ///     Gets the background color.
        /// </summary>
        public static Color BackgroundColor
        {
            get
            {
                return Color.FromArgb(200, Color.Black);
            }
        }

        /// <summary>
        ///     Gets or sets the menu font size.
        /// </summary>
        public static int MenuFontSize { get; set; }

        /// <summary>
        ///     Gets the menu item height.
        /// </summary>
        public static int MenuItemHeight
        {
            get
            {
                return Math.Min(Math.Max((int)(HUDInfo.GetHpBarSizeY() * 2.5), 15), 33)
                       + (Menu.Root.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value * 2); // 32
            }
        }

        /// <summary>
        ///     Gets the menu item width.
        /// </summary>
        public static int MenuItemWidth
        {
            get
            {
                return Math.Max((int)(HUDInfo.GetHPBarSizeX() * 2), 180)
                       + Menu.Root.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value; // 160
            }
        }

        /// <summary>
        ///     Gets the menu menu config path.
        /// </summary>
        public static string MenuMenuConfigPath
        {
            get
            {
                return Path.Combine(MenuConfig.AppDataDirectory, "MenuConfig");
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether draw menu.
        /// </summary>
        internal static bool DrawMenu
        {
            get
            {
                return drawTheMenu;
            }

            set
            {
                drawTheMenu = value;
                MenuGlobals.DrawMenu = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The game_ on wnd proc.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }

            if (Game.IsChatOpen)
            {
                return;
            }

            if ((args.Msg == (uint)Utils.WindowsMessages.WM_KEYUP || args.Msg == (uint)Utils.WindowsMessages.WM_KEYDOWN)
                && args.WParam == CommonMenu.MenuConfig.Item("pressKey").GetValue<KeyBind>().Key)
            {
                DrawMenu = args.Msg == (uint)Utils.WindowsMessages.WM_KEYDOWN;
            }

            if (args.Msg == (uint)Utils.WindowsMessages.WM_KEYUP
                && args.WParam == CommonMenu.MenuConfig.Item("toggleKey").GetValue<KeyBind>().Key)
            {
                DrawMenu = !DrawMenu;
            }
        }

        #endregion
    }
}