# Introduction

Plex Watchlist Migrator is an application meant for to quickly migrate watch data from one Plex instance to another.

## Getting Started

Requires Plex version `1.31.2.6810` or greater (currently tested against `1.31.2.6810`).

Windows Location:
`%LocalAppData%\Plex Media Server\Plug-in Support\Databases\com.plexapp.plugins.library.db`

Ubuntu Location:
`/var/lib/plexmediaserver/...`

Asustor NAS Location:
`/volume1/Plex/Library/Plex Media Server/Plug-in Support/Databases/com.plexapp.plugins.library.db`

See Also: [Stack Overflow](https://askubuntu.com/a/469056)

### Running Migrator

You can run the application by setting the inputfile path and outputfile paths. Input file will point to the source DB you wish to copy/migrate from. Output file points to the destination DB you wish to copy/migrate to.

```shell
.\PlexWatchlistMigrator.Console.exe -i "inputfilepath" -o "outputfilepath"
```

Example:

```shell
.\PlexWatchlistMigrator.Console.exe -i "J:\Temp\Plex_Stuffs\source\com.plexapp.plugins.library.db" -o "J:\Temp\Plex_Stuffs\destination\com.plexapp.plugins.library.db"
```

### Plex Migration Suggestions

Plex themselves [provide a solution](https://support.plex.tv/articles/201154527-move-viewstate-ratings-from-one-install-to-another/). This doesn't work with "on-deck" settings or view history, hence this app.

#### Affected Tables

- accounts
- library_sections
- metadata_item_settings
- metadata_item_views
- metadata_items

### Useful Tools

- Visual Studio 2022
- [DB Browser for SQLite](https://sqlitebrowser.org/)

### Startup Script

[TODO]

### Solution Structure

[TODO]

## Build and Test

[TODO]
