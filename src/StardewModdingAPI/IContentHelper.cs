using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace StardewModdingAPI
{
    /// <summary>Provides an API for loading content assets.</summary>
    public interface IContentHelper : IModLinked
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Interceptors which provide the initial versions of matching content assets.</summary>
        IList<IAssetLoader> AssetLoaders { get; }

        /// <summary>Interceptors which edit matching content assets after they're loaded.</summary>
        IList<IAssetEditor> AssetEditors { get; }

        /// <summary>The game's current locale code (like <c>pt-BR</c>).</summary>
        string CurrentLocale { get; }

        /// <summary>The game's current locale as an enum value.</summary>
        LocalizedContentManager.LanguageCode CurrentLocaleConstant { get; }


        /*********
        ** Public methods
        *********/
        /// <summary>Load content from the game folder or mod folder (if not already cached), and return it. When loading a <c>.png</c> file, this must be called outside the game's draw loop.</summary>
        /// <typeparam name="T">The expected data type. The main supported types are <see cref="Texture2D"/> and dictionaries; other types may be supported by the game's content pipeline.</typeparam>
        /// <param name="key">The asset key to fetch (if the <paramref name="source"/> is <see cref="ContentSource.GameContent"/>), or the local path to a content file relative to the mod folder.</param>
        /// <param name="source">Where to search for a matching content asset.</param>
        /// <exception cref="ArgumentException">The <paramref name="key"/> is empty or contains invalid characters.</exception>
        /// <exception cref="ContentLoadException">The content asset couldn't be loaded (e.g. because it doesn't exist).</exception>
        T Load<T>(string key, ContentSource source = ContentSource.ModFolder);

        /// <summary>Get the underlying key in the game's content cache for an asset. This can be used to load custom map tilesheets, but should be avoided when you can use the content API instead. This does not validate whether the asset exists.</summary>
        /// <param name="key">The asset key to fetch (if the <paramref name="source"/> is <see cref="ContentSource.GameContent"/>), or the local path to a content file relative to the mod folder.</param>
        /// <param name="source">Where to search for a matching content asset.</param>
        /// <exception cref="ArgumentException">The <paramref name="key"/> is empty or contains invalid characters.</exception>
        string GetActualAssetKey(string key, ContentSource source = ContentSource.ModFolder);

        /// <summary>Remove an asset from the content cache so it's reloaded on the next request. This will reload core game assets if needed, but references to the former asset will still show the previous content.</summary>
        /// <param name="key">The asset key to invalidate in the content folder.</param>
        /// <exception cref="ArgumentException">The <paramref name="key"/> is empty or contains invalid characters.</exception>
        /// <returns>Returns whether the given asset key was cached.</returns>
        bool InvalidateCache(string key);

        /// <summary>Remove all assets of the given type from the cache so they're reloaded on the next request. <b>This can be a very expensive operation and should only be used in very specific cases.</b> This will reload core game assets if needed, but references to the former assets will still show the previous content.</summary>
        /// <typeparam name="T">The asset type to remove from the cache.</typeparam>
        /// <returns>Returns whether any assets were invalidated.</returns>
        bool InvalidateCache<T>();
    }
}
